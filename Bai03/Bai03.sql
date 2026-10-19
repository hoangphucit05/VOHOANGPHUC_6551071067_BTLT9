-- Script tạo CSDL QL_DaNgoai và bảng ChuyenDi
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'QL_DaNgoai')
BEGIN
    CREATE DATABASE QL_DaNgoai;
END
GO

USE QL_DaNgoai;
GO

IF OBJECT_ID('dbo.ChuyenDi', 'U') IS NOT NULL
    DROP TABLE dbo.ChuyenDi;
GO

CREATE TABLE dbo.ChuyenDi (
    MaChuyen INT IDENTITY(1,1) PRIMARY KEY,
    TenChuyen NVARCHAR(100) NOT NULL,
    DiaDiem NVARCHAR(100) NOT NULL,
    NgayDi DATE NOT NULL,
    SoNguoi INT NOT NULL,
    NganSach DECIMAL(18, 0) NOT NULL,
    PhuongTien NVARCHAR(30) NOT NULL,
    DaHoanThanh BIT NOT NULL CONSTRAINT DF_ChuyenDi_DaHoanThanh DEFAULT 0,
    GhiChu NVARCHAR(200) NULL
);
GO

-- Thêm 8 chuyến đi mẫu với ngày đi trải từ vài ngày đến vài tuần tới
INSERT INTO dbo.ChuyenDi (TenChuyen, DiaDiem, NgayDi, SoNguoi, NganSach, PhuongTien, DaHoanThanh, GhiChu)
VALUES 
(N'Cắm trại đồi chè', N'Đà Lạt', '2026-10-25', 6, 3500000, N'Ô tô', 0, N'Mang theo áo ấm và lều chống thấm'),
(N'Săn mây Fansipan', N'Sa Pa', '2026-10-22', 4, 4200000, N'Xe khách', 0, N'Chuẩn bị gậy leo núi và giày thể thao'),
(N'Nghỉ dưỡng bãi Sau', N'Vũng Tàu', '2026-10-21', 5, 2800000, N'Xe máy', 0, N'Đi phượt ngắm biển cuối tuần'),
(N'Dạo phố cổ đêm hoa đăng', N'Hội An', '2026-10-28', 2, 5000000, N'Tàu hỏa', 0, N'Trải nghiệm ẩm thực phố cổ'),
(N'Lặn ngắm san hô vịnh biển', N'Nha Trang', '2026-10-30', 8, 6500000, N'Xe khách', 0, N'Thuê cano ra các đảo'),
(N'Trượt cát ngắm bình minh', N'Phan Thiết', '2026-11-02', 3, 2200000, N'Xe máy', 0, N'Chụp ảnh bàu trắng'),
(N'Chèo thuyền hang động', N'Ninh Bình', '2026-11-08', 7, 4800000, N'Tàu hỏa', 1, N'Khám phá danh thắng Tràng An'),
(N'Kỳ nghỉ biển đảo thiên đường', N'Phú Quốc', '2026-11-20', 4, 9000000, N'Ô tô', 0, N'Chuyến đi sau 16 ngày để kiểm tra dự báo');
GO
