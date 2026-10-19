namespace Bai03
{
    partial class FormChuyenDi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.grpTripInfo = new System.Windows.Forms.GroupBox();
            this.lblTripName = new System.Windows.Forms.Label();
            this.txtTenChuyen = new System.Windows.Forms.TextBox();
            this.lblDestination = new System.Windows.Forms.Label();
            this.txtDiaDiem = new System.Windows.Forms.TextBox();
            this.lblDepartureDate = new System.Windows.Forms.Label();
            this.dtpNgayDi = new System.Windows.Forms.DateTimePicker();
            this.lblPeopleCount = new System.Windows.Forms.Label();
            this.nudSoNguoi = new System.Windows.Forms.NumericUpDown();
            this.lblBudget = new System.Windows.Forms.Label();
            this.nudNganSach = new System.Windows.Forms.NumericUpDown();
            this.lblVehicle = new System.Windows.Forms.Label();
            this.cboPhuongTien = new System.Windows.Forms.ComboBox();
            this.chkDaHoanThanh = new System.Windows.Forms.CheckBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXemThoiTiet = new System.Windows.Forms.Button();
            this.pbLoading = new System.Windows.Forms.ProgressBar();
            this.pnlThoiTiet = new System.Windows.Forms.Panel();
            this.lblThoiTietTitle = new System.Windows.Forms.Label();
            this.picThoiTiet = new System.Windows.Forms.PictureBox();
            this.lblWeatherDesc = new System.Windows.Forms.Label();
            this.lblWeatherTemp = new System.Windows.Forms.Label();
            this.lblWeatherRain = new System.Windows.Forms.Label();
            this.pnlWarning = new System.Windows.Forms.Panel();
            this.lblWarningIcon = new System.Windows.Forms.Label();
            this.lblWarningText = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.chkLocChuaHoanThanh = new System.Windows.Forms.CheckBox();
            this.chkLocNgay = new System.Windows.Forms.CheckBox();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.btnTim = new System.Windows.Forms.Button();
            this.btnTatCa = new System.Windows.Forms.Button();
            this.dgvChuyenDi = new System.Windows.Forms.DataGridView();
            this.colMaChuyen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenChuyen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiaDiem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayDi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoNguoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNganSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhuongTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDaHoanThanh = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.grpTripInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoNguoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNganSach)).BeginInit();
            this.pnlThoiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picThoiTiet)).BeginInit();
            this.pnlWarning.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChuyenDi)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblHeaderTitle.Location = new System.Drawing.Point(18, 14);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(326, 30);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "Kế hoạch dã ngoại cuối tuần";
            // 
            // lblLanguage
            // 
            this.lblLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblLanguage.Location = new System.Drawing.Point(798, 20);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(72, 17);
            this.lblLanguage.TabIndex = 1;
            this.lblLanguage.Text = "Ngôn ngữ :";
            // 
            // cboLanguage
            // 
            this.cboLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanguage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Items.AddRange(new object[] {
            "Tiếng Việt",
            "English"});
            this.cboLanguage.Location = new System.Drawing.Point(876, 16);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(130, 25);
            this.cboLanguage.TabIndex = 2;
            this.cboLanguage.SelectedIndexChanged += new System.EventHandler(this.cboLanguage_SelectedIndexChanged);
            // 
            // grpTripInfo
            // 
            this.grpTripInfo.Controls.Add(this.lblTripName);
            this.grpTripInfo.Controls.Add(this.txtTenChuyen);
            this.grpTripInfo.Controls.Add(this.lblDestination);
            this.grpTripInfo.Controls.Add(this.txtDiaDiem);
            this.grpTripInfo.Controls.Add(this.lblDepartureDate);
            this.grpTripInfo.Controls.Add(this.dtpNgayDi);
            this.grpTripInfo.Controls.Add(this.lblPeopleCount);
            this.grpTripInfo.Controls.Add(this.nudSoNguoi);
            this.grpTripInfo.Controls.Add(this.lblBudget);
            this.grpTripInfo.Controls.Add(this.nudNganSach);
            this.grpTripInfo.Controls.Add(this.lblVehicle);
            this.grpTripInfo.Controls.Add(this.cboPhuongTien);
            this.grpTripInfo.Controls.Add(this.chkDaHoanThanh);
            this.grpTripInfo.Controls.Add(this.btnThem);
            this.grpTripInfo.Controls.Add(this.btnSua);
            this.grpTripInfo.Controls.Add(this.btnXoa);
            this.grpTripInfo.Controls.Add(this.btnLamMoi);
            this.grpTripInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.grpTripInfo.Location = new System.Drawing.Point(20, 55);
            this.grpTripInfo.Name = "grpTripInfo";
            this.grpTripInfo.Size = new System.Drawing.Size(535, 275);
            this.grpTripInfo.TabIndex = 3;
            this.grpTripInfo.TabStop = false;
            this.grpTripInfo.Text = "Thông tin chuyến đi";
            // 
            // lblTripName
            // 
            this.lblTripName.AutoSize = true;
            this.lblTripName.Location = new System.Drawing.Point(15, 27);
            this.lblTripName.Name = "lblTripName";
            this.lblTripName.Size = new System.Drawing.Size(77, 17);
            this.lblTripName.TabIndex = 0;
            this.lblTripName.Text = "Tên chuyến :";
            // 
            // txtTenChuyen
            // 
            this.txtTenChuyen.Location = new System.Drawing.Point(115, 24);
            this.txtTenChuyen.Name = "txtTenChuyen";
            this.txtTenChuyen.Size = new System.Drawing.Size(400, 25);
            this.txtTenChuyen.TabIndex = 1;
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.Location = new System.Drawing.Point(15, 59);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(68, 17);
            this.lblDestination.TabIndex = 2;
            this.lblDestination.Text = "Địa điểm :";
            // 
            // txtDiaDiem
            // 
            this.txtDiaDiem.Location = new System.Drawing.Point(115, 56);
            this.txtDiaDiem.Name = "txtDiaDiem";
            this.txtDiaDiem.Size = new System.Drawing.Size(400, 25);
            this.txtDiaDiem.TabIndex = 3;
            // 
            // lblDepartureDate
            // 
            this.lblDepartureDate.AutoSize = true;
            this.lblDepartureDate.Location = new System.Drawing.Point(15, 93);
            this.lblDepartureDate.Name = "lblDepartureDate";
            this.lblDepartureDate.Size = new System.Drawing.Size(61, 17);
            this.lblDepartureDate.TabIndex = 4;
            this.lblDepartureDate.Text = "Ngày đi :";
            // 
            // dtpNgayDi
            // 
            this.dtpNgayDi.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayDi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayDi.Location = new System.Drawing.Point(115, 88);
            this.dtpNgayDi.Name = "dtpNgayDi";
            this.dtpNgayDi.Size = new System.Drawing.Size(140, 25);
            this.dtpNgayDi.TabIndex = 5;
            // 
            // lblPeopleCount
            // 
            this.lblPeopleCount.AutoSize = true;
            this.lblPeopleCount.Location = new System.Drawing.Point(15, 125);
            this.lblPeopleCount.Name = "lblPeopleCount";
            this.lblPeopleCount.Size = new System.Drawing.Size(68, 17);
            this.lblPeopleCount.TabIndex = 6;
            this.lblPeopleCount.Text = "Số người :";
            // 
            // nudSoNguoi
            // 
            this.nudSoNguoi.Location = new System.Drawing.Point(115, 122);
            this.nudSoNguoi.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSoNguoi.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoNguoi.Name = "nudSoNguoi";
            this.nudSoNguoi.Size = new System.Drawing.Size(140, 25);
            this.nudSoNguoi.TabIndex = 7;
            this.nudSoNguoi.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblBudget
            // 
            this.lblBudget.AutoSize = true;
            this.lblBudget.Location = new System.Drawing.Point(15, 157);
            this.lblBudget.Name = "lblBudget";
            this.lblBudget.Size = new System.Drawing.Size(76, 17);
            this.lblBudget.TabIndex = 8;
            this.lblBudget.Text = "Ngân sách :";
            // 
            // nudNganSach
            // 
            this.nudNganSach.Increment = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudNganSach.Location = new System.Drawing.Point(115, 155);
            this.nudNganSach.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudNganSach.Name = "nudNganSach";
            this.nudNganSach.Size = new System.Drawing.Size(140, 25);
            this.nudNganSach.TabIndex = 9;
            this.nudNganSach.ThousandsSeparator = true;
            // 
            // lblVehicle
            // 
            this.lblVehicle.AutoSize = true;
            this.lblVehicle.Location = new System.Drawing.Point(15, 191);
            this.lblVehicle.Name = "lblVehicle";
            this.lblVehicle.Size = new System.Drawing.Size(89, 17);
            this.lblVehicle.TabIndex = 10;
            this.lblVehicle.Text = "Phương tiện :";
            // 
            // cboPhuongTien
            // 
            this.cboPhuongTien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhuongTien.FormattingEnabled = true;
            this.cboPhuongTien.Items.AddRange(new object[] {
            "Xe máy",
            "Ô tô",
            "Xe khách",
            "Tàu hỏa"});
            this.cboPhuongTien.Location = new System.Drawing.Point(115, 187);
            this.cboPhuongTien.Name = "cboPhuongTien";
            this.cboPhuongTien.Size = new System.Drawing.Size(140, 25);
            this.cboPhuongTien.TabIndex = 11;
            // 
            // chkDaHoanThanh
            // 
            this.chkDaHoanThanh.AutoSize = true;
            this.chkDaHoanThanh.Location = new System.Drawing.Point(115, 218);
            this.chkDaHoanThanh.Name = "chkDaHoanThanh";
            this.chkDaHoanThanh.Size = new System.Drawing.Size(110, 21);
            this.chkDaHoanThanh.TabIndex = 12;
            this.chkDaHoanThanh.Text = "Đã hoàn thành";
            this.chkDaHoanThanh.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.btnThem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnThem.Location = new System.Drawing.Point(15, 243);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(115, 28);
            this.btnThem.TabIndex = 13;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.btnSua.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSua.Location = new System.Drawing.Point(142, 243);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(115, 28);
            this.btnSua.TabIndex = 14;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.btnXoa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnXoa.Location = new System.Drawing.Point(270, 243);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(115, 28);
            this.btnXoa.TabIndex = 15;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.btnLamMoi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnLamMoi.Location = new System.Drawing.Point(400, 243);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(115, 28);
            this.btnLamMoi.TabIndex = 16;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXemThoiTiet
            // 
            this.btnXemThoiTiet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXemThoiTiet.BackColor = System.Drawing.Color.White;
            this.btnXemThoiTiet.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.btnXemThoiTiet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemThoiTiet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnXemThoiTiet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(105)))), ((int)(((byte)(161)))));
            this.btnXemThoiTiet.Location = new System.Drawing.Point(575, 62);
            this.btnXemThoiTiet.Name = "btnXemThoiTiet";
            this.btnXemThoiTiet.Size = new System.Drawing.Size(160, 34);
            this.btnXemThoiTiet.TabIndex = 4;
            this.btnXemThoiTiet.Text = "Xem thời tiết";
            this.btnXemThoiTiet.UseVisualStyleBackColor = false;
            this.btnXemThoiTiet.Click += new System.EventHandler(this.btnXemThoiTiet_Click);
            // 
            // pbLoading
            // 
            this.pbLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLoading.Location = new System.Drawing.Point(745, 73);
            this.pbLoading.MarqueeAnimationSpeed = 30;
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(261, 12);
            this.pbLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbLoading.TabIndex = 5;
            this.pbLoading.Visible = false;
            // 
            // pnlThoiTiet
            // 
            this.pnlThoiTiet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlThoiTiet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnlThoiTiet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlThoiTiet.Controls.Add(this.lblThoiTietTitle);
            this.pnlThoiTiet.Controls.Add(this.picThoiTiet);
            this.pnlThoiTiet.Controls.Add(this.lblWeatherDesc);
            this.pnlThoiTiet.Controls.Add(this.lblWeatherTemp);
            this.pnlThoiTiet.Controls.Add(this.lblWeatherRain);
            this.pnlThoiTiet.Controls.Add(this.pnlWarning);
            this.pnlThoiTiet.Location = new System.Drawing.Point(575, 105);
            this.pnlThoiTiet.Name = "pnlThoiTiet";
            this.pnlThoiTiet.Size = new System.Drawing.Size(431, 225);
            this.pnlThoiTiet.TabIndex = 6;
            // 
            // lblThoiTietTitle
            // 
            this.lblThoiTietTitle.AutoSize = true;
            this.lblThoiTietTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblThoiTietTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblThoiTietTitle.Location = new System.Drawing.Point(12, 10);
            this.lblThoiTietTitle.Name = "lblThoiTietTitle";
            this.lblThoiTietTitle.Size = new System.Drawing.Size(107, 19);
            this.lblThoiTietTitle.TabIndex = 0;
            this.lblThoiTietTitle.Text = "Dự báo thời tiết";
            // 
            // picThoiTiet
            // 
            this.picThoiTiet.Location = new System.Drawing.Point(20, 42);
            this.picThoiTiet.Name = "picThoiTiet";
            this.picThoiTiet.Size = new System.Drawing.Size(84, 84);
            this.picThoiTiet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picThoiTiet.TabIndex = 1;
            this.picThoiTiet.TabStop = false;
            // 
            // lblWeatherDesc
            // 
            this.lblWeatherDesc.AutoSize = true;
            this.lblWeatherDesc.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblWeatherDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblWeatherDesc.Location = new System.Drawing.Point(125, 42);
            this.lblWeatherDesc.Name = "lblWeatherDesc";
            this.lblWeatherDesc.Size = new System.Drawing.Size(147, 30);
            this.lblWeatherDesc.TabIndex = 2;
            this.lblWeatherDesc.Text = "Mưa rào nhẹ";
            // 
            // lblWeatherTemp
            // 
            this.lblWeatherTemp.AutoSize = true;
            this.lblWeatherTemp.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblWeatherTemp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblWeatherTemp.Location = new System.Drawing.Point(126, 78);
            this.lblWeatherTemp.Name = "lblWeatherTemp";
            this.lblWeatherTemp.Size = new System.Drawing.Size(134, 19);
            this.lblWeatherTemp.TabIndex = 3;
            this.lblWeatherTemp.Text = "Nhiệt độ: 14°C - 22°C";
            // 
            // lblWeatherRain
            // 
            this.lblWeatherRain.AutoSize = true;
            this.lblWeatherRain.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblWeatherRain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblWeatherRain.Location = new System.Drawing.Point(126, 102);
            this.lblWeatherRain.Name = "lblWeatherRain";
            this.lblWeatherRain.Size = new System.Drawing.Size(126, 19);
            this.lblWeatherRain.TabIndex = 4;
            this.lblWeatherRain.Text = "Khả năng mưa: 75%";
            // 
            // pnlWarning
            // 
            this.pnlWarning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(213)))));
            this.pnlWarning.Controls.Add(this.lblWarningIcon);
            this.pnlWarning.Controls.Add(this.lblWarningText);
            this.pnlWarning.Location = new System.Drawing.Point(16, 150);
            this.pnlWarning.Name = "pnlWarning";
            this.pnlWarning.Size = new System.Drawing.Size(398, 56);
            this.pnlWarning.TabIndex = 5;
            // 
            // lblWarningIcon
            // 
            this.lblWarningIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.lblWarningIcon.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblWarningIcon.ForeColor = System.Drawing.Color.White;
            this.lblWarningIcon.Location = new System.Drawing.Point(12, 14);
            this.lblWarningIcon.Name = "lblWarningIcon";
            this.lblWarningIcon.Size = new System.Drawing.Size(26, 26);
            this.lblWarningIcon.TabIndex = 0;
            this.lblWarningIcon.Text = "!";
            this.lblWarningIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWarningText
            // 
            this.lblWarningText.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblWarningText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(52)))), ((int)(((byte)(18)))));
            this.lblWarningText.Location = new System.Drawing.Point(45, 8);
            this.lblWarningText.Name = "lblWarningText";
            this.lblWarningText.Size = new System.Drawing.Size(342, 40);
            this.lblWarningText.TabIndex = 1;
            this.lblWarningText.Text = "Nên mang áo mưa hoặc cân nhắc dời lịch";
            this.lblWarningText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.Controls.Add(this.txtTimKiem);
            this.pnlSearch.Controls.Add(this.chkLocChuaHoanThanh);
            this.pnlSearch.Controls.Add(this.chkLocNgay);
            this.pnlSearch.Controls.Add(this.dtpTuNgay);
            this.pnlSearch.Controls.Add(this.lblDenNgay);
            this.pnlSearch.Controls.Add(this.dtpDenNgay);
            this.pnlSearch.Controls.Add(this.btnTim);
            this.pnlSearch.Controls.Add(this.btnTatCa);
            this.pnlSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pnlSearch.Location = new System.Drawing.Point(20, 342);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(986, 38);
            this.pnlSearch.TabIndex = 7;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(3, 7);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Tìm địa điểm...";
            this.txtTimKiem.Size = new System.Drawing.Size(220, 25);
            this.txtTimKiem.TabIndex = 0;
            this.txtTimKiem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTimKiem_KeyDown);
            // 
            // chkLocChuaHoanThanh
            // 
            this.chkLocChuaHoanThanh.AutoSize = true;
            this.chkLocChuaHoanThanh.Location = new System.Drawing.Point(235, 9);
            this.chkLocChuaHoanThanh.Name = "chkLocChuaHoanThanh";
            this.chkLocChuaHoanThanh.Size = new System.Drawing.Size(226, 21);
            this.chkLocChuaHoanThanh.TabIndex = 1;
            this.chkLocChuaHoanThanh.Text = "Chỉ hiện chuyến chưa hoàn thành";
            this.chkLocChuaHoanThanh.UseVisualStyleBackColor = true;
            this.chkLocChuaHoanThanh.CheckedChanged += new System.EventHandler(this.FilterChanged);
            // 
            // chkLocNgay
            // 
            this.chkLocNgay.AutoSize = true;
            this.chkLocNgay.Location = new System.Drawing.Point(475, 9);
            this.chkLocNgay.Name = "chkLocNgay";
            this.chkLocNgay.Size = new System.Drawing.Size(46, 21);
            this.chkLocNgay.TabIndex = 2;
            this.chkLocNgay.Text = "Từ:";
            this.chkLocNgay.UseVisualStyleBackColor = true;
            this.chkLocNgay.CheckedChanged += new System.EventHandler(this.chkLocNgay_CheckedChanged);
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Enabled = false;
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(525, 7);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(105, 25);
            this.dtpTuNgay.TabIndex = 3;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Location = new System.Drawing.Point(636, 10);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(16, 17);
            this.lblDenNgay.TabIndex = 4;
            this.lblDenNgay.Text = "-";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Enabled = false;
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(656, 7);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(105, 25);
            this.dtpDenNgay.TabIndex = 5;
            // 
            // btnTim
            // 
            this.btnTim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.btnTim.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnTim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTim.Location = new System.Drawing.Point(778, 6);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(85, 27);
            this.btnTim.TabIndex = 6;
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = false;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // btnTatCa
            // 
            this.btnTatCa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.btnTatCa.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnTatCa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTatCa.Location = new System.Drawing.Point(875, 6);
            this.btnTatCa.Name = "btnTatCa";
            this.btnTatCa.Size = new System.Drawing.Size(85, 27);
            this.btnTatCa.TabIndex = 7;
            this.btnTatCa.Text = "Tất cả";
            this.btnTatCa.UseVisualStyleBackColor = false;
            this.btnTatCa.Click += new System.EventHandler(this.btnTatCa_Click);
            // 
            // dgvChuyenDi
            // 
            this.dgvChuyenDi.AllowUserToAddRows = false;
            this.dgvChuyenDi.AllowUserToDeleteRows = false;
            this.dgvChuyenDi.AllowUserToResizeRows = false;
            this.dgvChuyenDi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChuyenDi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChuyenDi.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvChuyenDi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvChuyenDi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChuyenDi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaChuyen,
            this.colTenChuyen,
            this.colDiaDiem,
            this.colNgayDi,
            this.colSoNguoi,
            this.colNganSach,
            this.colPhuongTien,
            this.colDaHoanThanh});
            this.dgvChuyenDi.EnableHeadersVisualStyles = false;
            this.dgvChuyenDi.Location = new System.Drawing.Point(20, 388);
            this.dgvChuyenDi.MultiSelect = false;
            this.dgvChuyenDi.Name = "dgvChuyenDi";
            this.dgvChuyenDi.ReadOnly = true;
            this.dgvChuyenDi.RowHeadersWidth = 35;
            this.dgvChuyenDi.RowTemplate.Height = 28;
            this.dgvChuyenDi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChuyenDi.Size = new System.Drawing.Size(986, 260);
            this.dgvChuyenDi.TabIndex = 8;
            this.dgvChuyenDi.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvChuyenDi_CellFormatting);
            this.dgvChuyenDi.SelectionChanged += new System.EventHandler(this.dgvChuyenDi_SelectionChanged);
            this.dgvChuyenDi.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChuyenDi_CellClick);
            // 
            // colMaChuyen
            // 
            this.colMaChuyen.DataPropertyName = "MaChuyen";
            this.colMaChuyen.FillWeight = 40F;
            this.colMaChuyen.HeaderText = "Mã";
            this.colMaChuyen.Name = "colMaChuyen";
            this.colMaChuyen.ReadOnly = true;
            // 
            // colTenChuyen
            // 
            this.colTenChuyen.DataPropertyName = "TenChuyen";
            this.colTenChuyen.FillWeight = 130F;
            this.colTenChuyen.HeaderText = "Tên chuyến";
            this.colTenChuyen.Name = "colTenChuyen";
            this.colTenChuyen.ReadOnly = true;
            // 
            // colDiaDiem
            // 
            this.colDiaDiem.DataPropertyName = "DiaDiem";
            this.colDiaDiem.FillWeight = 90F;
            this.colDiaDiem.HeaderText = "Địa điểm";
            this.colDiaDiem.Name = "colDiaDiem";
            this.colDiaDiem.ReadOnly = true;
            // 
            // colNgayDi
            // 
            this.colNgayDi.DataPropertyName = "NgayDi";
            this.colNgayDi.FillWeight = 75F;
            this.colNgayDi.HeaderText = "Ngày đi";
            this.colNgayDi.Name = "colNgayDi";
            this.colNgayDi.ReadOnly = true;
            // 
            // colSoNguoi
            // 
            this.colSoNguoi.DataPropertyName = "SoNguoi";
            this.colSoNguoi.FillWeight = 50F;
            this.colSoNguoi.HeaderText = "Số người";
            this.colSoNguoi.Name = "colSoNguoi";
            this.colSoNguoi.ReadOnly = true;
            // 
            // colNganSach
            // 
            this.colNganSach.DataPropertyName = "NganSach";
            this.colNganSach.FillWeight = 80F;
            this.colNganSach.HeaderText = "Ngân sách";
            this.colNganSach.Name = "colNganSach";
            this.colNganSach.ReadOnly = true;
            // 
            // colPhuongTien
            // 
            this.colPhuongTien.DataPropertyName = "PhuongTien";
            this.colPhuongTien.FillWeight = 75F;
            this.colPhuongTien.HeaderText = "Phương tiện";
            this.colPhuongTien.Name = "colPhuongTien";
            this.colPhuongTien.ReadOnly = true;
            // 
            // colDaHoanThanh
            // 
            this.colDaHoanThanh.DataPropertyName = "DaHoanThanh";
            this.colDaHoanThanh.FillWeight = 60F;
            this.colDaHoanThanh.HeaderText = "Hoàn thành";
            this.colDaHoanThanh.Name = "colDaHoanThanh";
            this.colDaHoanThanh.ReadOnly = true;
            // 
            // FormChuyenDi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1028, 664);
            this.Controls.Add(this.dgvChuyenDi);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlThoiTiet);
            this.Controls.Add(this.pbLoading);
            this.Controls.Add(this.btnXemThoiTiet);
            this.Controls.Add(this.grpTripInfo);
            this.Controls.Add(this.cboLanguage);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.lblHeaderTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinimumSize = new System.Drawing.Size(1040, 700);
            this.Name = "FormChuyenDi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kế hoạch dã ngoại cuối tuần";
            this.Load += new System.EventHandler(this.FormChuyenDi_Load);
            this.grpTripInfo.ResumeLayout(false);
            this.grpTripInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoNguoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNganSach)).EndInit();
            this.pnlThoiTiet.ResumeLayout(false);
            this.pnlThoiTiet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picThoiTiet)).EndInit();
            this.pnlWarning.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChuyenDi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.ComboBox cboLanguage;
        private System.Windows.Forms.GroupBox grpTripInfo;
        private System.Windows.Forms.Label lblTripName;
        private System.Windows.Forms.TextBox txtTenChuyen;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.TextBox txtDiaDiem;
        private System.Windows.Forms.Label lblDepartureDate;
        private System.Windows.Forms.DateTimePicker dtpNgayDi;
        private System.Windows.Forms.Label lblPeopleCount;
        private System.Windows.Forms.NumericUpDown nudSoNguoi;
        private System.Windows.Forms.Label lblBudget;
        private System.Windows.Forms.NumericUpDown nudNganSach;
        private System.Windows.Forms.Label lblVehicle;
        private System.Windows.Forms.ComboBox cboPhuongTien;
        private System.Windows.Forms.CheckBox chkDaHoanThanh;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXemThoiTiet;
        private System.Windows.Forms.ProgressBar pbLoading;
        private System.Windows.Forms.Panel pnlThoiTiet;
        private System.Windows.Forms.Label lblThoiTietTitle;
        private System.Windows.Forms.PictureBox picThoiTiet;
        private System.Windows.Forms.Label lblWeatherDesc;
        private System.Windows.Forms.Label lblWeatherTemp;
        private System.Windows.Forms.Label lblWeatherRain;
        private System.Windows.Forms.Panel pnlWarning;
        private System.Windows.Forms.Label lblWarningIcon;
        private System.Windows.Forms.Label lblWarningText;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.CheckBox chkLocChuaHoanThanh;
        private System.Windows.Forms.CheckBox chkLocNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.Button btnTatCa;
        private System.Windows.Forms.DataGridView dgvChuyenDi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaChuyen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenChuyen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiaDiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayDi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoNguoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNganSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhuongTien;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colDaHoanThanh;
    }
}
