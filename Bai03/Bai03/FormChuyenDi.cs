using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bai03.Helpers;
using Bai03.Localization;
using Bai03.Models;
using Bai03.Services;
using Microsoft.EntityFrameworkCore;

namespace Bai03
{
    public partial class FormChuyenDi : Form
    {
        private QlDaNgoaiContext _context;
        private List<ChuyenDi> _allTrips = new();
        private ChuyenDi? _selectedTrip = null;
        private WeatherResult? _lastWeatherResult = null;
        private bool _isBinding = false;

        public FormChuyenDi()
        {
            InitializeComponent();
            _context = new QlDaNgoaiContext();
        }

        private async void FormChuyenDi_Load(object sender, EventArgs e)
        {
            // 1. Tải cài đặt ngôn ngữ đã lưu từ %LocalAppData%\usersettings.json
            var settings = UserSettingsService.LoadSettings();
            UserSettingsService.SaveSettings(settings); // Đảm bảo file được tạo sẵn
            LocalizationService.CurrentLanguage = settings.Language;

            // Thiết lập ComboBox ngôn ngữ
            _isBinding = true;
            cboLanguage.SelectedIndex = (settings.Language == "en") ? 1 : 0;
            _isBinding = false;

            // 2. Áp dụng ngôn ngữ
            ApplyLanguage();

            // 3. Khởi tạo mặc định form
            ResetForm();

            // 4. Tải dữ liệu từ CSDL
            await LoadTripsAsync();

            // 5. Nếu có dòng đầu tiên, chọn và xem thời tiết luôn
            if (dgvChuyenDi.Rows.Count > 0)
            {
                dgvChuyenDi.Rows[0].Selected = true;
            }
        }

        #region Đa Ngôn Ngữ (Task 8)

        private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isBinding) return;

