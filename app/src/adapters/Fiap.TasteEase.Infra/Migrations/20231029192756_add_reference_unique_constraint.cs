using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.TasteEase.Infra.Migrations
{
    /// <inheritdoc />
    public partial class add_reference_unique_constraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_order_payment_reference",
                schema: "taste_ease",
                table: "order_payment",
                column: "reference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_order_payment_reference",
                schema: "taste_ease",
                table: "order_payment");
        }
    }
}
