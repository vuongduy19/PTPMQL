
using DemoMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Services
{
    public class SinhVienCodeGenerator
    {
        private readonly ApplicationDbContext _context;

        public SinhVienCodeGenerator(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateAsync(
            int namNhapHoc,
            string maHeDaoTao,
            string maNganh)
        {
            string prefix =
                $"{namNhapHoc % 100:00}{maHeDaoTao}{maNganh}";

            var codes = await _context.SinhViens
                .Where(sv => sv.MaSV.StartsWith(prefix))
                .Select(sv => sv.MaSV)
                .ToListAsync();

            int maxNumber = codes
                .Where(code => code.Length == prefix.Length + 4)
                .Select(code =>
                    int.TryParse(
                        code.Substring(prefix.Length),
                        out int number)
                        ? number
                        : 0)
                .DefaultIfEmpty(0)
                .Max();

            if (maxNumber >= 9999)
            {
                throw new InvalidOperationException(
                    "Đã hết số thứ tự cho nhóm mã này.");
            }

            return prefix + (maxNumber + 1).ToString("D4");
        }
    }
}
