using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dencove_API.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QtdCasosDengue",
                table: "Bairro",
                newName: "QtdAlertaMax");

            migrationBuilder.AddColumn<int>(
                name: "CasosConfirmados",
                table: "Bairro",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CasosConfirmados",
                table: "Bairro");

            migrationBuilder.RenameColumn(
                name: "QtdAlertaMax",
                table: "Bairro",
                newName: "QtdCasosDengue");
        }
    }
}
