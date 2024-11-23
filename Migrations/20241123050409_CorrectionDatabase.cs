using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestoStockDB1.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPlatos_Ingredientes_ingredienteId",
                table: "DetallesPlatos");

            migrationBuilder.RenameColumn(
                name: "ingredienteId",
                table: "DetallesPlatos",
                newName: "IngredienteId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesPlatos_ingredienteId",
                table: "DetallesPlatos",
                newName: "IX_DetallesPlatos_IngredienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPlatos_Ingredientes_IngredienteId",
                table: "DetallesPlatos",
                column: "IngredienteId",
                principalTable: "Ingredientes",
                principalColumn: "IngredienteId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPlatos_Ingredientes_IngredienteId",
                table: "DetallesPlatos");

            migrationBuilder.RenameColumn(
                name: "IngredienteId",
                table: "DetallesPlatos",
                newName: "ingredienteId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesPlatos_IngredienteId",
                table: "DetallesPlatos",
                newName: "IX_DetallesPlatos_ingredienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPlatos_Ingredientes_ingredienteId",
                table: "DetallesPlatos",
                column: "ingredienteId",
                principalTable: "Ingredientes",
                principalColumn: "IngredienteId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
