
using Microsoft.EntityFrameworkCore;
using DemoMVC.Models;

namespace DemoMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Dữ liệu mẫu các khoa
            modelBuilder.Entity<Khoa>().HasData(
                new Khoa { MaKhoa = "01", TenKhoa = "Khoa Công nghệ thông tin" },
                new Khoa { MaKhoa = "02", TenKhoa = "Khoa Cơ điện" },
                new Khoa { MaKhoa = "03", TenKhoa = "Khoa Dầu khí và Năng lượng" },
                new Khoa { MaKhoa = "04", TenKhoa = "Khoa Kinh tế và Quản trị kinh doanh" },
                new Khoa { MaKhoa = "05", TenKhoa = "Khoa Khoa học và Kỹ thuật Địa chất" },
                new Khoa { MaKhoa = "06", TenKhoa = "Khoa Mỏ" },
                new Khoa { MaKhoa = "07", TenKhoa = "Khoa Môi trường" },
                new Khoa { MaKhoa = "08", TenKhoa = "Khoa Trắc địa - Bản đồ và Quản lý đất đai" },
                new Khoa { MaKhoa = "09", TenKhoa = "Khoa Xây dựng" },
                new Khoa { MaKhoa = "10", TenKhoa = "Khoa Khoa học cơ bản" },
                new Khoa { MaKhoa = "11", TenKhoa = "Khoa Lý luận Chính trị" },
                new Khoa { MaKhoa = "12", TenKhoa = "Khoa Giáo dục Quốc phòng" }
            );

            // Liên kết SinhVien với Khoa bằng MaKhoa
            modelBuilder.Entity<SinhVien>()
                .HasOne(sv => sv.Khoa)
                .WithMany(k => k.SinhViens)
                .HasForeignKey(sv => sv.MaKhoa)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
