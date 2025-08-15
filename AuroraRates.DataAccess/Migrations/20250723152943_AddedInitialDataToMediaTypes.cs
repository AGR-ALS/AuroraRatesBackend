using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuroraRates.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedInitialDataToMediaTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "MediaTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("21d3f992-10e2-4f25-aa2d-2c05fb0cace8"), "Series" },
                    { new Guid("af156cc2-379f-44e0-b730-d98c2796ad73"), "Game" },
                    { new Guid("c8349eec-7435-4f2c-891f-edea77817c81"), "Music" },
                    { new Guid("ee9fced5-20b5-4c1d-852c-a0214eb6a458"), "Movie" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Nickname",
                table: "Users",
                column: "Nickname",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Nickname",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "MediaTypes",
                keyColumn: "Id",
                keyValue: new Guid("21d3f992-10e2-4f25-aa2d-2c05fb0cace8"));

            migrationBuilder.DeleteData(
                table: "MediaTypes",
                keyColumn: "Id",
                keyValue: new Guid("af156cc2-379f-44e0-b730-d98c2796ad73"));

            migrationBuilder.DeleteData(
                table: "MediaTypes",
                keyColumn: "Id",
                keyValue: new Guid("c8349eec-7435-4f2c-891f-edea77817c81"));

            migrationBuilder.DeleteData(
                table: "MediaTypes",
                keyColumn: "Id",
                keyValue: new Guid("ee9fced5-20b5-4c1d-852c-a0214eb6a458"));

            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
