using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.TasteEase.Infra.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "taste_ease");

            migrationBuilder.CreateTable(
                name: "client",
                schema: "taste_ease",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    taxpayer_number = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "food",
                schema: "taste_ease",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order",
                schema: "taste_ease",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    status = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_client_client_id",
                        column: x => x.client_id,
                        principalSchema: "taste_ease",
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_food",
                schema: "taste_ease",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    food_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_food", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_food_food_food_id",
                        column: x => x.food_id,
                        principalSchema: "taste_ease",
                        principalTable: "food",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_food_order_order_id",
                        column: x => x.order_id,
                        principalSchema: "taste_ease",
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_client_id",
                schema: "taste_ease",
                table: "order",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_food_food_id",
                schema: "taste_ease",
                table: "order_food",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_food_order_id",
                schema: "taste_ease",
                table: "order_food",
                column: "order_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_food",
                schema: "taste_ease");

            migrationBuilder.DropTable(
                name: "food",
                schema: "taste_ease");

            migrationBuilder.DropTable(
                name: "order",
                schema: "taste_ease");

            migrationBuilder.DropTable(
                name: "client",
                schema: "taste_ease");
        }
    }
}
