using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CamposDealer_React.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VendaProdutos",
                table: "VendaProdutos");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "VendaProdutos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VendaProdutos",
                table: "VendaProdutos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VendaProdutos_VendaId",
                table: "VendaProdutos",
                column: "VendaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VendaProdutos",
                table: "VendaProdutos");

            migrationBuilder.DropIndex(
                name: "IX_VendaProdutos_VendaId",
                table: "VendaProdutos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VendaProdutos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VendaProdutos",
                table: "VendaProdutos",
                column: "VendaId");
        }
    }
}
