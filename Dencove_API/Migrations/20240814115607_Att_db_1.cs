using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dencove_API.Migrations
{
    /// <inheritdoc />
    public partial class Att_db_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Denuncia");

            migrationBuilder.AddColumn<string>(
                name: "ImgURL",
                table: "Denuncia",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgURL",
                table: "Denuncia");

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Denuncia",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
