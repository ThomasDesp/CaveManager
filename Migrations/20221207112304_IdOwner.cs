using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaveManager.Migrations
{
    /// <inheritdoc />
    public partial class IdOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Cave",
                newName: "IdOwner");

            migrationBuilder.UpdateData(
                table: "Cave",
                keyColumn: "Id",
                keyValue: 1,
                column: "IdOwner",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Cave",
                keyColumn: "Id",
                keyValue: 2,
                column: "IdOwner",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Cave",
                keyColumn: "Id",
                keyValue: 3,
                column: "IdOwner",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdOwner",
                table: "Cave",
                newName: "IdUser");

            migrationBuilder.UpdateData(
                table: "Cave",
                keyColumn: "Id",
                keyValue: 1,
                column: "IdUser",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Cave",
                keyColumn: "Id",
                keyValue: 2,
                column: "IdUser",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Cave",
                keyColumn: "Id",
                keyValue: 3,
                column: "IdUser",
                value: 0);
        }
    }
}
