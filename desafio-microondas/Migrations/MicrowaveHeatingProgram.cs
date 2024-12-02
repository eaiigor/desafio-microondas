using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace desafio_microondas.Migrations
{
    public partial class MicrowaveHeatingProgram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHeatingProgram",
                table: "Microwave",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHeatingProgram",
                table: "Microwave");
        }
    }
}
