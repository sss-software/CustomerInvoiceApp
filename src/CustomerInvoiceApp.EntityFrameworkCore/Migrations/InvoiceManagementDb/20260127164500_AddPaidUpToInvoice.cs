using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerInvoiceApp.Migrations.InvoiceManagementDb
{
    /// <inheritdoc />
    public partial class AddPaidUpToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PaidUp",
                table: "Invoices",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidUp",
                table: "Invoices");
        }
    }
}
