
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models
{
    public class SinhVien
    {
        [Key]
        [StringLength(10)]
        [Display(Name = "Mã sinh viên")]
        public string MaSV { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [StringLength(100)]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        public DateTime? NgaySinh { get; set; }

        [Required]
        [StringLength(2)]
        [Display(Name = "Mã khoa")]
        public string MaKhoa { get; set; } = string.Empty;

        [Required]
        [Range(2000, 2100)]
        [Display(Name = "Năm nhập học")]
        public int NamNhapHoc { get; set; }

        [Required]
        [StringLength(2)]
        [Display(Name = "Mã hệ đào tạo")]
        public string MaHeDaoTao { get; set; } = "21";

        [Required]
        [StringLength(2)]
        [Display(Name = "Mã ngành")]
        public string MaNganh { get; set; } = "05";

        [ForeignKey(nameof(MaKhoa))]
        public Khoa? Khoa { get; set; }
    }
}
