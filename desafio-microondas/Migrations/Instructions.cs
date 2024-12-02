using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace desafio_microondas.Migrations
{
    public partial class Instructions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Instructions",
                table: "HeatingProgram",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instructions",
                table: "HeatingProgram");
        }
    }
}
