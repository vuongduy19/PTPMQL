using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DemoMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddKhoaSinhVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Khoas",
                columns: table => new
                {
                    MaKhoa = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    TenKhoa = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoas", x => x.MaKhoa);
                });

            migrationBuilder.CreateTable(
                name: "SinhViens",
                columns: table => new
                {
                    MaSV = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    HoTen = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MaKhoa = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    NamNhapHoc = table.Column<int>(type: "INTEGER", nullable: false),
                    MaHeDaoTao = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    MaNganh = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhViens", x => x.MaSV);
                    table.ForeignKey(
                        name: "FK_SinhViens_Khoas_MaKhoa",
                        column: x => x.MaKhoa,
                        principalTable: "Khoas",
                        principalColumn: "MaKhoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Khoas",
                columns: new[] { "MaKhoa", "TenKhoa" },
                values: new object[,]
                {
                    { "01", "Khoa Công nghệ thông tin" },
                    { "02", "Khoa Cơ điện" },
                    { "03", "Khoa Dầu khí và Năng lượng" },
                    { "04", "Khoa Kinh tế và Quản trị kinh doanh" },
                    { "05", "Khoa Khoa học và Kỹ thuật Địa chất" },
                    { "06", "Khoa Mỏ" },
                    { "07", "Khoa Môi trường" },
                    { "08", "Khoa Trắc địa - Bản đồ và Quản lý đất đai" },
                    { "09", "Khoa Xây dựng" },
                    { "10", "Khoa Khoa học cơ bản" },
                    { "11", "Khoa Lý luận Chính trị" },
                    { "12", "Khoa Giáo dục Quốc phòng" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_MaKhoa",
                table: "SinhViens",
                column: "MaKhoa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SinhViens");

            migrationBuilder.DropTable(
                name: "Khoas");
        }
    }
}
