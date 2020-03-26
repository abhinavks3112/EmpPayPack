using Microsoft.EntityFrameworkCore.Migrations;

namespace EmpPayPack.Persistence.Migrations
{
    public partial class MakeTotalEarningsDeductionsAndNetPayColumnNonNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalEarnings",
                table: "PaymentRecords",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalDeductions",
                table: "PaymentRecords",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "NetPayment",
                table: "PaymentRecords",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalEarnings",
                table: "PaymentRecords",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalDeductions",
                table: "PaymentRecords",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "NetPayment",
                table: "PaymentRecords",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");
        }
    }
}
