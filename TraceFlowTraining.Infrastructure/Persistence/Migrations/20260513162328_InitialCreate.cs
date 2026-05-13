using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraceFlowTraining.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_PRODUCTS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    NAME = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SKU = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    QUANTITY = table.Column<int>(type: "integer", nullable: false),
                    PRICE = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PRODUCTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_WAREHOUSE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    NAME = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LOCATION = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CAPACITY = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_WAREHOUSE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_STOCK_MOVEMENTS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PRODUCT_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    QUANTITY = table.Column<int>(type: "integer", nullable: false),
                    TYPE = table.Column<int>(type: "integer", nullable: false),
                    TIMESTAMP = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PRODUCT_ID1 = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_STOCK_MOVEMENTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TB_STOCK_MOVEMENTS_TB_PRODUCTS_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "TB_PRODUCTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_STOCK_MOVEMENTS_PRODUCT_ID",
                table: "TB_STOCK_MOVEMENTS",
                column: "PRODUCT_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_STOCK_MOVEMENTS");

            migrationBuilder.DropTable(
                name: "TB_WAREHOUSE");

            migrationBuilder.DropTable(
                name: "TB_PRODUCTS");
        }
    }
}
