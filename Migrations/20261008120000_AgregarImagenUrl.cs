using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaToxicaCRUD.Migrations
{
    /// <inheritdoc />
    public partial class AgregarImagenUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagenUrl",
                table: "Platos",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenUrl",
                table: "Platos");
        }
    }
}
