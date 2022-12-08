using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaveManager.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAged = table.Column<bool>(type: "bit", nullable: false),
                    IsFirstConnection = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cave_Owner_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drawer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxPlace = table.Column<int>(type: "int", nullable: false),
                    PlaceUsed = table.Column<int>(type: "int", nullable: false),
                    CaveId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drawer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drawer_Cave_CaveId",
                        column: x => x.CaveId,
                        principalTable: "Cave",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinVintageRecommended = table.Column<int>(type: "int", nullable: true),
                    MaxVintageRecommended = table.Column<int>(type: "int", nullable: true),
                    Bottling = table.Column<int>(type: "int", nullable: false),
                    DrawerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wine_Drawer_DrawerId",
                        column: x => x.DrawerId,
                        principalTable: "Drawer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Owner",
                columns: new[] { "Id", "Adress", "Email", "FirstName", "FullName", "IsAged", "IsFirstConnection", "LastName", "Password", "PhoneNumber1", "PhoneNumber2", "PhoneNumber3" },
                values: new object[,]
                {
                    { 1, null, "wil@gmail.com", "Wil", null, true, false, "TF", "MelmanoucheA9", null, null, null },
                    { 2, null, "leo@gmail.com", "Leo", null, true, false, "SMaster", "1v9A", null, null, null },
                    { 3, null, "thom@gmail.com", "Thom", null, true, true, "PokFan", "DAzE2", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Cave",
                columns: new[] { "Id", "Name", "OwnerId" },
                values: new object[,]
                {
                    { 1, "BatCave", 2 },
                    { 2, "ThomCave", 2 },
                    { 3, "Cavaleo", 1 }
                });

            migrationBuilder.InsertData(
                table: "Drawer",
                columns: new[] { "Id", "CaveId", "MaxPlace", "Name", "PlaceUsed" },
                values: new object[,]
                {
                    { 1, 1, 10, "Pomme", 2 },
                    { 2, 2, 10, "Poire", 1 },
                    { 3, 1, 10, "Banana", 0 }
                });

            migrationBuilder.InsertData(
                table: "Wine",
                columns: new[] { "Id", "Bottling", "Designation", "DrawerId", "MaxVintageRecommended", "MinVintageRecommended", "Name", "Type" },
                values: new object[,]
                {
                    { 1, 0, null, 1, null, null, "Vin de fou", "Red Wine" },
                    { 2, 0, null, 1, null, null, "Vin pas fou", "Rosé Wine" },
                    { 3, 0, null, 2, null, null, "Vin de fou pas fou", "White Wine" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cave_OwnerId",
                table: "Cave",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Drawer_CaveId",
                table: "Drawer",
                column: "CaveId");

            migrationBuilder.CreateIndex(
                name: "IX_Wine_DrawerId",
                table: "Wine",
                column: "DrawerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wine");

            migrationBuilder.DropTable(
                name: "Drawer");

            migrationBuilder.DropTable(
                name: "Cave");

            migrationBuilder.DropTable(
                name: "Owner");
        }
    }
}
