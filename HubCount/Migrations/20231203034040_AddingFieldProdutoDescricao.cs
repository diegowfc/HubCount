using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubCount.Migrations
{
    public partial class AddingFieldProdutoDescricao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProdutoDescricao",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProdutoDescricao",
                table: "Pedidos");
        }
    }
}
