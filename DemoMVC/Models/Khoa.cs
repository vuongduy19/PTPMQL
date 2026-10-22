using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class Khoa
    {
        [Key]
        [StringLength(2)]
        [Display(Name = "Mã khoa")]
        public string MaKhoa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tên khoa")]
        [StringLength(100)]
        [Display(Name = "Tên khoa")]
        public string TenKhoa { get; set; } = string.Empty;

        public ICollection<SinhVien> SinhViens { get; set; }
            = new List<SinhVien>();
    }
}