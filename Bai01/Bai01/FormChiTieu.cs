using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Bai_01.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Bai_01
{
    public partial class FormChiTieu : Form
    {
        private List<ChiTieu> _danhSachHienTai = new();

        public FormChiTieu()
        {
            InitializeComponent();
        }

        private void FormChiTieu_Load(object sender, EventArgs e)
        {
            // Thiết lập giá trị mặc định cho form nhập liệu
            cboLoaiChi.SelectedIndex = 0;
            dtpNgayChi.Value = DateTime.Today;

            // Thiết lập giá trị mặc định cho bộ lọc
            cboTimLoaiChi.SelectedIndex = 0; // "Tất cả"

            // Mặc định lọc từ đầu tháng hiện tại đến ngày hôm nay
            var now = DateTime.Today;
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgay.Value = now;

            // Tải dữ liệu ban đầu
            LoadData();
        }

        /// <summary>
        /// Tải toàn bộ dữ liệu từ CSDL lên DataGridView
        /// </summary>
        private void LoadData()
        {
            try
            {
                using var context = new QL_ChiTieuContext();
                _danhSachHienTai = context.ChiTieus
                    .AsNoTracking()
                    .OrderByDescending(c => c.NgayChi)
                    .ThenByDescending(c => c.MaCt)
                    .ToList();

                HienThiDanhSach(_danhSachHienTai);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi truy cập cơ sở dữ liệu khi tải danh sách chi tiêu.");
                MessageBox.Show(
                    "Không thể kết nối hoặc truy cập cơ sở dữ liệu.\nVui lòng kiểm tra lại cấu hình kết nối trong file appsettings.json!",
                    "Lỗi cơ sở dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Gán danh sách lên DataGridView và tự động cập nhật tổng chi
        /// </summary>
        private void HienThiDanhSach(List<ChiTieu> danhSach)
        {
            dgvChiTieu.DataSource = null;
            dgvChiTieu.DataSource = danhSach;

            // Tính tổng tiền các dòng đang hiển thị
            decimal tongTien = danhSach.Sum(x => x.SoTien);
            lblTongChi.Text = $"Tổng chi: {tongTien:N0} đ";

            // Xóa chọn dòng ban đầu
            dgvChiTieu.ClearSelection();
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của dữ liệu đầu vào theo yêu cầu Task 3
        /// </summary>
        private bool KiemTraHopLe()
        {
            if (string.IsNullOrWhiteSpace(txtNoiDung.Text))
            {
                MessageBox.Show("Nội dung chi tiêu không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNoiDung.Focus();
                return false;
            }

            if (nudSoTien.Value <= 0)
            {
                MessageBox.Show("Số tiền chi tiêu phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudSoTien.Focus();
                return false;
            }

            if (dtpNgayChi.Value.Date > DateTime.Today)
            {
                MessageBox.Show("Ngày chi không được lớn hơn ngày hôm nay!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayChi.Focus();
                return false;
            }

            if (cboLoaiChi.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại chi!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiChi.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Xử lý Thêm khoản chi mới
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraHopLe()) return;

            try
            {
                var chiTieu = new ChiTieu
                {
                    NgayChi = DateOnly.FromDateTime(dtpNgayChi.Value.Date),
                    NoiDung = txtNoiDung.Text.Trim(),
                    LoaiChi = cboLoaiChi.SelectedItem?.ToString() ?? "Khác",
                    SoTien = nudSoTien.Value,
                    GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim()
                };

                using (var context = new QL_ChiTieuContext())
                {
                    context.ChiTieus.Add(chiTieu);
                    context.SaveChanges();
                }

                // Ghi log Information kèm MaCT
                Log.Information("Thêm khoản chi mới thành công. MaCT: {MaCT}, Nội dung: {NoiDung}, Loại chi: {LoaiChi}, Số tiền: {SoTien:N0}",
                    chiTieu.MaCt, chiTieu.NoiDung, chiTieu.LoaiChi, chiTieu.SoTien);

                MessageBox.Show("Thêm khoản chi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LamMoiForm();
                LoadData();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi truy cập cơ sở dữ liệu khi Thêm khoản chi mới.");
                MessageBox.Show(
                    "Đã xảy ra lỗi khi lưu vào cơ sở dữ liệu. Vui lòng kiểm tra lại kết nối!",
                    "Lỗi cơ sở dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Xử lý Sửa khoản chi đã chọn
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaCT.Text) || !int.TryParse(txtMaCT.Text, out int maCT))
            {
                MessageBox.Show("Vui lòng chọn một dòng khoản chi trên danh sách để sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!KiemTraHopLe()) return;

            try
            {
                using var context = new QL_ChiTieuContext();
                var chiTieu = context.ChiTieus.Find(maCT);
                if (chiTieu == null)
                {
                    MessageBox.Show("Khoản chi không tồn tại hoặc đã bị xóa trước đó!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadData();
                    return;
                }

                chiTieu.NgayChi = DateOnly.FromDateTime(dtpNgayChi.Value.Date);
                chiTieu.NoiDung = txtNoiDung.Text.Trim();
                chiTieu.LoaiChi = cboLoaiChi.SelectedItem?.ToString() ?? "Khác";
                chiTieu.SoTien = nudSoTien.Value;
                chiTieu.GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim();

                context.SaveChanges();

                // Ghi log Information kèm MaCT
                Log.Information("Cập nhật khoản chi thành công. MaCT: {MaCT}, Nội dung: {NoiDung}, Loại chi: {LoaiChi}, Số tiền: {SoTien:N0}",
                    chiTieu.MaCt, chiTieu.NoiDung, chiTieu.LoaiChi, chiTieu.SoTien);

                MessageBox.Show("Cập nhật khoản chi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LamMoiForm();
                LoadData();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi truy cập cơ sở dữ liệu khi Sửa khoản chi. MaCT: {MaCT}", maCT);
                MessageBox.Show(
                    "Đã xảy ra lỗi khi cập nhật vào cơ sở dữ liệu. Vui lòng kiểm tra lại kết nối!",
                    "Lỗi cơ sở dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Xử lý Xóa khoản chi (có hỏi xác nhận)
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaCT.Text) || !int.TryParse(txtMaCT.Text, out int maCT))
            {
                MessageBox.Show("Vui lòng chọn một dòng khoản chi trên danh sách để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa khoản chi '{txtNoiDung.Text}' (Mã: {maCT}) không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult != DialogResult.Yes) return;

            try
            {
                using var context = new QL_ChiTieuContext();
                var chiTieu = context.ChiTieus.Find(maCT);
                if (chiTieu != null)
                {
                    context.ChiTieus.Remove(chiTieu);
                    context.SaveChanges();

                    // Ghi log Information kèm MaCT
                    Log.Information("Xóa khoản chi thành công. MaCT: {MaCT}, Nội dung: {NoiDung}", maCT, chiTieu.NoiDung);

                    MessageBox.Show("Xóa khoản chi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Khoản chi không tồn tại trong hệ thống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                LamMoiForm();
                LoadData();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi truy cập cơ sở dữ liệu khi Xóa khoản chi. MaCT: {MaCT}", maCT);
                MessageBox.Show(
                    "Đã xảy ra lỗi khi xóa trong cơ sở dữ liệu. Vui lòng kiểm tra lại kết nối!",
                    "Lỗi cơ sở dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Nút Làm mới form nhập liệu
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoiForm();
        }

        private void LamMoiForm()
        {
            txtMaCT.Clear();
            dtpNgayChi.Value = DateTime.Today;
            txtNoiDung.Clear();
            cboLoaiChi.SelectedIndex = 0;
            nudSoTien.Value = 0;
            txtGhiChu.Clear();
            dgvChiTieu.ClearSelection();
            txtNoiDung.Focus();
        }

        /// <summary>
        /// Click dòng trên DataGridView để hiển thị dữ liệu lên form nhập liệu
        /// </summary>
        private void dgvChiTieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvChiTieu.Rows.Count) return;

            var row = dgvChiTieu.Rows[e.RowIndex];
            if (row.DataBoundItem is ChiTieu chiTieu)
            {
                txtMaCT.Text = chiTieu.MaCt.ToString();
                dtpNgayChi.Value = chiTieu.NgayChi.ToDateTime(TimeOnly.MinValue);
                txtNoiDung.Text = chiTieu.NoiDung;

                // Chọn loại chi tương ứng trong ComboBox
                int index = cboLoaiChi.FindStringExact(chiTieu.LoaiChi);
                if (index >= 0)
                {
                    cboLoaiChi.SelectedIndex = index;
                }
                else
                {
                    cboLoaiChi.Text = chiTieu.LoaiChi;
                }

                nudSoTien.Value = Math.Min(nudSoTien.Maximum, Math.Max(nudSoTien.Minimum, chiTieu.SoTien));
                txtGhiChu.Text = chiTieu.GhiChu ?? string.Empty;
            }
        }

        /// <summary>
        /// Xử lý Tìm kiếm và Lọc theo điều kiện
        /// </summary>
        private void btnTim_Click(object sender, EventArgs e)
        {
            ThucHienLoc();
        }

        private void ThucHienLoc()
        {
            try
            {
                var tuNgay = DateOnly.FromDateTime(dtpTuNgay.Value.Date);
                var denNgay = DateOnly.FromDateTime(dtpDenNgay.Value.Date);

                if (tuNgay > denNgay)
                {
                    MessageBox.Show("'Từ ngày' không được lớn hơn 'Đến ngày'!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string tuKhoa = txtTimNoiDung.Text.Trim();
                string loaiChi = cboTimLoaiChi.SelectedItem?.ToString() ?? "Tất cả";

                using var context = new QL_ChiTieuContext();
                var query = context.ChiTieus.AsNoTracking().AsQueryable();

                // Lọc theo khoảng thời gian
                query = query.Where(c => c.NgayChi >= tuNgay && c.NgayChi <= denNgay);

                // Lọc theo loại chi
                if (!string.IsNullOrEmpty(loaiChi) && loaiChi != "Tất cả")
                {
                    query = query.Where(c => c.LoaiChi == loaiChi);
                }

                // Lọc theo nội dung
                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    query = query.Where(c => c.NoiDung.Contains(tuKhoa));
                }

                var ketQua = query
                    .OrderByDescending(c => c.NgayChi)
                    .ThenByDescending(c => c.MaCt)
                    .ToList();

                HienThiDanhSach(ketQua);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Lỗi truy cập cơ sở dữ liệu khi tìm kiếm/lọc khoản chi.");
                MessageBox.Show(
                    "Đã xảy ra lỗi khi tìm kiếm trong cơ sở dữ liệu. Vui lòng kiểm tra lại kết nối!",
                    "Lỗi cơ sở dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Đặt lại bộ lọc và hiển thị tất cả
        /// </summary>
        private void btnDatLaiBoLoc_Click(object sender, EventArgs e)
        {
            txtTimNoiDung.Clear();
            cboTimLoaiChi.SelectedIndex = 0;

            var now = DateTime.Today;
            dtpTuNgay.Value = new DateTime(now.Year, 1, 1);
            dtpDenNgay.Value = now;

            LoadData();
        }
    }
}
