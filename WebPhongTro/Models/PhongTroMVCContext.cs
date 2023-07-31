using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebPhongTro.Models
{
    public partial class PhongTroMVCContext : DbContext
    {
        public PhongTroMVCContext()
        {
        }

        public PhongTroMVCContext(DbContextOptions<PhongTroMVCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<HoaDon> HoaDons { get; set; } = null!;
        public virtual DbSet<HopDong> HopDongs { get; set; } = null!;
        public virtual DbSet<Phong> Phongs { get; set; } = null!;
        public virtual DbSet<VatDung> VatDungs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=KHOA;Database=PhongTroMVC;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.IdHoadon)
                    .HasName("PK__Hoa_don__F9F51331A22E1280");

                entity.ToTable("Hoa_don");

                entity.Property(e => e.IdHoadon)
                    .ValueGeneratedNever()
                    .HasColumnName("id_hoadon");

                entity.Property(e => e.IdHopdong).HasColumnName("id_hopdong");

                entity.Property(e => e.NgayLap)
                    .HasColumnType("date")
                    .HasColumnName("ngay_lap");

                entity.Property(e => e.SoTien).HasColumnName("so_tien");

                entity.HasOne(d => d.IdHopdongNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.IdHopdong)
                    .HasConstraintName("FK__Hoa_don__id_hopd__797309D9");
            });

            modelBuilder.Entity<HopDong>(entity =>
            {
                entity.HasKey(e => e.IdHopdong)
                    .HasName("PK__Hop_dong__B2370444AD9A3AE7");

                entity.ToTable("Hop_dong");

                entity.Property(e => e.IdHopdong)
                    .ValueGeneratedNever()
                    .HasColumnName("id_hopdong");

                entity.Property(e => e.IdKhach).HasColumnName("id_khach");

                entity.Property(e => e.IdPhong).HasColumnName("id_phong");

                entity.Property(e => e.NgayBatDau)
                    .HasColumnType("date")
                    .HasColumnName("ngay_bat_dau");

                entity.Property(e => e.NgayKetThuc)
                    .HasColumnType("date")
                    .HasColumnName("ngay_ket_thuc");

                entity.HasOne(d => d.IdPhongNavigation)
                    .WithMany(p => p.HopDongs)
                    .HasForeignKey(d => d.IdPhong)
                    .HasConstraintName("FK__Hop_dong__id_pho__75A278F5");

                entity.HasMany(d => d.IdUsers)
                    .WithMany(p => p.IdHds)
                    .UsingEntity<Dictionary<string, object>>(
                        "HopDongUser",
                        l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("IdUser").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__HopDongUs__idUse__01142BA1"),
                        r => r.HasOne<HopDong>().WithMany().HasForeignKey("IdHd").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__HopDongUse__idHD__00200768"),
                        j =>
                        {
                            j.HasKey("IdHd", "IdUser");

                            j.ToTable("HopDongUser");

                            j.IndexerProperty<int>("IdHd").HasColumnName("idHD");

                            j.IndexerProperty<string>("IdUser").HasColumnName("idUser");
                        });
            });

            modelBuilder.Entity<Phong>(entity =>
            {
                entity.HasKey(e => e.IdPhong)
                    .HasName("PK__Phong__599EC1F8D729FF38");

                entity.ToTable("Phong");

                entity.Property(e => e.IdPhong)
                    .ValueGeneratedNever()
                    .HasColumnName("id_phong");

                entity.Property(e => e.DienTich).HasColumnName("dien_tich");

                entity.Property(e => e.GiaPhong).HasColumnName("gia_phong");

                entity.Property(e => e.HinhAnh).IsUnicode(false);

                entity.Property(e => e.TenPhong)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ten_phong");

                entity.Property(e => e.TrangThai)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("trang_thai");
            });

            modelBuilder.Entity<VatDung>(entity =>
            {
                entity.HasKey(e => e.IdVatdung)
                    .HasName("PK__Vat_dung__0A6C28FD122AD489");

                entity.ToTable("Vat_dung");

                entity.Property(e => e.IdVatdung)
                    .ValueGeneratedNever()
                    .HasColumnName("id_vatdung");

                entity.Property(e => e.IdPhong).HasColumnName("id_phong");

                entity.Property(e => e.TenVatdung)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ten_vatdung");

                entity.HasOne(d => d.IdPhongNavigation)
                    .WithMany(p => p.VatDungs)
                    .HasForeignKey(d => d.IdPhong)
                    .HasConstraintName("FK__Vat_dung__id_pho__70DDC3D8");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
