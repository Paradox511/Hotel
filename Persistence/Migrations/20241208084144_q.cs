using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class q : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_taikhoan",
                table: "taikhoan");

            migrationBuilder.DropColumn(
                name: "Quyen",
                table: "taikhoan");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "taikhoan");

            migrationBuilder.RenameTable(
                name: "taikhoan",
                newName: "TaiKhoan");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "TaiKhoan",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "TaiKhoan",
                newName: "password");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "TaiKhoan",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "TaiKhoan",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_id_2",
                table: "TaiKhoan",
                column: "MaTaiKhoan")
                .Annotation("SqlServer:Clustered", false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user_id_2",
                table: "TaiKhoan");

            migrationBuilder.RenameTable(
                name: "TaiKhoan",
                newName: "taikhoan");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "taikhoan",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "taikhoan",
                newName: "Password");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "taikhoan",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "taikhoan",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Quyen",
                table: "taikhoan",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TrangThai",
                table: "taikhoan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_taikhoan",
                table: "taikhoan",
                column: "MaTaiKhoan");
        }
    }
}
