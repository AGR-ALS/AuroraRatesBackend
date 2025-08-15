using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuroraRates.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedMediaType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MediaTypeId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MediaTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MediaTypeId",
                table: "Reviews",
                column: "MediaTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_MediaTypes_MediaTypeId",
                table: "Reviews",
                column: "MediaTypeId",
                principalTable: "MediaTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_MediaTypes_MediaTypeId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "MediaTypes");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_MediaTypeId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "MediaTypeId",
                table: "Reviews");
        }
    }
}
