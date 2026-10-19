#nullable enable

namespace QL_NhaTro;

public sealed partial class FormKhachThue
{
    private System.ComponentModel.IContainer? components;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            components?.Dispose();

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        rootLayout = new TableLayoutPanel();
        leftPanel = new Panel();
        entryGroup = new GroupBox();
        fieldsTable = new TableLayoutPanel();
        nameField = new TableLayoutPanel();
        nameCaption = new Label();
        _nameBox = new TextBox();
        genderGroup = new GroupBox();
        genderFlow = new FlowLayoutPanel();
        _maleRadio = new RadioButton();
        _femaleRadio = new RadioButton();
        birthField = new TableLayoutPanel();
        birthCaption = new Label();
        _birthPicker = new DateTimePicker();
        phoneField = new TableLayoutPanel();
        phoneCaption = new Label();
        _phoneBox = new TextBox();
        roomField = new TableLayoutPanel();
        roomCaption = new Label();
        _roomCombo = new ComboBox();
        provinceField = new TableLayoutPanel();
        provinceCaption = new Label();
        _provinceCombo = new ComboBox();
        wardField = new TableLayoutPanel();
        wardCaption = new Label();
        _wardCombo = new ComboBox();
        _registeredCheck = new CheckBox();
        buttonFlow = new FlowLayoutPanel();
        _addButton = new Button();
        _editButton = new Button();
        _deleteButton = new Button();
        _clearButton = new Button();
        rightPanel = new TableLayoutPanel();
        searchFlow = new FlowLayoutPanel();
        _searchBox = new TextBox();
        roomFilterCaption = new Label();
        _roomFilter = new ComboBox();
        provinceFilterCaption = new Label();
        _provinceFilter = new ComboBox();
        _findButton = new Button();
        _clearFilterButton = new Button();
        _grid = new DataGridView();
        statusPanel = new Panel();
        _statusLabel = new Label();
        _testErrorButton = new Button();
        rootLayout.SuspendLayout();
        leftPanel.SuspendLayout();
        entryGroup.SuspendLayout();
        fieldsTable.SuspendLayout();
        nameField.SuspendLayout();
        genderGroup.SuspendLayout();
        genderFlow.SuspendLayout();
        birthField.SuspendLayout();
        phoneField.SuspendLayout();
        roomField.SuspendLayout();
        provinceField.SuspendLayout();
        wardField.SuspendLayout();
        buttonFlow.SuspendLayout();
        rightPanel.SuspendLayout();
        searchFlow.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).BeginInit();
        statusPanel.SuspendLayout();
        SuspendLayout();
        // 
        // rootLayout
        // 
        rootLayout.BackColor = Color.FromArgb(246, 248, 251);
        rootLayout.ColumnCount = 2;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 365F));
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Controls.Add(leftPanel, 0, 0);
        rootLayout.Controls.Add(rightPanel, 1, 0);
        rootLayout.Controls.Add(statusPanel, 0, 1);
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Location = new Point(0, 0);
        rootLayout.Name = "rootLayout";
        rootLayout.Padding = new Padding(12);
        rootLayout.RowCount = 2;
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        rootLayout.Size = new Size(1234, 751);
        rootLayout.TabIndex = 0;
        // 
        // leftPanel
        // 
        leftPanel.Controls.Add(entryGroup);
        leftPanel.Dock = DockStyle.Fill;
        leftPanel.Location = new Point(15, 15);
        leftPanel.Name = "leftPanel";
        leftPanel.Padding = new Padding(0, 0, 10, 0);
        leftPanel.Size = new Size(359, 691);
        leftPanel.TabIndex = 0;
        // 
        // entryGroup
        // 
        entryGroup.BackColor = Color.White;
        entryGroup.Controls.Add(fieldsTable);
        entryGroup.Dock = DockStyle.Fill;
        entryGroup.Location = new Point(0, 0);
        entryGroup.Name = "entryGroup";
        entryGroup.Padding = new Padding(12);
        entryGroup.Size = new Size(349, 691);
        entryGroup.TabIndex = 0;
        entryGroup.TabStop = false;
        entryGroup.Text = "Thông tin khách thuê";
        // 
        // fieldsTable
        // 
        fieldsTable.ColumnCount = 1;
        fieldsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        fieldsTable.Controls.Add(nameField, 0, 0);
        fieldsTable.Controls.Add(genderGroup, 0, 1);
        fieldsTable.Controls.Add(birthField, 0, 2);
        fieldsTable.Controls.Add(phoneField, 0, 3);
        fieldsTable.Controls.Add(roomField, 0, 4);
        fieldsTable.Controls.Add(provinceField, 0, 5);
        fieldsTable.Controls.Add(wardField, 0, 6);
        fieldsTable.Controls.Add(_registeredCheck, 0, 7);
        fieldsTable.Controls.Add(buttonFlow, 0, 8);
        fieldsTable.Dock = DockStyle.Fill;
        fieldsTable.Location = new Point(12, 28);
        fieldsTable.Name = "fieldsTable";
        fieldsTable.Padding = new Padding(6, 8, 6, 4);
        fieldsTable.RowCount = 9;
        fieldsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
        fieldsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
        fieldsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
        fieldsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
        fieldsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
        fieldsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
        fieldsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
        fieldsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
        fieldsTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        fieldsTable.Size = new Size(325, 651);
        fieldsTable.TabIndex = 0;
        // 
        // nameField
        // 
        nameField.ColumnCount = 2;
        nameField.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        nameField.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        nameField.Controls.Add(nameCaption, 0, 0);
        nameField.Controls.Add(_nameBox, 1, 0);
        nameField.Dock = DockStyle.Fill;
        nameField.Location = new Point(9, 11);
        nameField.Name = "nameField";
        nameField.Padding = new Padding(0, 3, 0, 3);
        nameField.RowCount = 1;
        nameField.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        nameField.Size = new Size(307, 68);
        nameField.TabIndex = 0;
        // 
        // nameCaption
        // 
        nameCaption.Dock = DockStyle.Fill;
        nameCaption.Location = new Point(3, 3);
        nameCaption.Name = "nameCaption";
        nameCaption.Size = new Size(106, 62);
        nameCaption.TabIndex = 0;
        nameCaption.Text = "Họ tên";
        nameCaption.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _nameBox
        // 
        _nameBox.Dock = DockStyle.Fill;
        _nameBox.Font = new Font("Segoe UI", 9F);
        _nameBox.Location = new Point(112, 7);
        _nameBox.Margin = new Padding(0, 4, 0, 4);
        _nameBox.MaxLength = 100;
        _nameBox.Name = "_nameBox";
        _nameBox.Size = new Size(195, 23);
        _nameBox.TabIndex = 1;
        // 
        // genderGroup
        // 
        genderGroup.Controls.Add(genderFlow);
        genderGroup.Dock = DockStyle.Fill;
        genderGroup.Location = new Point(9, 85);
        genderGroup.Name = "genderGroup";
        genderGroup.Padding = new Padding(10, 4, 4, 4);
        genderGroup.Size = new Size(307, 68);
        genderGroup.TabIndex = 1;
        genderGroup.TabStop = false;
        genderGroup.Text = "Giới tính";
        // 
        // genderFlow
        // 
        genderFlow.Controls.Add(_maleRadio);
        genderFlow.Controls.Add(_femaleRadio);
        genderFlow.Dock = DockStyle.Fill;
        genderFlow.Location = new Point(10, 20);
        genderFlow.Name = "genderFlow";
        genderFlow.Size = new Size(293, 44);
        genderFlow.TabIndex = 0;
        genderFlow.WrapContents = false;
        // 
        // _maleRadio
        // 
        _maleRadio.AutoSize = true;
        _maleRadio.Checked = true;
        _maleRadio.Location = new Point(3, 3);
        _maleRadio.Name = "_maleRadio";
        _maleRadio.Size = new Size(51, 19);
        _maleRadio.TabIndex = 0;
        _maleRadio.TabStop = true;
        _maleRadio.Text = "Nam";
        // 
        // _femaleRadio
        // 
        _femaleRadio.AutoSize = true;
        _femaleRadio.Location = new Point(60, 3);
        _femaleRadio.Name = "_femaleRadio";
        _femaleRadio.Size = new Size(41, 19);
        _femaleRadio.TabIndex = 1;
        _femaleRadio.Text = "Nữ";
        // 
        // birthField
        // 
        birthField.ColumnCount = 2;
        birthField.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        birthField.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        birthField.Controls.Add(birthCaption, 0, 0);
        birthField.Controls.Add(_birthPicker, 1, 0);
        birthField.Dock = DockStyle.Fill;
        birthField.Location = new Point(9, 159);
        birthField.Name = "birthField";
        birthField.Padding = new Padding(0, 3, 0, 3);
        birthField.RowCount = 1;
        birthField.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        birthField.Size = new Size(307, 68);
        birthField.TabIndex = 2;
        // 
        // birthCaption
        // 
        birthCaption.Dock = DockStyle.Fill;
        birthCaption.Location = new Point(3, 3);
        birthCaption.Name = "birthCaption";
        birthCaption.Size = new Size(106, 62);
        birthCaption.TabIndex = 0;
        birthCaption.Text = "Ngày sinh";
        birthCaption.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _birthPicker
        // 
        _birthPicker.Dock = DockStyle.Fill;
        _birthPicker.Format = DateTimePickerFormat.Short;
        _birthPicker.Location = new Point(112, 7);
        _birthPicker.Margin = new Padding(0, 4, 0, 4);
        _birthPicker.Name = "_birthPicker";
        _birthPicker.Size = new Size(195, 23);
        _birthPicker.TabIndex = 1;
        // 
        // phoneField
        // 
        phoneField.ColumnCount = 2;
        phoneField.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        phoneField.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        phoneField.Controls.Add(phoneCaption, 0, 0);
        phoneField.Controls.Add(_phoneBox, 1, 0);
        phoneField.Dock = DockStyle.Fill;
        phoneField.Location = new Point(9, 233);
        phoneField.Name = "phoneField";
        phoneField.Padding = new Padding(0, 3, 0, 3);
        phoneField.RowCount = 1;
        phoneField.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        phoneField.Size = new Size(307, 68);
        phoneField.TabIndex = 3;
        // 
        // phoneCaption
        // 
        phoneCaption.Dock = DockStyle.Fill;
        phoneCaption.Location = new Point(3, 3);
        phoneCaption.Name = "phoneCaption";
        phoneCaption.Size = new Size(106, 62);
        phoneCaption.TabIndex = 0;
        phoneCaption.Text = "Số điện thoại";
        phoneCaption.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _phoneBox
        // 
        _phoneBox.Dock = DockStyle.Fill;
        _phoneBox.Font = new Font("Segoe UI", 9F);
        _phoneBox.Location = new Point(112, 7);
        _phoneBox.Margin = new Padding(0, 4, 0, 4);
        _phoneBox.MaxLength = 15;
        _phoneBox.Name = "_phoneBox";
        _phoneBox.Size = new Size(195, 23);
        _phoneBox.TabIndex = 1;
        // 
        // roomField
        // 
        roomField.ColumnCount = 2;
        roomField.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        roomField.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        roomField.Controls.Add(roomCaption, 0, 0);
        roomField.Controls.Add(_roomCombo, 1, 0);
        roomField.Dock = DockStyle.Fill;
        roomField.Location = new Point(9, 307);
        roomField.Name = "roomField";
        roomField.Padding = new Padding(0, 3, 0, 3);
        roomField.RowCount = 1;
        roomField.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        roomField.Size = new Size(307, 68);
        roomField.TabIndex = 4;
        // 
        // roomCaption
        // 
        roomCaption.Dock = DockStyle.Fill;
        roomCaption.Location = new Point(3, 3);
        roomCaption.Name = "roomCaption";
        roomCaption.Size = new Size(106, 62);
        roomCaption.TabIndex = 0;
        roomCaption.Text = "Phòng";
        roomCaption.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _roomCombo
        // 
        _roomCombo.Dock = DockStyle.Fill;
        _roomCombo.DropDownStyle = ComboBoxStyle.DropDownList;
        _roomCombo.Font = new Font("Segoe UI", 9F);
        _roomCombo.Location = new Point(112, 7);
        _roomCombo.Margin = new Padding(0, 4, 0, 4);
        _roomCombo.Name = "_roomCombo";
        _roomCombo.Size = new Size(195, 23);
        _roomCombo.TabIndex = 1;
        // 
        // provinceField
        // 
        provinceField.ColumnCount = 2;
        provinceField.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        provinceField.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        provinceField.Controls.Add(provinceCaption, 0, 0);
        provinceField.Controls.Add(_provinceCombo, 1, 0);
        provinceField.Dock = DockStyle.Fill;
        provinceField.Location = new Point(9, 381);
        provinceField.Name = "provinceField";
        provinceField.Padding = new Padding(0, 3, 0, 3);
        provinceField.RowCount = 1;
        provinceField.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        provinceField.Size = new Size(307, 68);
        provinceField.TabIndex = 5;
        // 
        // provinceCaption
        // 
        provinceCaption.Dock = DockStyle.Fill;
        provinceCaption.Location = new Point(3, 3);
        provinceCaption.Name = "provinceCaption";
        provinceCaption.Size = new Size(106, 62);
        provinceCaption.TabIndex = 0;
        provinceCaption.Text = "Tỉnh/Thành";
        provinceCaption.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _provinceCombo
        // 
        _provinceCombo.Dock = DockStyle.Fill;
        _provinceCombo.Font = new Font("Segoe UI", 9F);
        _provinceCombo.Location = new Point(112, 7);
        _provinceCombo.Margin = new Padding(0, 4, 0, 4);
        _provinceCombo.Name = "_provinceCombo";
        _provinceCombo.Size = new Size(195, 23);
        _provinceCombo.TabIndex = 1;
        // 
        // wardField
        // 
        wardField.ColumnCount = 2;
        wardField.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
        wardField.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        wardField.Controls.Add(wardCaption, 0, 0);
        wardField.Controls.Add(_wardCombo, 1, 0);
        wardField.Dock = DockStyle.Fill;
        wardField.Location = new Point(9, 455);
        wardField.Name = "wardField";
        wardField.Padding = new Padding(0, 3, 0, 3);
        wardField.RowCount = 1;
        wardField.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        wardField.Size = new Size(307, 68);
        wardField.TabIndex = 6;
        // 
        // wardCaption
        // 
        wardCaption.Dock = DockStyle.Fill;
        wardCaption.Location = new Point(3, 3);
        wardCaption.Name = "wardCaption";
        wardCaption.Size = new Size(106, 62);
        wardCaption.TabIndex = 0;
        wardCaption.Text = "Phường/Xã";
        wardCaption.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // _wardCombo
        // 
        _wardCombo.Dock = DockStyle.Fill;
        _wardCombo.Font = new Font("Segoe UI", 9F);
        _wardCombo.Location = new Point(112, 7);
        _wardCombo.Margin = new Padding(0, 4, 0, 4);
        _wardCombo.Name = "_wardCombo";
        _wardCombo.Size = new Size(195, 23);
        _wardCombo.TabIndex = 1;
        // 
        // _registeredCheck
        // 
        _registeredCheck.AutoSize = true;
        _registeredCheck.Location = new Point(9, 529);
        _registeredCheck.Name = "_registeredCheck";
        _registeredCheck.Size = new Size(127, 19);
        _registeredCheck.TabIndex = 7;
        _registeredCheck.Text = "Đã đăng ký tạm trú";
        // 
        // buttonFlow
        // 
        buttonFlow.Controls.Add(_addButton);
        buttonFlow.Controls.Add(_editButton);
        buttonFlow.Controls.Add(_deleteButton);
        buttonFlow.Controls.Add(_clearButton);
        buttonFlow.Dock = DockStyle.Fill;
        buttonFlow.Location = new Point(9, 603);
        buttonFlow.Name = "buttonFlow";
        buttonFlow.Padding = new Padding(0, 3, 0, 0);
        buttonFlow.Size = new Size(307, 41);
        buttonFlow.TabIndex = 8;
        buttonFlow.WrapContents = false;
        // 
        // _addButton
        // 
        _addButton.Location = new Point(3, 6);
        _addButton.Name = "_addButton";
        _addButton.Size = new Size(70, 34);
        _addButton.TabIndex = 0;
        _addButton.Text = "Thêm";
        // 
        // _editButton
        // 
        _editButton.Location = new Point(79, 6);
        _editButton.Name = "_editButton";
        _editButton.Size = new Size(70, 34);
        _editButton.TabIndex = 1;
        _editButton.Text = "Sửa";
        // 
        // _deleteButton
        // 
        _deleteButton.Location = new Point(155, 6);
        _deleteButton.Name = "_deleteButton";
        _deleteButton.Size = new Size(70, 34);
        _deleteButton.TabIndex = 2;
        _deleteButton.Text = "Xóa";
        // 
        // _clearButton
        // 
        _clearButton.Location = new Point(231, 6);
        _clearButton.Name = "_clearButton";
        _clearButton.Size = new Size(88, 34);
        _clearButton.TabIndex = 3;
        _clearButton.Text = "Làm mới";
        // 
        // rightPanel
        // 
        rightPanel.ColumnCount = 1;
        rightPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rightPanel.Controls.Add(searchFlow, 0, 0);
        rightPanel.Controls.Add(_grid, 0, 1);
        rightPanel.Dock = DockStyle.Fill;
        rightPanel.Location = new Point(380, 15);
        rightPanel.Name = "rightPanel";
        rightPanel.RowCount = 2;
        rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        rightPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        rightPanel.Size = new Size(839, 691);
        rightPanel.TabIndex = 1;
        // 
        // searchFlow
        // 
        searchFlow.BackColor = Color.FromArgb(246, 248, 251);
        searchFlow.Controls.Add(_searchBox);
        searchFlow.Controls.Add(roomFilterCaption);
        searchFlow.Controls.Add(_roomFilter);
        searchFlow.Controls.Add(provinceFilterCaption);
        searchFlow.Controls.Add(_provinceFilter);
        searchFlow.Controls.Add(_findButton);
        searchFlow.Controls.Add(_clearFilterButton);
        searchFlow.Dock = DockStyle.Fill;
        searchFlow.Location = new Point(3, 3);
        searchFlow.Name = "searchFlow";
        searchFlow.Padding = new Padding(0, 4, 0, 2);
        searchFlow.Size = new Size(833, 42);
        searchFlow.TabIndex = 0;
        searchFlow.WrapContents = false;
        // 
        // _searchBox
        // 
        _searchBox.Location = new Point(3, 7);
        _searchBox.Name = "_searchBox";
        _searchBox.PlaceholderText = "Tìm theo họ tên hoặc SĐT";
        _searchBox.Size = new Size(270, 23);
        _searchBox.TabIndex = 0;
        // 
        // roomFilterCaption
        // 
        roomFilterCaption.AutoSize = true;
        roomFilterCaption.Location = new Point(279, 4);
        roomFilterCaption.Name = "roomFilterCaption";
        roomFilterCaption.Padding = new Padding(0, 8, 0, 0);
        roomFilterCaption.Size = new Size(45, 23);
        roomFilterCaption.TabIndex = 1;
        roomFilterCaption.Text = "Phòng:";
        // 
        // _roomFilter
        // 
        _roomFilter.Location = new Point(330, 7);
        _roomFilter.Name = "_roomFilter";
        _roomFilter.Size = new Size(150, 23);
        _roomFilter.TabIndex = 2;
        // 
        // provinceFilterCaption
        // 
        provinceFilterCaption.AutoSize = true;
        provinceFilterCaption.Location = new Point(486, 4);
        provinceFilterCaption.Name = "provinceFilterCaption";
        provinceFilterCaption.Padding = new Padding(0, 8, 0, 0);
        provinceFilterCaption.Size = new Size(34, 23);
        provinceFilterCaption.TabIndex = 3;
        provinceFilterCaption.Text = "Tỉnh:";
        // 
        // _provinceFilter
        // 
        _provinceFilter.Location = new Point(526, 7);
        _provinceFilter.Name = "_provinceFilter";
        _provinceFilter.Size = new Size(165, 23);
        _provinceFilter.TabIndex = 4;
        // 
        // _findButton
        // 
        _findButton.Location = new Point(697, 7);
        _findButton.Name = "_findButton";
        _findButton.Size = new Size(70, 23);
        _findButton.TabIndex = 5;
        _findButton.Text = "Tìm";
        // 
        // _clearFilterButton
        // 
        _clearFilterButton.Location = new Point(773, 7);
        _clearFilterButton.Name = "_clearFilterButton";
        _clearFilterButton.Size = new Size(78, 23);
        _clearFilterButton.TabIndex = 6;
        _clearFilterButton.Text = "Bỏ lọc";
        // 
        // _grid
        // 
        _grid.AllowUserToAddRows = false;
        _grid.AllowUserToDeleteRows = false;
        _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _grid.BackgroundColor = Color.White;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(233, 239, 247);
        dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        _grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        _grid.Dock = DockStyle.Fill;
        _grid.EnableHeadersVisualStyles = false;
        _grid.Location = new Point(3, 51);
        _grid.MultiSelect = false;
        _grid.Name = "_grid";
        _grid.ReadOnly = true;
        _grid.RowHeadersWidth = 28;
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _grid.Size = new Size(833, 637);
        _grid.TabIndex = 1;
        // 
        // statusPanel
        // 
        rootLayout.SetColumnSpan(statusPanel, 2);
        statusPanel.Controls.Add(_statusLabel);
        statusPanel.Dock = DockStyle.Fill;
        statusPanel.Location = new Point(15, 712);
        statusPanel.Name = "statusPanel";
        statusPanel.Padding = new Padding(0, 5, 0, 0);
        statusPanel.Size = new Size(1204, 24);
        statusPanel.TabIndex = 2;
        // 
        // _statusLabel
        // 
        _statusLabel.AutoSize = true;
        _statusLabel.Location = new Point(0, 0);
        _statusLabel.Name = "_statusLabel";
        _statusLabel.Size = new Size(54, 15);
        _statusLabel.TabIndex = 0;
        _statusLabel.Text = "Sẵn sàng";
        // 
        // _testErrorButton
        // 
        _testErrorButton.Location = new Point(0, 0);
        _testErrorButton.Name = "_testErrorButton";
        _testErrorButton.Size = new Size(75, 23);
        _testErrorButton.TabIndex = 1;
        _testErrorButton.Tag = "Ctrl+Shift+E";
        _testErrorButton.Text = "Gây lỗi thử";
        _testErrorButton.Visible = false;
        // 
        // FormKhachThue
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(246, 248, 251);
        ClientSize = new Size(1234, 751);
        Controls.Add(rootLayout);
        Controls.Add(_testErrorButton);
        Font = new Font("Segoe UI", 9F);
        KeyPreview = true;
        MinimumSize = new Size(1050, 690);
        Name = "FormKhachThue";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Quản lý nhà trọ - Khách thuê";
        rootLayout.ResumeLayout(false);
        leftPanel.ResumeLayout(false);
        entryGroup.ResumeLayout(false);
        fieldsTable.ResumeLayout(false);
        fieldsTable.PerformLayout();
        nameField.ResumeLayout(false);
        nameField.PerformLayout();
        genderGroup.ResumeLayout(false);
        genderFlow.ResumeLayout(false);
        genderFlow.PerformLayout();
        birthField.ResumeLayout(false);
        phoneField.ResumeLayout(false);
        phoneField.PerformLayout();
        roomField.ResumeLayout(false);
        provinceField.ResumeLayout(false);
        wardField.ResumeLayout(false);
        buttonFlow.ResumeLayout(false);
        rightPanel.ResumeLayout(false);
        searchFlow.ResumeLayout(false);
        searchFlow.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_grid).EndInit();
        statusPanel.ResumeLayout(false);
        statusPanel.PerformLayout();
        ResumeLayout(false);
    }

    private TableLayoutPanel rootLayout = null!;
    private Panel leftPanel = null!;
    private GroupBox entryGroup = null!;
    private TableLayoutPanel fieldsTable = null!;
    private TableLayoutPanel nameField = null!;
    private Label nameCaption = null!;
    private GroupBox genderGroup = null!;
    private FlowLayoutPanel genderFlow = null!;
    private TableLayoutPanel birthField = null!;
    private Label birthCaption = null!;
    private TableLayoutPanel phoneField = null!;
    private Label phoneCaption = null!;
    private TableLayoutPanel roomField = null!;
    private Label roomCaption = null!;
    private TableLayoutPanel provinceField = null!;
    private Label provinceCaption = null!;
    private TableLayoutPanel wardField = null!;
    private Label wardCaption = null!;
    private FlowLayoutPanel buttonFlow = null!;
    private TableLayoutPanel rightPanel = null!;
    private FlowLayoutPanel searchFlow = null!;
    private Label roomFilterCaption = null!;
    private Label provinceFilterCaption = null!;
    private Panel statusPanel = null!;
    private TextBox _nameBox = null!;
    private RadioButton _maleRadio = null!;
    private RadioButton _femaleRadio = null!;
    private DateTimePicker _birthPicker = null!;
    private TextBox _phoneBox = null!;
    private ComboBox _roomCombo = null!;
    private ComboBox _provinceCombo = null!;
    private ComboBox _wardCombo = null!;
    private CheckBox _registeredCheck = null!;
    private TextBox _searchBox = null!;
    private ComboBox _roomFilter = null!;
    private ComboBox _provinceFilter = null!;
    private DataGridView _grid = null!;
    private DataGridViewTextBoxColumn columnMaKhach = null!;
    private DataGridViewTextBoxColumn columnHoTen = null!;
    private DataGridViewTextBoxColumn columnSoDienThoai = null!;
    private DataGridViewTextBoxColumn columnTenPhong = null!;
    private DataGridViewTextBoxColumn columnGiaThue = null!;
    private DataGridViewTextBoxColumn columnQueQuan = null!;
    private DataGridViewCheckBoxColumn columnTamTru = null!;
    private Label _statusLabel = null!;
    private Button _addButton = null!;
    private Button _editButton = null!;
    private Button _deleteButton = null!;
    private Button _clearButton = null!;
    private Button _findButton = null!;
    private Button _clearFilterButton = null!;
    private Button _testErrorButton = null!;
}
