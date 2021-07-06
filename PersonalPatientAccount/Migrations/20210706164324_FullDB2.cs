using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalPatientAccount.Migrations
{
    public partial class FullDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Outpatient_cards",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "Outpatient_cards");
        }
    }
}
