using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace desafio_microondas.Migrations
{
    public partial class HeatingProgram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeatingProgram",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Time = table.Column<long>(type: "INTEGER", nullable: false),
                    Power = table.Column<long>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MicrowaveId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeatingProgram", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeatingProgram_Microwave_MicrowaveId",
                        column: x => x.MicrowaveId,
                        principalTable: "Microwave",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeatingProgram_MicrowaveId",
                table: "HeatingProgram",
                column: "MicrowaveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeatingProgram");
        }
    }
}
