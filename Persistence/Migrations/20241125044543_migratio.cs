using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class migratio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dichvu_cthoadon_cthdMacthd",
                table: "dichvu");

            migrationBuilder.DropIndex(
                name: "IX_dichvu_cthdMacthd",
                table: "dichvu");

            migrationBuilder.DropIndex(
                name: "IX_cthoadon_MaDichVu",
                table: "cthoadon");

            migrationBuilder.DropColumn(
                name: "cthdMacthd",
                table: "dichvu");

            migrationBuilder.CreateIndex(
                name: "IX_cthoadon_MaDichVu",
                table: "cthoadon",
                column: "MaDichVu",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_cthoadon_MaDichVu",
                table: "cthoadon");

            migrationBuilder.AddColumn<int>(
                name: "cthdMacthd",
                table: "dichvu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_dichvu_cthdMacthd",
                table: "dichvu",
                column: "cthdMacthd");

            migrationBuilder.CreateIndex(
                name: "IX_cthoadon_MaDichVu",
                table: "cthoadon",
                column: "MaDichVu");

            migrationBuilder.AddForeignKey(
                name: "FK_dichvu_cthoadon_cthdMacthd",
                table: "dichvu",
                column: "cthdMacthd",
                principalTable: "cthoadon",
                principalColumn: "Macthd",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
