using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace desafio_microondas.Migrations
{
    public partial class MicrowavePower : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Power",
                table: "Microwave",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Power",
                table: "Microwave");
        }
    }
}
