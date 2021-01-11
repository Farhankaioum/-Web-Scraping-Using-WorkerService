using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DhakaStockExchangeWorker.Migrations
{
    public partial class DSEModelEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DSEModels",
                columns: table => new
                {
                    SerialNo = table.Column<Guid>(nullable: false),
                    Id = table.Column<long>(nullable: false),
                    TrandingCode = table.Column<string>(nullable: false),
                    LTP = table.Column<double>(nullable: false),
                    High = table.Column<double>(nullable: false),
                    Low = table.Column<double>(nullable: false),
                    Closep = table.Column<double>(nullable: false),
                    YCP = table.Column<double>(nullable: false),
                    Change = table.Column<double>(nullable: false),
                    Trade = table.Column<long>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    Volume = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DSEModels", x => x.SerialNo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DSEModels");
        }
    }
}
