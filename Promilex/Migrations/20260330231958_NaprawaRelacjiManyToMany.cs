using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promilex.Migrations
{
    /// <inheritdoc />
    public partial class NaprawaRelacjiManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produkty_Skladniki_SkladnikId",
                table: "Produkty");

            migrationBuilder.DropIndex(
                name: "IX_Produkty_SkladnikId",
                table: "Produkty");

            migrationBuilder.DropColumn(
                name: "SkladnikId",
                table: "Produkty");

            migrationBuilder.CreateTable(
                name: "ProduktSkladnik",
                columns: table => new
                {
                    ProduktyId = table.Column<int>(type: "int", nullable: false),
                    SkladnikiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduktSkladnik", x => new { x.ProduktyId, x.SkladnikiId });
                    table.ForeignKey(
                        name: "FK_ProduktSkladnik_Produkty_ProduktyId",
                        column: x => x.ProduktyId,
                        principalTable: "Produkty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduktSkladnik_Skladniki_SkladnikiId",
                        column: x => x.SkladnikiId,
                        principalTable: "Skladniki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProduktSkladnik_SkladnikiId",
                table: "ProduktSkladnik",
                column: "SkladnikiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProduktSkladnik");

            migrationBuilder.AddColumn<int>(
                name: "SkladnikId",
                table: "Produkty",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produkty_SkladnikId",
                table: "Produkty",
                column: "SkladnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produkty_Skladniki_SkladnikId",
                table: "Produkty",
                column: "SkladnikId",
                principalTable: "Skladniki",
                principalColumn: "Id");
        }
    }
}
