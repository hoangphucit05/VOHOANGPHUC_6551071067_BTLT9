using Microsoft.EntityFrameworkCore;
using QL_NhaTro.Models;

namespace QL_NhaTro.Data;

public partial class QLNhaTroContext : DbContext
{
    public QLNhaTroContext() { }

    public QLNhaTroContext(DbContextOptions<QLNhaTroContext> options) : base(options) { }

    public virtual DbSet<KhachThue> KhachThues { get; set; } = null!;
    public virtual DbSet<PhongTro> PhongTros { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = Environment.GetEnvironmentVariable("QL_NHATRO_CONNECTION")
                ?? "Server=(localdb)\\MSSQLLocalDB;Database=QL_NhaTro;Trusted_Connection=True;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString, options => options.CommandTimeout(15));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PhongTro>(entity =>
        {
            entity.HasKey(e => e.MaPhong);
            entity.ToTable("PhongTro");
            entity.Property(e => e.TenPhong).HasMaxLength(20);
            entity.Property(e => e.GiaThue).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TinhTrang).HasMaxLength(20);
        });

        modelBuilder.Entity<KhachThue>(entity =>
        {
            entity.HasKey(e => e.MaKhach);
            entity.ToTable("KhachThue");
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.GioiTinh).HasMaxLength(5).IsUnicode();
            entity.Property(e => e.SoDienThoai).HasMaxLength(15).IsUnicode(false);
            entity.Property(e => e.TenTinh).HasMaxLength(100);
            entity.Property(e => e.TenPhuongXa).HasMaxLength(100);
            entity.HasOne(d => d.MaPhongNavigation)
                .WithMany(p => p.KhachThues)
                .HasForeignKey(d => d.MaPhong)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_KhachThue_PhongTro");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
