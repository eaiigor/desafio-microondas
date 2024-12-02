using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace desafio_microondas.Migrations
{
    public partial class MicrowaveTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MicroWaves",
                table: "MicroWaves");

            migrationBuilder.RenameTable(
                name: "MicroWaves",
                newName: "Microwave");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Microwave",
                table: "Microwave",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Microwave",
                table: "Microwave");

            migrationBuilder.RenameTable(
                name: "Microwave",
                newName: "MicroWaves");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MicroWaves",
                table: "MicroWaves",
                column: "Id");
        }
    }
}
