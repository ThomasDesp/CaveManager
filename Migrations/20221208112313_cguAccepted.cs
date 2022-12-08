using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaveManager.Migrations
{
    /// <inheritdoc />
    public partial class cguAccepted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAged",
                table: "Owner");

            migrationBuilder.RenameColumn(
                name: "IsFirstConnection",
                table: "Owner",
                newName: "IsCGUAccepted");

            migrationBuilder.UpdateData(
                table: "Owner",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsCGUAccepted",
                value: true);

            migrationBuilder.UpdateData(
                table: "Owner",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsCGUAccepted",
                value: true);

            migrationBuilder.UpdateData(
                table: "Owner",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsCGUAccepted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Wine",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Bottling", "MaxVintageRecommended", "MinVintageRecommended" },
                values: new object[] { 2019, 8, 2 });

            migrationBuilder.UpdateData(
                table: "Wine",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Bottling", "MaxVintageRecommended", "MinVintageRecommended" },
                values: new object[] { 2011, 8, 4 });

            migrationBuilder.UpdateData(
                table: "Wine",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Bottling", "MaxVintageRecommended", "MinVintageRecommended" },
                values: new object[] { 2012, 12, 10 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCGUAccepted",
                table: "Owner",
                newName: "IsFirstConnection");

            migrationBuilder.AddColumn<bool>(
                name: "IsAged",
                table: "Owner",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Owner",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsAged", "IsFirstConnection" },
                values: new object[] { true, false });

            migrationBuilder.UpdateData(
                table: "Owner",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsAged", "IsFirstConnection" },
                values: new object[] { true, false });

            migrationBuilder.UpdateData(
                table: "Owner",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IsAged", "IsFirstConnection" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Wine",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Bottling", "MaxVintageRecommended", "MinVintageRecommended" },
                values: new object[] { 0, null, null });

            migrationBuilder.UpdateData(
                table: "Wine",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Bottling", "MaxVintageRecommended", "MinVintageRecommended" },
                values: new object[] { 0, null, null });

            migrationBuilder.UpdateData(
                table: "Wine",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Bottling", "MaxVintageRecommended", "MinVintageRecommended" },
                values: new object[] { 0, null, null });
        }
    }
}
