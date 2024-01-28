using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.TasteEase.Infra.Migrations
{
    /// <inheritdoc />
    public partial class add_payment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "order_payment",
                schema: "taste_ease",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    paid = table.Column<bool>(type: "boolean", nullable: false),
                    reference = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    payment_link = table.Column<string>(type: "character varying(4098)", maxLength: 4098, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    paid_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_payment_order_order_id",
                        column: x => x.order_id,
                        principalSchema: "taste_ease",
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_payment_order_id",
                schema: "taste_ease",
                table: "order_payment",
                column: "order_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_payment",
                schema: "taste_ease");
        }
    }
}
