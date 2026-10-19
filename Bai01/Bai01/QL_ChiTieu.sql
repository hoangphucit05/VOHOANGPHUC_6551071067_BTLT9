-- =============================================
-- SCRIPT TẠO CƠ SỞ DỮ LIỆU VÀ DỮ LIỆU MẪU
-- Tên CSDL: QL_ChiTieu
-- Ứng dụng: Sổ chi tiêu cá nhân (WinForms - EF Core)
-- =============================================

USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'QL_ChiTieu')
BEGIN
    CREATE DATABASE QL_ChiTieu;
END
GO

USE QL_ChiTieu;
GO

-- Xóa bảng nếu đã tồn tại để tạo mới sạch sẽ
IF OBJECT_ID(N'dbo.ChiTieu', N'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.ChiTieu;
END
GO

-- Tạo bảng ChiTieu theo đúng đặc tả đề bài
CREATE TABLE dbo.ChiTieu (
    MaCT INT IDENTITY(1,1) NOT NULL,
    NgayChi DATE NOT NULL,
    NoiDung NVARCHAR(100) NOT NULL,
    LoaiChi NVARCHAR(30) NOT NULL,
    SoTien DECIMAL(18, 0) NOT NULL,
    GhiChu NVARCHAR(200) NULL,
    CONSTRAINT PK_ChiTieu PRIMARY KEY CLUSTERED (MaCT ASC)
);
GO

-- Thêm ít nhất 10 dòng dữ liệu mẫu đa dạng các loại chi tiêu
INSERT INTO dbo.ChiTieu (NgayChi, NoiDung, LoaiChi, SoTien, GhiChu)
VALUES 
    ('2026-10-01', N'Cơm trưa', N'Ăn uống', 35000, N'Cơm văn phòng'),
    ('2026-10-02', N'Xăng xe', N'Đi lại', 60000, N'Đổ xăng xe máy'),
    ('2026-10-03', N'Photo giáo trình', N'Học tập', 25000, N'Tài liệu C# WinForms'),
    ('2026-10-04', N'Xem phim', N'Giải trí', 90000, N'Vé xem phim rạp cuối tuần'),
    ('2026-10-05', N'Trà sữa', N'Ăn uống', 40000, N'Uống cùng đồng nghiệp'),
    ('2026-10-06', N'Mua đồ dùng cá nhân', N'Khác', 150000, N'Sữa tắm, kem đánh răng'),
    ('2026-10-07', N'Tiền điện thoại', N'Khác', 100000, N'Nạp thẻ cào Viettel'),
    ('2026-10-08', N'Ăn sáng', N'Ăn uống', 25000, N'Phở bò'),
    ('2026-10-09', N'Mua sách tham khảo', N'Học tập', 120000, N'Giáo trình Lập trình .NET'),
    ('2026-10-10', N'Thay nhớt xe máy', N'Đi lại', 150000, N'Bảo dưỡng định kỳ'),
    ('2026-10-15', N'Cà phê với bạn', N'Ăn uống', 45000, N'Họp nhóm đồ án'),
    ('2026-10-18', N'Mua vé ca nhạc', N'Giải trí', 200000, N'Cuối tuần cùng bạn bè');
GO

-- Kiểm tra dữ liệu vừa nhập
SELECT * FROM dbo.ChiTieu ORDER BY MaCT ASC;
GO
