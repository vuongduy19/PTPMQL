
using System;
using System.Linq;
using System.Threading.Tasks;
using DemoMVC.Data;
using DemoMVC.Models;
using DemoMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Controllers
{
    public class SinhVienController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SinhVienCodeGenerator _codeGenerator;

        public SinhVienController(
            ApplicationDbContext context,
            SinhVienCodeGenerator codeGenerator)
        {
            _context = context;
            _codeGenerator = codeGenerator;
        }

        // GET: SinhVien
        public async Task<IActionResult> Index()
        {
            var danhSach = await _context.SinhViens
                .Include(sv => sv.Khoa)
                .AsNoTracking()
                .OrderBy(sv => sv.MaSV)
                .ToListAsync();

            return View(danhSach);
        }

        // GET: SinhVien/Details/2821050001
        public async Task<IActionResult> Details(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var sinhVien = await _context.SinhViens
                .Include(sv => sv.Khoa)
                .AsNoTracking()
                .FirstOrDefaultAsync(sv => sv.MaSV == id);

            if (sinhVien == null)
                return NotFound();

            return View(sinhVien);
        }

        // GET: SinhVien/Create
        public async Task<IActionResult> Create()
        {
            await NapDanhSachKhoa();

            var sinhVien = new SinhVien
            {
                NamNhapHoc = 2028,
                MaHeDaoTao = "21",
                MaNganh = "05"
            };

            return View(sinhVien);
        }

        // POST: SinhVien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("HoTen,NgaySinh,MaKhoa,NamNhapHoc,MaHeDaoTao,MaNganh")]
            SinhVien sinhVien)
        {
            // Không cho chọn khoa không tồn tại
            bool khoaTonTai = await _context.Khoas
                .AnyAsync(k => k.MaKhoa == sinhVien.MaKhoa);

            if (!khoaTonTai)
            {
                ModelState.AddModelError(
                    nameof(sinhVien.MaKhoa),
                    "Vui lòng chọn khoa hợp lệ.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // TỰ ĐỘNG SINH MÃ SINH VIÊN
                    sinhVien.MaSV = await _codeGenerator.GenerateAsync(
                        sinhVien.NamNhapHoc,
                        sinhVien.MaHeDaoTao,
                        sinhVien.MaNganh);

                    _context.SinhViens.Add(sinhVien);
                    await _context.SaveChangesAsync();

                    TempData["Success"] =
                        "Thêm sinh viên thành công! Mã sinh viên: "
                        + sinhVien.MaSV;

                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        "Không thể lưu sinh viên. Vui lòng thử lại.");
                }
            }

            await NapDanhSachKhoa(sinhVien.MaKhoa);
            return View(sinhVien);
        }

        // GET: SinhVien/Edit/2821050001
        public async Task<IActionResult> Edit(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var sinhVien = await _context.SinhViens.FindAsync(id);

            if (sinhVien == null)
                return NotFound();

            await NapDanhSachKhoa(sinhVien.MaKhoa);

            return View(sinhVien);
        }

        // POST: SinhVien/Edit/2821050001
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            string id,
            [Bind("MaSV,HoTen,NgaySinh,MaKhoa")]
            SinhVien formSinhVien)
        {
            if (id != formSinhVien.MaSV)
                return NotFound();

            var sinhVien = await _context.SinhViens.FindAsync(id);

            if (sinhVien == null)
                return NotFound();

            bool khoaTonTai = await _context.Khoas
                .AnyAsync(k => k.MaKhoa == formSinhVien.MaKhoa);

            if (!khoaTonTai)
            {
                ModelState.AddModelError(
                    nameof(formSinhVien.MaKhoa),
                    "Vui lòng chọn khoa hợp lệ.");
            }

            if (ModelState.IsValid)
            {
                // Giữ nguyên mã sinh viên, chỉ cập nhật thông tin
                sinhVien.HoTen = formSinhVien.HoTen;
                sinhVien.NgaySinh = formSinhVien.NgaySinh;
                sinhVien.MaKhoa = formSinhVien.MaKhoa;

                await _context.SaveChangesAsync();

                TempData["Success"] = "Cập nhật sinh viên thành công.";

                return RedirectToAction(nameof(Index));
            }

            await NapDanhSachKhoa(formSinhVien.MaKhoa);
            return View(formSinhVien);
        }

        // GET: SinhVien/Delete/2821050001
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var sinhVien = await _context.SinhViens
                .Include(sv => sv.Khoa)
                .AsNoTracking()
                .FirstOrDefaultAsync(sv => sv.MaSV == id);

            if (sinhVien == null)
                return NotFound();

            return View(sinhVien);
        }

        // POST: SinhVien/Delete/2821050001
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sinhVien = await _context.SinhViens.FindAsync(id);

            if (sinhVien != null)
            {
                _context.SinhViens.Remove(sinhVien);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Xóa sinh viên thành công.";
            }

            return RedirectToAction(nameof(Index));
        }

        // Nạp dropdown khoa cho các trang Create/Edit
        private async Task NapDanhSachKhoa(string? maKhoaDuocChon = null)
        {
            var danhSachKhoa = await _context.Khoas
                .AsNoTracking()
                .OrderBy(k => k.TenKhoa)
                .ToListAsync();

            ViewBag.MaKhoa = new SelectList(
                danhSachKhoa,
                "MaKhoa",
                "TenKhoa",
                maKhoaDuocChon);
        }
    }
}
