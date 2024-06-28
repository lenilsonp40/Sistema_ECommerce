using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class produtos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ECommerce_TB002_Produtos",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ImagemUrl = table.Column<string>(type: "varchar(200)", nullable: false),
                    Estoque = table.Column<double>(type: "float", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECommerce_TB002_Produtos", x => x.ProdutoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ECommerce_TB002_Produtos_ProdutoId",
                table: "ECommerce_TB002_Produtos",
                column: "ProdutoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ECommerce_TB002_Produtos");
        }
    }
}
