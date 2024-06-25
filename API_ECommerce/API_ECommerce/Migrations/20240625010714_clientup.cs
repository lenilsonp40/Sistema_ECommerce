using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class clientup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ECommerce_TB001_Clientes",
                newName: "ClienteID");

            migrationBuilder.RenameIndex(
                name: "IX_ECommerce_TB001_Clientes_ID",
                table: "ECommerce_TB001_Clientes",
                newName: "IX_ECommerce_TB001_Clientes_ClienteID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClienteID",
                table: "ECommerce_TB001_Clientes",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_ECommerce_TB001_Clientes_ClienteID",
                table: "ECommerce_TB001_Clientes",
                newName: "IX_ECommerce_TB001_Clientes_ID");
        }
    }
}
