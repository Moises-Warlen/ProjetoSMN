using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioSMN.Infra.Migrations
{
    public partial class AdicinandoPropiedadeNamodeltarefa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CriadorId",
                table: "Tarefas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriadorId",
                table: "Tarefas");
        }
    }
}
