using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaveManager.Migrations
{
    /// <inheritdoc />
    public partial class AdressChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Owner",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Owner",
                newName: "Adress");
        }
    }
}
