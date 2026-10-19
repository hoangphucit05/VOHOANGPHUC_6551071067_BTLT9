/* Chạy file này trong SQL Server Management Studio hoặc Azure Data Studio. */
IF DB_ID(N'QL_NhaTro') IS NULL
BEGIN
    CREATE DATABASE QL_NhaTro;
END;
GO

USE QL_NhaTro;
GO

IF OBJECT_ID(N'dbo.PhongTro', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PhongTro
    (
        MaPhong int IDENTITY(1,1) NOT NULL CONSTRAINT PK_PhongTro PRIMARY KEY,
        TenPhong nvarchar(20) NOT NULL,
        GiaThue decimal(18,0) NOT NULL,
        TinhTrang nvarchar(20) NOT NULL CONSTRAINT DF_PhongTro_TinhTrang DEFAULT N'Trống',
        CONSTRAINT CK_PhongTro_TinhTrang CHECK (TinhTrang IN (N'Trống', N'Đang thuê')),
        CONSTRAINT UQ_PhongTro_TenPhong UNIQUE (TenPhong)
    );
END;
GO

IF OBJECT_ID(N'dbo.KhachThue', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.KhachThue
    (
        MaKhach int IDENTITY(1,1) NOT NULL CONSTRAINT PK_KhachThue PRIMARY KEY,
        HoTen nvarchar(100) NOT NULL,
        GioiTinh nvarchar(5) NOT NULL,
        NgaySinh date NOT NULL,
        SoDienThoai varchar(15) NOT NULL,
        MaTinh int NOT NULL,
        TenTinh nvarchar(100) NOT NULL,
        MaPhuongXa int NOT NULL,
        TenPhuongXa nvarchar(100) NOT NULL,
        DaDangKyTamTru bit NOT NULL CONSTRAINT DF_KhachThue_TamTru DEFAULT 0,
        MaPhong int NOT NULL,
        CONSTRAINT FK_KhachThue_PhongTro FOREIGN KEY (MaPhong)
            REFERENCES dbo.PhongTro(MaPhong)
    );
    CREATE INDEX IX_KhachThue_MaPhong ON dbo.KhachThue(MaPhong);
    CREATE INDEX IX_KhachThue_TenTinh ON dbo.KhachThue(TenTinh);
END;
GO

INSERT INTO dbo.PhongTro (TenPhong, GiaThue, TinhTrang)
SELECT seed.TenPhong, seed.GiaThue, seed.TinhTrang
FROM (VALUES
    (N'P01', CONVERT(decimal(18,0), 1500000), N'Đang thuê'),
    (N'P02', CONVERT(decimal(18,0), 1600000), N'Đang thuê'),
    (N'P03', CONVERT(decimal(18,0), 1800000), N'Đang thuê'),
    (N'P04', CONVERT(decimal(18,0), 2000000), N'Đang thuê'),
    (N'P05', CONVERT(decimal(18,0), 1700000), N'Trống'),
    (N'P06', CONVERT(decimal(18,0), 1900000), N'Trống')
) AS seed(TenPhong, GiaThue, TinhTrang)
WHERE NOT EXISTS (SELECT 1 FROM dbo.PhongTro existing WHERE existing.TenPhong = seed.TenPhong);
GO

/* Mã phường/xã = 0 đánh dấu dữ liệu mẫu nhập tay; khi chọn từ API, ứng dụng lưu mã hiện hành. */
INSERT INTO dbo.KhachThue
    (HoTen, GioiTinh, NgaySinh, SoDienThoai, MaTinh, TenTinh, MaPhuongXa, TenPhuongXa, DaDangKyTamTru, MaPhong)
SELECT seed.HoTen, seed.GioiTinh, seed.NgaySinh, seed.SoDienThoai, seed.MaTinh, seed.TenTinh,
       seed.MaPhuongXa, seed.TenPhuongXa, seed.DaDangKyTamTru, room.MaPhong
FROM (VALUES
    (N'Nguyễn Văn An', N'Nam', CONVERT(date, '2004-05-12'), '0912345678', 1, N'Thành phố Hà Nội', 0, N'Phường Ba Đình', CONVERT(bit, 1), N'P03'),
    (N'Trần Thị Bình', N'Nữ', CONVERT(date, '2002-08-20'), '0876543210', 38, N'Tỉnh Thanh Hóa', 0, N'Phường Hạc Thành', CONVERT(bit, 0), N'P01'),
    (N'Lê Văn Cường', N'Nam', CONVERT(date, '1999-11-02'), '0901234567', 38, N'Tỉnh Thanh Hóa', 0, N'Phường Lam Sơn', CONVERT(bit, 0), N'P02'),
    (N'Phạm Minh Đức', N'Nam', CONVERT(date, '2001-03-18'), '0987654321', 24, N'Thành phố Bắc Ninh', 0, N'Phường Kinh Bắc', CONVERT(bit, 1), N'P04')
) AS seed(HoTen, GioiTinh, NgaySinh, SoDienThoai, MaTinh, TenTinh, MaPhuongXa, TenPhuongXa, DaDangKyTamTru, TenPhong)
INNER JOIN dbo.PhongTro room ON room.TenPhong = seed.TenPhong
WHERE NOT EXISTS (SELECT 1 FROM dbo.KhachThue existing WHERE existing.SoDienThoai = seed.SoDienThoai);
GO

/* Đồng bộ trạng thái phòng với dữ liệu khách (hữu ích khi chạy lại script). */
UPDATE room
SET TinhTrang = CASE WHEN EXISTS (SELECT 1 FROM dbo.KhachThue guest WHERE guest.MaPhong = room.MaPhong)
                     THEN N'Đang thuê' ELSE N'Trống' END
FROM dbo.PhongTro room;
GO
