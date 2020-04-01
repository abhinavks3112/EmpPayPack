using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpPayPack.Persistence.Migrations
{
    public partial class MakeIndexOnTaxYearIdColumnInPaymentRecordsTableNotUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PaymentRecords_TaxYearId",
                table: "PaymentRecords");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRecords_TaxYearId",
                table: "PaymentRecords",
                column: "TaxYearId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PaymentRecords_TaxYearId",
                table: "PaymentRecords");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRecords_TaxYearId",
                table: "PaymentRecords",
                column: "TaxYearId",
                unique: true);
        }
    }
}
