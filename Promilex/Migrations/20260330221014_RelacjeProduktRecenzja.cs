using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promilex.Migrations
{
    /// <inheritdoc />
    public partial class RelacjeProduktRecenzja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tresc",
                table: "Recenzje",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "KategoriaId",
                table: "Produkty",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProducentId",
                table: "Produkty",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Recenzje_ProduktId",
                table: "Recenzje",
                column: "ProduktId");

            migrationBuilder.CreateIndex(
                name: "IX_Produkty_KategoriaId",
                table: "Produkty",
                column: "KategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produkty_ProducentId",
                table: "Produkty",
                column: "ProducentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produkty_Kategorie_KategoriaId",
                table: "Produkty",
                column: "KategoriaId",
                principalTable: "Kategorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produkty_Producenci_ProducentId",
                table: "Produkty",
                column: "ProducentId",
                principalTable: "Producenci",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recenzje_Produkty_ProduktId",
                table: "Recenzje",
                column: "ProduktId",
                principalTable: "Produkty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produkty_Kategorie_KategoriaId",
                table: "Produkty");

            migrationBuilder.DropForeignKey(
                name: "FK_Produkty_Producenci_ProducentId",
                table: "Produkty");

            migrationBuilder.DropForeignKey(
                name: "FK_Recenzje_Produkty_ProduktId",
                table: "Recenzje");

            migrationBuilder.DropIndex(
                name: "IX_Recenzje_ProduktId",
                table: "Recenzje");

            migrationBuilder.DropIndex(
                name: "IX_Produkty_KategoriaId",
                table: "Produkty");

            migrationBuilder.DropIndex(
                name: "IX_Produkty_ProducentId",
                table: "Produkty");

            migrationBuilder.DropColumn(
                name: "KategoriaId",
                table: "Produkty");

            migrationBuilder.DropColumn(
                name: "ProducentId",
                table: "Produkty");

            migrationBuilder.AlterColumn<string>(
                name: "Tresc",
                table: "Recenzje",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
