using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class clients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ECommerce_TB001_Clientes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    CPF = table.Column<string>(type: "char(11)", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECommerce_TB001_Clientes", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ECommerce_TB001_Clientes_ID",
                table: "ECommerce_TB001_Clientes",
                column: "ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ECommerce_TB001_Clientes");
        }
    }
}
