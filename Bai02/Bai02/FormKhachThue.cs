using System.ComponentModel;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using QL_NhaTro.Data;
using QL_NhaTro.Models;
using QL_NhaTro.Services;
using Serilog;

namespace QL_NhaTro;

public sealed partial class FormKhachThue : Form
{
    private const string FriendlyError = "Đã xảy ra lỗi, thông tin đã được ghi lại";
    private readonly AddressApiClient _addressApi = new();
    private List<AddressItem> _provinces = [];
    private List<AddressItem> _wards = [];
    private CancellationTokenSource? _wardRequestCts;
    private int? _selectedGuestId;
    private int? _loadedGuestCount;
    private bool _loadingProvinceSelection;

    public FormKhachThue()
    {
        InitializeComponent();
        _birthPicker.MaxDate = DateTime.Today;

        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            return;

        WireEvents();
        Shown += async (_, _) => await InitializeAsync();
    }

    private void WireEvents()
    {
        _addButton.Click += async (_, _) =>
        {
            if (_selectedGuestId is not null)
            {
                _selectedGuestId = null;
                await RunUiActionAsync(LoadRoomChoicesAsync);
            }
            await RunUiActionAsync(AddGuestAsync);
        };
        _editButton.Click += async (_, _) => await RunUiActionAsync(UpdateGuestAsync);
        _deleteButton.Click += async (_, _) => await RunUiActionAsync(DeleteGuestAsync);
        _clearButton.Click += async (_, _) => { ClearForm(); await LoadRoomChoicesAsync(); };
        _findButton.Click += async (_, _) => await RunUiActionAsync(LoadGuestsAsync);
        _searchBox.KeyDown += async (_, e) =>
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; await RunUiActionAsync(LoadGuestsAsync); }
        };
        _clearFilterButton.Click += async (_, _) =>
        {
            _searchBox.Clear();
            _roomFilter.SelectedIndex = 0;
            _provinceFilter.SelectedIndex = 0;
            await RunUiActionAsync(LoadGuestsAsync);
        };
        _provinceCombo.SelectedIndexChanged += async (_, _) =>
        {
            if (!_loadingProvinceSelection && _provinceCombo.SelectedItem is AddressItem province)
                await LoadWardsAsync(province);
        };
        _grid.SelectionChanged += (_, _) =>
        {
            if (_grid.SelectedRows.Count > 0 && _grid.CurrentRow?.DataBoundItem is GuestRow row)
                _ = RunUiActionAsync(() => LoadGuestToFormAsync(row));
        };
        _testErrorButton.Click += (_, _) => throw new InvalidOperationException("Lỗi thử nghiệm theo yêu cầu.");
    }

    private async Task InitializeAsync()
    {
        _statusLabel.Text = "Đang kết nối CSDL…";
        await RunUiActionAsync(async () =>
        {
            await LoadRoomChoicesAsync();
            await LoadGuestsAsync();
        });
        await LoadProvincesAsync();
    }

    private async Task LoadProvincesAsync()
    {
        _statusLabel.Text = "Đang tải danh sách tỉnh/thành…";
        _provinceCombo.Enabled = false;
        try
        {
            _provinces = (await _addressApi.GetProvincesAsync()).OrderBy(p => p.Name).ToList();
            _provinceCombo.DataSource = null;
            _provinceCombo.DisplayMember = nameof(AddressItem.Name);
            _provinceCombo.ValueMember = nameof(AddressItem.Code);
            _provinceCombo.DataSource = _provinces;
            _provinceCombo.SelectedIndex = -1;
            _provinceCombo.Text = "";
            _statusLabel.Text = AddGuestCountToStatus($"Đã tải {_provinces.Count} tỉnh/thành từ provinces.open-api.vn");
            _provinceCombo.Enabled = true;
        }
        catch (HttpRequestException ex)
        {
            HandleAddressApiFailure(ex, "tỉnh/thành");
        }
        catch (TaskCanceledException ex)
        {
            HandleAddressApiFailure(ex, "tỉnh/thành (quá thời gian chờ 10 giây)");
        }
        catch (System.Text.Json.JsonException ex)
        {
            HandleAddressApiFailure(ex, "tỉnh/thành (dữ liệu phản hồi không hợp lệ)");
        }
        finally
        {
            _provinceCombo.Enabled = true;
            if (_provinces.Count == 0)
            {
                _provinceCombo.DropDownStyle = ComboBoxStyle.DropDown;
                _statusLabel.Text = "Không có dữ liệu địa chỉ API; có thể nhập Tỉnh/Thành và Phường/Xã bằng tay";
            }
        }
    }

    private async Task LoadWardsAsync(AddressItem province)
    {
        _wardRequestCts?.Cancel();
        _wardRequestCts?.Dispose();
        var request = _wardRequestCts = new CancellationTokenSource();
        _wardCombo.Enabled = false;
        _wardCombo.Text = "Đang tải…";
        try
        {
            var wards = (await _addressApi.GetWardsAsync(province.Code, request.Token)).OrderBy(w => w.Name).ToList();
            if (request.IsCancellationRequested || !ReferenceEquals(request, _wardRequestCts)) return;
            _wards = wards;
            _wardCombo.DataSource = null;
            _wardCombo.DisplayMember = nameof(AddressItem.Name);
            _wardCombo.ValueMember = nameof(AddressItem.Code);
            _wardCombo.DataSource = _wards;
            _wardCombo.SelectedIndex = -1;
            _wardCombo.Text = "";
            _statusLabel.Text = $"Đã tải {wards.Count} phường/xã thuộc {province.Name}";
        }
        catch (TaskCanceledException ex) when (request.IsCancellationRequested)
        {
            Log.Debug(ex, "Đã hủy yêu cầu tải phường/xã do người dùng đổi tỉnh");
        }
        catch (HttpRequestException ex)
        {
            HandleAddressApiFailure(ex, "phường/xã");
            MakeWardComboEditable();
        }
        catch (TaskCanceledException ex)
        {
            HandleAddressApiFailure(ex, "phường/xã (quá thời gian chờ 10 giây)");
            MakeWardComboEditable();
        }
        catch (System.Text.Json.JsonException ex)
        {
            HandleAddressApiFailure(ex, "phường/xã (dữ liệu phản hồi không hợp lệ)");
            MakeWardComboEditable();
        }
        finally
        {
            if (ReferenceEquals(request, _wardRequestCts)) _wardCombo.Enabled = true;
        }
    }

    private void HandleAddressApiFailure(Exception exception, string operation)
    {
        _addressApi.LogApiWarning(exception, operation);
        _statusLabel.Text = "Không tải được danh sách địa chỉ; bạn có thể nhập tay";
        MakeWardComboEditable();
        MessageBox.Show(
            "Không tải được danh sách địa chỉ, vui lòng kiểm tra mạng. Bạn vẫn có thể gõ tay Tỉnh/Thành và Phường/Xã để lưu khách.",
            "Không tải được địa chỉ",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
    }

    private void MakeWardComboEditable()
    {
        _wardCombo.DataSource = null;
        _wardCombo.Items.Clear();
        _wardCombo.Text = "";
        _wardCombo.DropDownStyle = ComboBoxStyle.DropDown;
        _wardCombo.Enabled = true;
    }

    private async Task LoadRoomChoicesAsync()
    {
        await using var db = new QLNhaTroContext();
        var selectedId = _selectedGuestId is null ? (int?)null : await db.KhachThues
            .Where(g => g.MaKhach == _selectedGuestId.Value)
            .Select(g => (int?)g.MaPhong)
            .FirstOrDefaultAsync();
        var allRooms = await db.PhongTros
            .OrderBy(room => room.TenPhong)
            .Select(room => new RoomOption(room.MaPhong, room.TenPhong, room.GiaThue))
            .ToListAsync();
        var occupiedRoomIds = await db.KhachThues.Select(guest => guest.MaPhong).Distinct().ToListAsync();
        var rooms = allRooms.Where(room => !occupiedRoomIds.Contains(room.MaPhong) || room.MaPhong == selectedId).ToList();
        _roomCombo.DataSource = null;
        _roomCombo.DisplayMember = nameof(RoomOption.Display);
        _roomCombo.ValueMember = nameof(RoomOption.MaPhong);
        _roomCombo.DataSource = rooms;
        if (selectedId is not null) _roomCombo.SelectedValue = selectedId.Value;
        else if (rooms.Count > 0) _roomCombo.SelectedIndex = 0;
        RefreshRoomFilter(allRooms);
    }

    private void RefreshRoomFilter(List<RoomOption> rooms)
    {
        var selected = (_roomFilter.SelectedItem as RoomFilterOption)?.MaPhong;
        var options = new List<RoomFilterOption> { new(null, "Tất cả") };
        options.AddRange(rooms.Select(room => new RoomFilterOption(room.MaPhong, room.TenPhong)));
        _roomFilter.DisplayMember = nameof(RoomFilterOption.Display);
        _roomFilter.ValueMember = nameof(RoomFilterOption.MaPhong);
        _roomFilter.DataSource = options;
        if (selected is not null) _roomFilter.SelectedValue = selected;
    }

    private async Task LoadGuestsAsync()
    {
        await using var db = new QLNhaTroContext();
        var query = db.KhachThues.Include(g => g.MaPhongNavigation).AsQueryable();
        var search = _searchBox.Text.Trim();
        if (search.Length > 0)
            query = query.Where(g => g.HoTen.Contains(search) || g.SoDienThoai.Contains(search));
        if ((_roomFilter.SelectedItem as RoomFilterOption)?.MaPhong is int roomId)
            query = query.Where(g => g.MaPhong == roomId);
        if ((_provinceFilter.SelectedItem as ProvinceFilterOption)?.Name is string provinceName)
            query = query.Where(g => g.TenTinh == provinceName);

        var guests = await query.OrderBy(g => g.HoTen)
            .Select(g => new GuestRow(
                g.MaKhach,
                g.HoTen,
                g.SoDienThoai,
                g.MaPhongNavigation.TenPhong,
                g.MaPhongNavigation.GiaThue,
                g.TenPhuongXa,
                g.TenTinh,
                g.DaDangKyTamTru,
                g.GioiTinh,
                g.NgaySinh,
                g.MaTinh,
                g.MaPhuongXa,
                g.MaPhong))
            .ToListAsync();
        _grid.DataSource = new BindingList<GuestRow>(guests);
        RefreshProvinceFilter(guests.Select(g => g.TenTinh));
        _loadedGuestCount = guests.Count;
        _statusLabel.Text = $"Có {guests.Count} khách thuê";
    }

    private void RefreshProvinceFilter(IEnumerable<string?> names)
    {
        var selectedName = (_provinceFilter.SelectedItem as ProvinceFilterOption)?.Name;
        var allNames = _provinces.Select(p => p.Name)
            .Concat(names)
            .OfType<string>()
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .Distinct(StringComparer.CurrentCultureIgnoreCase)
            .OrderBy(name => name)
            .ToList();
        var options = new List<ProvinceFilterOption> { new(null, "Tất cả") };
        options.AddRange(allNames.Select(name => new ProvinceFilterOption(name, name)));
        _provinceFilter.DisplayMember = nameof(ProvinceFilterOption.Display);
        _provinceFilter.ValueMember = nameof(ProvinceFilterOption.Name);
        _provinceFilter.DataSource = options;
        if (selectedName is not null) _provinceFilter.SelectedValue = selectedName;
    }

    private string AddGuestCountToStatus(string message) =>
        _loadedGuestCount is int count ? $"{message} • {count} khách thuê" : message;

    private async Task AddGuestAsync()
    {
        if (!ValidateForm(out var roomId, out var province, out var ward)) return;
        await using var db = new QLNhaTroContext();
        var room = await db.PhongTros.SingleOrDefaultAsync(p => p.MaPhong == roomId);
        if (room is null || await db.KhachThues.AnyAsync(g => g.MaPhong == roomId))
        {
            MessageBox.Show("Phòng này vừa được chuyển sang trạng thái đang thuê. Hãy chọn phòng trống.", "Không thể thêm khách", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LoadRoomChoicesAsync();
            return;
        }

        await using var transaction = await db.Database.BeginTransactionAsync();
        db.KhachThues.Add(CreateGuest(roomId, province, ward));
        room.TinhTrang = "Đang thuê";
        await db.SaveChangesAsync();
        await transaction.CommitAsync();
        MessageBox.Show("Đã thêm khách thuê.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ClearForm();
        await LoadRoomChoicesAsync();
        await LoadGuestsAsync();
    }

    private async Task UpdateGuestAsync()
    {
        if (_selectedGuestId is null)
        {
            MessageBox.Show("Hãy chọn khách cần sửa trên danh sách.", "Chưa chọn khách", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        if (!ValidateForm(out var roomId, out var province, out var ward)) return;
        await using var db = new QLNhaTroContext();
        var guest = await db.KhachThues.SingleOrDefaultAsync(g => g.MaKhach == _selectedGuestId.Value);
        if (guest is null) { MessageBox.Show("Khách thuê không còn trong CSDL."); await LoadGuestsAsync(); return; }
        var room = await db.PhongTros.SingleOrDefaultAsync(p => p.MaPhong == roomId);
        if (room is null || (roomId != guest.MaPhong && await db.KhachThues.AnyAsync(g => g.MaPhong == roomId)))
        {
            MessageBox.Show("Chỉ có thể chuyển khách sang phòng trống.", "Không thể sửa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LoadRoomChoicesAsync();
            return;
        }

        var oldRoomId = guest.MaPhong;
        await using var transaction = await db.Database.BeginTransactionAsync();
        guest.HoTen = _nameBox.Text.Trim();
        guest.GioiTinh = _maleRadio.Checked ? "Nam" : "Nữ";
        guest.NgaySinh = _birthPicker.Value.Date;
        guest.SoDienThoai = _phoneBox.Text.Trim();
        guest.MaTinh = province.Code;
        guest.TenTinh = province.Name;
        guest.MaPhuongXa = ward.Code;
        guest.TenPhuongXa = ward.Name;
        guest.DaDangKyTamTru = _registeredCheck.Checked;
        guest.MaPhong = roomId;
        await db.SaveChangesAsync();
        await RefreshRoomStatusAsync(db, oldRoomId);
        await RefreshRoomStatusAsync(db, roomId);
        await db.SaveChangesAsync();
        await transaction.CommitAsync();
        MessageBox.Show("Đã cập nhật thông tin khách thuê.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ClearForm();
        await LoadRoomChoicesAsync();
        await LoadGuestsAsync();
    }

    private async Task DeleteGuestAsync()
    {
        if (_selectedGuestId is null)
        {
            MessageBox.Show("Hãy chọn khách cần xóa trên danh sách.", "Chưa chọn khách", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        if (MessageBox.Show("Bạn có chắc muốn xóa khách thuê đang chọn?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        await using var db = new QLNhaTroContext();
        var guest = await db.KhachThues.SingleOrDefaultAsync(g => g.MaKhach == _selectedGuestId.Value);
        if (guest is null) { await LoadGuestsAsync(); return; }
        var roomId = guest.MaPhong;
        await using var transaction = await db.Database.BeginTransactionAsync();
        db.KhachThues.Remove(guest);
        await db.SaveChangesAsync();
        await RefreshRoomStatusAsync(db, roomId);
        await db.SaveChangesAsync();
        await transaction.CommitAsync();
        MessageBox.Show("Đã xóa khách thuê.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ClearForm();
        await LoadRoomChoicesAsync();
        await LoadGuestsAsync();
    }

    private async Task RefreshRoomStatusAsync(QLNhaTroContext db, int roomId)
    {
        var room = await db.PhongTros.SingleOrDefaultAsync(p => p.MaPhong == roomId);
        if (room is not null)
            room.TinhTrang = await db.KhachThues.AnyAsync(g => g.MaPhong == roomId) ? "Đang thuê" : "Trống";
    }

    private KhachThue CreateGuest(int roomId, (int Code, string Name) province, (int Code, string Name) ward) => new()
    {
        HoTen = _nameBox.Text.Trim(),
        GioiTinh = _maleRadio.Checked ? "Nam" : "Nữ",
        NgaySinh = _birthPicker.Value.Date,
        SoDienThoai = _phoneBox.Text.Trim(),
        MaTinh = province.Code,
        TenTinh = province.Name,
        MaPhuongXa = ward.Code,
        TenPhuongXa = ward.Name,
        DaDangKyTamTru = _registeredCheck.Checked,
        MaPhong = roomId
    };

    private bool ValidateForm(out int roomId, out (int Code, string Name) province, out (int Code, string Name) ward)
    {
        roomId = 0;
        province = default;
        ward = default;
        var name = _nameBox.Text.Trim();
        var phone = _phoneBox.Text.Trim();
        if (name.Length == 0)
        {
            MessageBox.Show("Vui lòng nhập họ tên khách thuê.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _nameBox.Focus(); return false;
        }
        if (phone.Length == 0 || phone.Length > 15 || phone.Any(ch => !char.IsDigit(ch) && ch is not '+' and not '-' and not '(' and not ')' and not ' '))
        {
            MessageBox.Show("Số điện thoại chỉ gồm chữ số, dấu +, dấu cách hoặc dấu gạch ngang; tối đa 15 ký tự.", "Số điện thoại chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _phoneBox.Focus(); return false;
        }
        if (_roomCombo.SelectedValue is not int selectedRoom)
        {
            MessageBox.Show("Không còn phòng trống để chọn. Hãy kiểm tra danh sách phòng.", "Thiếu phòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
        roomId = selectedRoom;

        var provinceItem = _provinceCombo.SelectedItem as AddressItem;
        var provinceName = provinceItem?.Name ?? _provinceCombo.Text.Trim();
        var wardItem = _wardCombo.SelectedItem as AddressItem;
        var wardName = wardItem?.Name ?? _wardCombo.Text.Trim();
        if (provinceName.Length == 0 || wardName.Length == 0)
        {
            MessageBox.Show("Vui lòng chọn hoặc nhập Tỉnh/Thành và Phường/Xã.", "Thiếu quê quán", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
        province = (provinceItem?.Code ?? 0, provinceName);
        ward = (wardItem?.Code ?? 0, wardName);
        return true;
    }

    private async Task LoadGuestToFormAsync(GuestRow row)
    {
        _selectedGuestId = row.MaKhach;
        _nameBox.Text = row.HoTen;
        _maleRadio.Checked = row.GioiTinh == "Nam";
        _femaleRadio.Checked = row.GioiTinh != "Nam";
        _birthPicker.Value = row.NgaySinh < _birthPicker.MinDate ? _birthPicker.MinDate : row.NgaySinh > DateTime.Today ? DateTime.Today : row.NgaySinh;
        _phoneBox.Text = row.SoDienThoai;
        _registeredCheck.Checked = row.DaDangKyTamTru;
        await LoadRoomChoicesAsync();
        _roomCombo.SelectedValue = row.MaPhong;

        _loadingProvinceSelection = true;
        var provinceItem = _provinces.FirstOrDefault(p => p.Code == row.MaTinh)
            ?? _provinces.FirstOrDefault(p => string.Equals(p.Name, row.TenTinh, StringComparison.CurrentCultureIgnoreCase));
        _provinceCombo.SelectedItem = provinceItem;
        if (provinceItem is null) _provinceCombo.Text = row.TenTinh ?? "";
        _loadingProvinceSelection = false;
        if (provinceItem is not null)
        {
            await LoadWardsAsync(provinceItem);
            var wardItem = _wards.FirstOrDefault(w => w.Code == row.MaPhuongXa)
                ?? _wards.FirstOrDefault(w => string.Equals(w.Name, row.TenPhuongXa, StringComparison.CurrentCultureIgnoreCase));
            _wardCombo.SelectedItem = wardItem;
            if (wardItem is null) _wardCombo.Text = row.TenPhuongXa ?? "";
        }
        else
        {
            MakeWardComboEditable();
            _wardCombo.Text = row.TenPhuongXa ?? "";
        }
    }

    private void ClearForm()
    {
        _selectedGuestId = null;
        _nameBox.Clear();
        _maleRadio.Checked = true;
        _birthPicker.Value = DateTime.Today;
        _phoneBox.Clear();
        _registeredCheck.Checked = false;
        _roomCombo.SelectedIndex = _roomCombo.Items.Count > 0 ? 0 : -1;
        _loadingProvinceSelection = true;
        _provinceCombo.SelectedIndex = -1;
        _provinceCombo.Text = "";
        _loadingProvinceSelection = false;
        _wardRequestCts?.Cancel();
        _wards.Clear();
        _wardCombo.DataSource = null;
        _wardCombo.Items.Clear();
        _wardCombo.Text = "";
        _wardCombo.Enabled = true;
        _addButton.Enabled = true;
        _editButton.Enabled = true;
        _deleteButton.Enabled = true;
        _grid.ClearSelection();
    }

    private async Task RunUiActionAsync(Func<Task> action)
    {
        try { await action(); }
        catch (Exception ex)
        {
            Log.Error(ex, "Lỗi khi thực hiện thao tác trên form khách thuê");
            MessageBox.Show(FriendlyError, "Quản lý nhà trọ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == (Keys.Control | Keys.Shift | Keys.E))
        {
            throw new InvalidOperationException("Lỗi thử nghiệm theo yêu cầu.");
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }

    private sealed record RoomOption(int MaPhong, string TenPhong, decimal GiaThue)
    {
        public string Display => $"{TenPhong} - {GiaThue:N0} đ";
    }
    private sealed record RoomFilterOption(int? MaPhong, string Display);
    private sealed record ProvinceFilterOption(string? Name, string Display);
    private sealed record GuestRow(
        int MaKhach,
        string HoTen,
        string SoDienThoai,
        string TenPhong,
        decimal GiaThue,
        string? TenPhuongXa,
        string? TenTinh,
        bool DaDangKyTamTru,
        string GioiTinh,
        DateTime NgaySinh,
        int? MaTinh,
        int? MaPhuongXa,
        int MaPhong)
    {
        public string GiaThueDisplay => $"{GiaThue:N0} đ";
        public string QueQuan => string.Join(" – ", new[] { TenPhuongXa, TenTinh }.Where(value => !string.IsNullOrWhiteSpace(value)));
    }
}
