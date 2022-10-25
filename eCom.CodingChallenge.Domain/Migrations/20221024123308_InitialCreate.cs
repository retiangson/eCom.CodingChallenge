using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCom.CodingChallenge.Domain.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Names",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    First = table.Column<string>(type: "TEXT", nullable: false),
                    Middle = table.Column<string>(type: "TEXT", nullable: false),
                    Last = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Names", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NameId = table.Column<int>(type: "INTEGER", nullable: false),
                    Street = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    Zip = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Names_NameId",
                        column: x => x.NameId,
                        principalTable: "Names",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NameId = table.Column<int>(type: "INTEGER", nullable: false),
                    PhoneTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_Names_NameId",
                        column: x => x.NameId,
                        principalTable: "Names",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Phones_PhoneTypes_PhoneTypeId",
                        column: x => x.PhoneTypeId,
                        principalTable: "PhoneTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PhoneTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 1, "Home" });

            migrationBuilder.InsertData(
                table: "PhoneTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 2, "Work" });

            migrationBuilder.InsertData(
                table: "PhoneTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 3, "Mobile" });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Id",
                table: "Addresses",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_NameId",
                table: "Addresses",
                column: "NameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NameId",
                table: "Addresses",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Names_Id",
                table: "Names",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NameId1",
                table: "Phones",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_Id",
                table: "Phones",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phones_PhoneTypeId",
                table: "Phones",
                column: "PhoneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeId",
                table: "Phones",
                column: "NameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "Names");

            migrationBuilder.DropTable(
                name: "PhoneTypes");
        }
    }
}
