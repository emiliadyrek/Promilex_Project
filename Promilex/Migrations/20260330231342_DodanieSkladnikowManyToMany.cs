using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Promilex.Migrations
{
    /// <inheritdoc />
    public partial class DodanieSkladnikowManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkladnikId",
                table: "Produkty",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Skladniki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skladniki", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produkty_Skladniki_SkladnikId",
                table: "Produkty");

            migrationBuilder.DropTable(
                name: "Skladniki");

            migrationBuilder.DropIndex(
                name: "IX_Produkty_SkladnikId",
                table: "Produkty");

            migrationBuilder.DropColumn(
                name: "SkladnikId",
                table: "Produkty");
        }
    }
}