            string selectedLang = cboLanguage.SelectedIndex == 1 ? "en" : "vi";
            if (LocalizationService.CurrentLanguage != selectedLang)
            {
                LocalizationService.CurrentLanguage = selectedLang;

                // Lưu vào %LocalAppData%\usersettings.json
                UserSettingsService.SaveSettings(new UserSettings { Language = selectedLang });

                // Cập nhật toàn bộ giao diện ngay lập tức
                ApplyLanguage();
            }
        }

        private void ApplyLanguage()
        {
            this.Text = LocalizationService.Get("AppTitle");
            lblHeaderTitle.Text = LocalizationService.Get("HeaderTitle");
            lblLanguage.Text = LocalizationService.Get("LanguageLabel") + " :";

            // GroupBox & Fields
            grpTripInfo.Text = LocalizationService.Get("TripInfoGroup");
            lblTripName.Text = LocalizationService.Get("TripName") + " :";
            lblDestination.Text = LocalizationService.Get("Destination") + " :";
            lblDepartureDate.Text = LocalizationService.Get("DepartureDate") + " :";
            lblPeopleCount.Text = LocalizationService.Get("PeopleCount") + " :";
            lblBudget.Text = LocalizationService.Get("Budget") + " :";
            lblVehicle.Text = LocalizationService.Get("Vehicle") + " :";
            chkDaHoanThanh.Text = LocalizationService.Get("Completed");

            // Buttons
            btnThem.Text = LocalizationService.Get("BtnAdd");
            btnSua.Text = LocalizationService.Get("BtnUpdate");
            btnXoa.Text = LocalizationService.Get("BtnDelete");
            btnLamMoi.Text = LocalizationService.Get("BtnReset");
            btnXemThoiTiet.Text = LocalizationService.Get("BtnCheckWeather");

            // DateTimePicker format
            dtpNgayDi.CustomFormat = LocalizationService.DateFormat;
            dtpTuNgay.CustomFormat = LocalizationService.DateFormat;
            dtpDenNgay.CustomFormat = LocalizationService.DateFormat;

            // ComboBox Phương tiện
            string currentVehicle = cboPhuongTien.SelectedItem?.ToString() ?? string.Empty;
            string rawVehicle = LocalizationService.GetVehicleDbValue(currentVehicle);

            cboPhuongTien.Items.Clear();
            cboPhuongTien.Items.Add(LocalizationService.Get("VehicleMotorbike"));
            cboPhuongTien.Items.Add(LocalizationService.Get("VehicleCar"));
            cboPhuongTien.Items.Add(LocalizationService.Get("VehicleBus"));
            cboPhuongTien.Items.Add(LocalizationService.Get("VehicleTrain"));

            string localizedVehicle = LocalizationService.GetVehicleLocalized(rawVehicle);
            int idx = cboPhuongTien.Items.IndexOf(localizedVehicle);
            cboPhuongTien.SelectedIndex = idx >= 0 ? idx : 0;

            // Search Panel
            txtTimKiem.PlaceholderText = LocalizationService.Get("SearchPlaceholder");
            chkLocChuaHoanThanh.Text = LocalizationService.Get("SearchFilterOnlyIncomplete");
            btnTim.Text = LocalizationService.Get("BtnSearch");
            btnTatCa.Text = LocalizationService.Get("BtnShowAll");

            // DataGridView Headers
            colMaChuyen.HeaderText = LocalizationService.Get("ColId");
            colTenChuyen.HeaderText = LocalizationService.Get("ColTripName");
            colDiaDiem.HeaderText = LocalizationService.Get("ColDestination");
            colNgayDi.HeaderText = LocalizationService.Get("ColDepartureDate");
            colSoNguoi.HeaderText = LocalizationService.Get("ColPeople");
            colNganSach.HeaderText = LocalizationService.Get("ColBudget");
            colPhuongTien.HeaderText = LocalizationService.Get("ColVehicle");
            colDaHoanThanh.HeaderText = LocalizationService.Get("ColCompleted");

            // Cập nhật lại khung hiển thị thời tiết hiện tại nếu có
            UpdateWeatherDisplay();

            // Refresh grid để format lại tiền tệ và ngày tháng
            dgvChuyenDi.Refresh();
        }

        #endregion

        #region Tải và Hiển thị Dữ liệu (Task 1 & 4)

        private async Task LoadTripsAsync()
        {
            try
            {
                _allTrips = await _context.ChuyenDis.OrderBy(c => c.NgayDi).ToListAsync();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"{LocalizationService.Get("MsgError")}: {ex.Message}",
                    LocalizationService.Get("MsgError"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ApplyFilter()
        {
            var filtered = _allTrips.AsEnumerable();

            // Lọc theo từ khóa tìm kiếm (tên chuyến hoặc địa điểm)
            string keyword = txtTimKiem.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                string kwUnaccent = WeatherService.RemoveDiacritics(keyword).ToLower();
                filtered = filtered.Where(t =>
                    t.TenChuyen.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    t.DiaDiem.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    WeatherService.RemoveDiacritics(t.TenChuyen).ToLower().Contains(kwUnaccent) ||
                    WeatherService.RemoveDiacritics(t.DiaDiem).ToLower().Contains(kwUnaccent)
                );
            }

            // Lọc chỉ hiện chuyến chưa hoàn thành
            if (chkLocChuaHoanThanh.Checked)
            {
                filtered = filtered.Where(t => !t.DaHoanThanh);
            }

            // Lọc theo khoảng ngày đi
            if (chkLocNgay.Checked)
            {
                var fromDate = DateOnly.FromDateTime(dtpTuNgay.Value.Date);
                var toDate = DateOnly.FromDateTime(dtpDenNgay.Value.Date);
                filtered = filtered.Where(t => t.NgayDi >= fromDate && t.NgayDi <= toDate);
            }

            var list = filtered.ToList();

            _isBinding = true;
            dgvChuyenDi.DataSource = null;
            dgvChuyenDi.DataSource = list;
            _isBinding = false;
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void chkLocNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpTuNgay.Enabled = chkLocNgay.Checked;
            dtpDenNgay.Enabled = chkLocNgay.Checked;
            ApplyFilter();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btnTatCa_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            chkLocChuaHoanThanh.Checked = false;
            chkLocNgay.Checked = false;
            dtpTuNgay.Value = DateTime.Today;
            dtpDenNgay.Value = DateTime.Today.AddDays(30);
            ApplyFilter();
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ApplyFilter();
            }
        }

        private void dgvChuyenDi_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;

            string colName = dgvChuyenDi.Columns[e.ColumnIndex].Name;

            // Format ngày đi theo Culture
            if (colName == "colNgayDi" && e.Value is DateOnly dateVal)
            {
                e.Value = LocalizationService.FormatDate(dateVal);
                e.FormattingApplied = true;
            }
            // Format ngân sách theo Culture
            else if (colName == "colNganSach" && e.Value is decimal budgetVal)
            {
                e.Value = LocalizationService.FormatMoney(budgetVal);
                e.FormattingApplied = true;
            }
            // Dịch tên phương tiện
            else if (colName == "colPhuongTien" && e.Value is string vehicleStr)
            {
                e.Value = LocalizationService.GetVehicleLocalized(vehicleStr);
                e.FormattingApplied = true;
            }
        }

        private int _weatherRequestId = 0;

        private void BindTripToForm(ChuyenDi trip)
        {
            _selectedTrip = trip;

            // Đổ dữ liệu lên form ĐỒNG BỘ và NGAY LẬP TỨC
            txtTenChuyen.Text = trip.TenChuyen;
            txtDiaDiem.Text = trip.DiaDiem;
            dtpNgayDi.Value = trip.NgayDi.ToDateTime(TimeOnly.MinValue);
            nudSoNguoi.Value = Math.Max(1, trip.SoNguoi);
            nudNganSach.Value = Math.Max(0, trip.NganSach);

            string locVehicle = LocalizationService.GetVehicleLocalized(trip.PhuongTien);
            int idx = cboPhuongTien.Items.IndexOf(locVehicle);
            cboPhuongTien.SelectedIndex = idx >= 0 ? idx : 0;

            chkDaHoanThanh.Checked = trip.DaHoanThanh;
        }

        private async void dgvChuyenDi_SelectionChanged(object sender, EventArgs e)
        {
            if (_isBinding || dgvChuyenDi.CurrentRow == null) return;

            if (dgvChuyenDi.CurrentRow.DataBoundItem is ChuyenDi trip)
            {
                BindTripToForm(trip);

                // Tải thời tiết với cờ isUserManualClick = false (không hiện popup modal chặn UI)
                await FetchAndDisplayWeatherAsync(trip.DiaDiem, trip.NgayDi, isUserManualClick: false);
            }
        }

        private async void dgvChuyenDi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _isBinding) return;

            if (dgvChuyenDi.Rows[e.RowIndex].DataBoundItem is ChuyenDi trip)
            {
                BindTripToForm(trip);
                await FetchAndDisplayWeatherAsync(trip.DiaDiem, trip.NgayDi, isUserManualClick: false);
            }
        }

        #endregion

        #region CRUD (Task 3)

        private async void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra tính hợp lệ
            string tenChuyen = txtTenChuyen.Text.Trim();
            string diaDiem = txtDiaDiem.Text.Trim();

            if (string.IsNullOrEmpty(tenChuyen))
            {
                MessageBox.Show(LocalizationService.Get("MsgEnterTripName"), LocalizationService.Get("MsgNotification"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenChuyen.Focus();
                return;
            }

            if (string.IsNullOrEmpty(diaDiem))
            {
                MessageBox.Show(LocalizationService.Get("MsgEnterDestination"), LocalizationService.Get("MsgNotification"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaDiem.Focus();
                return;
            }

            // Kiểm tra: ngày đi không nhỏ hơn hôm nay khi thêm mới
            DateOnly ngayDi = DateOnly.FromDateTime(dtpNgayDi.Value.Date);
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            if (ngayDi < today)
            {
                MessageBox.Show(LocalizationService.Get("MsgInvalidDepartureDate"), LocalizationService.Get("MsgNotification"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayDi.Focus();
                return;
            }

            if (nudSoNguoi.Value < 1)
            {
                MessageBox.Show(LocalizationService.Get("MsgInvalidPeople"), LocalizationService.Get("MsgNotification"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudSoNguoi.Focus();
                return;
            }

            if (nudNganSach.Value < 0)
            {
                MessageBox.Show(LocalizationService.Get("MsgInvalidBudget"), LocalizationService.Get("MsgNotification"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudNganSach.Focus();
                return;
            }

            string displayVehicle = cboPhuongTien.SelectedItem?.ToString() ?? "Xe máy";
            string dbVehicle = LocalizationService.GetVehicleDbValue(displayVehicle);

            try
            {
                var newTrip = new ChuyenDi
                {
                    TenChuyen = tenChuyen,
                    DiaDiem = diaDiem,
                    NgayDi = ngayDi,
                    SoNguoi = (int)nudSoNguoi.Value,
                    NganSach = nudNganSach.Value,
                    PhuongTien = dbVehicle,
                    DaHoanThanh = chkDaHoanThanh.Checked,
                    GhiChu = null
                };

                _context.ChuyenDis.Add(newTrip);
                await _context.SaveChangesAsync();

                MessageBox.Show(LocalizationService.Get("MsgAddSuccess"), LocalizationService.Get("MsgNotification"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadTripsAsync();

                // Chọn dòng vừa thêm
                SelectTripById(newTrip.MaChuyen);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{LocalizationService.Get("MsgError")}: {ex.Message}", LocalizationService.Get("MsgError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (_selectedTrip == null)
            {
                MessageBox.Show(LocalizationService.Get("MsgSelectTripFirst"), LocalizationService.Get("MsgNotification"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenChuyen = txtTenChuyen.Text.Trim();
            string diaDiem = txtDiaDiem.Text.Trim();

            if (string.IsNullOrEmpty(tenChuyen))
            {
                MessageBox.Show(LocalizationService.Get("MsgEnterTripName"), LocalizationService.Get("MsgNotification"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenChuyen.Focus();
                return;
            }

            if (string.IsNullOrEmpty(diaDiem))
            {
                MessageBox.Show(LocalizationService.Get("MsgEnterDestination"), LocalizationService.Get("MsgNotification"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaDiem.Focus();
                return;
            }

            if (nudSoNguoi.Value < 1)
            {
                MessageBox.Show(LocalizationService.Get("MsgInvalidPeople"), LocalizationService.Get("MsgNotification"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudSoNguoi.Focus();
                return;
            }

            if (nudNganSach.Value < 0)
            {
                MessageBox.Show(LocalizationService.Get("MsgInvalidBudget"), LocalizationService.Get("MsgNotification"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudNganSach.Focus();
                return;
            }

            string displayVehicle = cboPhuongTien.SelectedItem?.ToString() ?? "Xe máy";
            string dbVehicle = LocalizationService.GetVehicleDbValue(displayVehicle);

            try
            {
                var tripToUpdate = await _context.ChuyenDis.FindAsync(_selectedTrip.MaChuyen);
                if (tripToUpdate != null)
                {
                    tripToUpdate.TenChuyen = tenChuyen;
                    tripToUpdate.DiaDiem = diaDiem;
                    tripToUpdate.NgayDi = DateOnly.FromDateTime(dtpNgayDi.Value.Date);
                    tripToUpdate.SoNguoi = (int)nudSoNguoi.Value;
                    tripToUpdate.NganSach = nudNganSach.Value;
                    tripToUpdate.PhuongTien = dbVehicle;
                    tripToUpdate.DaHoanThanh = chkDaHoanThanh.Checked;

                    await _context.SaveChangesAsync();

                    MessageBox.Show(LocalizationService.Get("MsgUpdateSuccess"), LocalizationService.Get("MsgNotification"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    int savedId = tripToUpdate.MaChuyen;
                    await LoadTripsAsync();
                    SelectTripById(savedId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{LocalizationService.Get("MsgError")}: {ex.Message}", LocalizationService.Get("MsgError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (_selectedTrip == null)
            {
                MessageBox.Show(LocalizationService.Get("MsgSelectTripFirst"), LocalizationService.Get("MsgNotification"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string confirmMsg = string.Format(LocalizationService.Get("MsgConfirmDelete"), _selectedTrip.TenChuyen);
            var confirmResult = MessageBox.Show(confirmMsg, LocalizationService.Get("MsgConfirmDeleteTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    var tripToDelete = await _context.ChuyenDis.FindAsync(_selectedTrip.MaChuyen);
                    if (tripToDelete != null)
                    {
                        _context.ChuyenDis.Remove(tripToDelete);
                        await _context.SaveChangesAsync();

                        MessageBox.Show(LocalizationService.Get("MsgDeleteSuccess"), LocalizationService.Get("MsgNotification"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ResetForm();
                        await LoadTripsAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{LocalizationService.Get("MsgError")}: {ex.Message}", LocalizationService.Get("MsgError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            dgvChuyenDi.ClearSelection();
        }

        private void ResetForm()
        {
            _selectedTrip = null;
            txtTenChuyen.Clear();
            txtDiaDiem.Clear();
            dtpNgayDi.Value = DateTime.Today;
            nudSoNguoi.Value = 1;
            nudNganSach.Value = 0;
            cboPhuongTien.SelectedIndex = 0;
            chkDaHoanThanh.Checked = false;

            // Reset Panel Thời tiết
            _lastWeatherResult = null;
            lblThoiTietTitle.Text = LocalizationService.Get("WeatherTitleDefault");
            lblWeatherDesc.Text = "--";
            lblWeatherTemp.Text = LocalizationService.Get("WeatherTitleDefault");
            lblWeatherRain.Text = string.Empty;
            picThoiTiet.Image = WeatherIconHelper.CreateWeatherIcon(null, 84, 84);
            pnlWarning.Visible = false;
        }

        private void SelectTripById(int id)
        {
            foreach (DataGridViewRow row in dgvChuyenDi.Rows)
            {
                if (row.DataBoundItem is ChuyenDi trip && trip.MaChuyen == id)
                {
                    row.Selected = true;
                    dgvChuyenDi.CurrentCell = row.Cells[0];
                    break;
                }
            }
        }

        #endregion

        #region Tích hợp API Thời tiết & Hiển thị kết quả (Task 5, 6, 7)

        private async void btnXemThoiTiet_Click(object sender, EventArgs e)
        {
            string diaDiem = txtDiaDiem.Text.Trim();
            if (string.IsNullOrEmpty(diaDiem))
            {
                MessageBox.Show(LocalizationService.Get("MsgEnterDestination"), LocalizationService.Get("MsgNotification"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaDiem.Focus();
                return;
            }

            DateOnly ngayDi = DateOnly.FromDateTime(dtpNgayDi.Value.Date);
            await FetchAndDisplayWeatherAsync(diaDiem, ngayDi, isUserManualClick: true);
        }

        private async Task FetchAndDisplayWeatherAsync(string diaDiem, DateOnly ngayDi, bool isUserManualClick = false)
        {
            if (string.IsNullOrWhiteSpace(diaDiem)) return;

            int currentRequestId = ++_weatherRequestId;

            // Khóa nút bấm và hiện hiệu ứng tải (Marquee)
            btnXemThoiTiet.Enabled = false;
            pbLoading.Visible = true;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                var result = await WeatherService.GetForecastAsync(diaDiem, ngayDi);

                // Nếu người dùng đã chuyển sang dòng khác trong lúc tải thì bỏ qua kết quả cũ
                if (currentRequestId != _weatherRequestId) return;

                _lastWeatherResult = result;
                UpdateWeatherDisplay();

                // Chỉ hiển thị Popup MessageBox khi người dùng chủ động bấm nút "Xem thời tiết"
                if (!result.Success && isUserManualClick)
                {
                    string msg = result.ErrorMessage ?? "Lỗi không xác định";
                    if (!string.IsNullOrEmpty(result.Suggestion))
                    {
                        msg += "\n" + result.Suggestion;
                    }

                    MessageBox.Show(msg, LocalizationService.Get("MsgNotification"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                if (currentRequestId == _weatherRequestId && isUserManualClick)
                {
                    MessageBox.Show($"{LocalizationService.Get("MsgError")}: {ex.Message}", LocalizationService.Get("MsgError"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
                if (currentRequestId == _weatherRequestId)
                {
                    btnXemThoiTiet.Enabled = true;
                    pbLoading.Visible = false;
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void UpdateWeatherDisplay()
        {
            if (_lastWeatherResult == null) return;

            var r = _lastWeatherResult;
            string dateStr = LocalizationService.FormatDate(r.TargetDate);
            lblThoiTietTitle.Text = string.Format(LocalizationService.Get("WeatherTitleFormat"), dateStr, r.LocationName);

            if (!r.Success)
            {
                lblWeatherDesc.Text = LocalizationService.Get("MsgError");
                lblWeatherTemp.Text = r.ErrorMessage ?? string.Empty;
                lblWeatherRain.Text = string.Empty;
                picThoiTiet.Image = WeatherIconHelper.CreateWeatherIcon(null, 84, 84);
                pnlWarning.Visible = false;
                return;
            }

            if (r.OutOfRange)
            {
                lblWeatherDesc.Text = LocalizationService.CurrentLanguage == "en" ? "No Data" : "Chưa có dữ liệu";
                lblWeatherTemp.Text = LocalizationService.Get("WeatherOutOfRange");
                lblWeatherRain.Text = string.Empty;
                picThoiTiet.Image = WeatherIconHelper.CreateWeatherIcon(null, 84, 84);
                pnlWarning.Visible = false;
                return;
            }

            // Hiển thị đầy đủ thông tin thời tiết
            lblWeatherDesc.Text = r.GetDescription(LocalizationService.CurrentLanguage);
            lblWeatherTemp.Text = string.Format(LocalizationService.Get("WeatherTempFormat"), r.TempMin?.ToString("0.#"), r.TempMax?.ToString("0.#"));
            lblWeatherRain.Text = string.Format(LocalizationService.Get("WeatherRainFormat"), r.PrecipProbability ?? 0);

            // Icon thời tiết theo weather_code
            picThoiTiet.Image = WeatherIconHelper.CreateWeatherIcon(r.WeatherCode, 84, 84);

            // Cảnh báo mưa (xuất hiện khi khả năng mưa >= 60%)
            if (r.ShouldWarnRain)
            {
                pnlWarning.Visible = true;
                pnlWarning.BackColor = Color.FromArgb(255, 237, 213); // Cam nhạt
                lblWarningIcon.BackColor = Color.FromArgb(234, 88, 12); // Cam đậm
                lblWarningIcon.Text = "!";
                lblWarningText.ForeColor = Color.FromArgb(154, 52, 18);
                lblWarningText.Text = LocalizationService.Get("WeatherRainWarning");
            }
            else
            {
                pnlWarning.Visible = false;
            }
        }

        #endregion
    }
}
