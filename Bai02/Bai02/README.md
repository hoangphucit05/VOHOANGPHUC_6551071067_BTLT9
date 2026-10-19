# Quản lý nhà trọ

Ứng dụng WinForms quản lý phòng và khách thuê, sử dụng SQL Server/EF Core, API tỉnh thành Việt Nam, tìm kiếm/lọc và log Serilog.

## 1. Tạo CSDL

1. Mở `database/QL_NhaTro.sql` bằng SQL Server Management Studio hoặc Azure Data Studio.
2. Chạy toàn bộ script. Script tạo CSDL `QL_NhaTro`, hai bảng có khóa ngoại, sáu phòng và bốn khách mẫu.
3. Mặc định ứng dụng dùng LocalDB: `(localdb)\MSSQLLocalDB`. Nếu dùng SQL Server khác, đặt biến môi trường `QL_NHATRO_CONNECTION`, ví dụ:

```powershell
$env:QL_NHATRO_CONNECTION = 'Server=.\SQLEXPRESS;Database=QL_NhaTro;Trusted_Connection=True;TrustServerCertificate=True;'
```

## 2. Database First và navigation property

Trong Visual Studio Package Manager Console, sau khi tạo CSDL, cài công cụ EF rồi scaffold lại model để đối chiếu:

```powershell
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 10.0.2
Scaffold-DbContext "Server=(localdb)\MSSQLLocalDB;Database=QL_NhaTro;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/Scaffold -ContextDir Data/Scaffold -Context QLNhaTroScaffoldContext -DataAnnotations -Force
```

Trong mã ứng dụng, `Models/PhongTro.cs`, `Models/KhachThue.cs` và `Data/QLNhaTroContext.cs` là phần mapping Database First tương ứng. Navigation property `PhongTro.KhachThues` và `KhachThue.MaPhongNavigation` được ánh xạ qua `FK_KhachThue_PhongTro`.

## 3. Chạy ứng dụng

Cần Windows, .NET 10 SDK/Windows Desktop Runtime và SQL Server LocalDB hoặc SQL Server tương thích.

```powershell
dotnet restore
dotnet run
```

Form tải danh sách tỉnh/thành khi mở. Chọn tỉnh để tải phường/xã qua `https://provinces.open-api.vn/api/v2/`. Yêu cầu mạng có timeout 10 giây. Nếu API lỗi, ứng dụng ghi cảnh báo, cho gõ tay cả Tỉnh/Thành và Phường/Xã, lưu mã bằng `0` khi không có mã API.

Khi thêm khách, chỉ phòng chưa có khách được đưa vào danh sách chọn. Sửa khách được giữ phòng hiện tại và có thể chuyển sang phòng trống. Thêm/xóa/sửa cập nhật tình trạng phòng trong cùng transaction.

## 4. Log và kiểm tra lỗi

Log Serilog được ghi theo ngày vào `%LOCALAPPDATA%\QL_NhaTro\logs`, tối đa 14 tệp. Lỗi UI chưa xử lý được ghi cùng stack trace và hiện thông báo thân thiện. Nút kiểm tra lỗi được ẩn theo yêu cầu; nhấn `Ctrl+Shift+E` để kích hoạt. `Application.ThreadException` giữ ứng dụng chạy tiếp sau lỗi trên UI thread. Lỗi `AppDomain` được ghi log và hiển thị thông báo nếu UI còn khả dụng; lỗi nghiêm trọng ở mức tiến trình có thể khiến Windows kết thúc ứng dụng.

## 5. Nội dung chính

- `database/QL_NhaTro.sql`: schema, khóa ngoại, seed sáu phòng và bốn khách.
- `FormKhachThue.cs`: form CRUD, lọc, DataGridView JOIN, gọi API và xử lý lỗi mạng.
- `Services/AddressApiClient.cs`: một `HttpClient` dùng chung, timeout 10 giây.
- `Data/QLNhaTroContext.cs`, `Models/`: DbContext, entity và navigation property.
