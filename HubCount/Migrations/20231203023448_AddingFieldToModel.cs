using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HubCount.Migrations
{
    public partial class AddingFieldToModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Regiao",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Regiao",
                table: "Pedidos");
        }
    }
}
