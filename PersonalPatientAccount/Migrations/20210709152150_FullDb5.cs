using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalPatientAccount.Migrations
{
    public partial class FullDb5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Outpatient_cards_Doctors_patientid",
                table: "Outpatient_cards");

            migrationBuilder.AddForeignKey(
                name: "FK_Outpatient_cards_Patients_patientid",
                table: "Outpatient_cards",
                column: "patientid",
                principalTable: "Patients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Outpatient_cards_Patients_patientid",
                table: "Outpatient_cards");

            migrationBuilder.AddForeignKey(
                name: "FK_Outpatient_cards_Doctors_patientid",
                table: "Outpatient_cards",
                column: "patientid",
                principalTable: "Doctors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
