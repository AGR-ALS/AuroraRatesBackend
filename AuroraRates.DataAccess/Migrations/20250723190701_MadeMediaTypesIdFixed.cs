using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuroraRates.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MadeMediaTypesIdFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "MediaTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("75f391d8-4f36-4346-8574-9db7515598f9"), "Game" },
                    { new Guid("7dc36adb-99f3-49d7-ae19-5dc35fbc9b4d"), "Music" },
                    { new Guid("e9956f89-e020-4ae4-a190-f95d4afa5788"), "Movie" },
                    { new Guid("ee0c7412-c291-4adc-b503-02746c3a60d9"), "Series" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MediaTypes",
                keyColumn: "Id",
                keyValue: new Guid("75f391d8-4f36-4346-8574-9db7515598f9"));

            migrationBuilder.DeleteData(
                table: "MediaTypes",
                keyColumn: "Id",
                keyValue: new Guid("7dc36adb-99f3-49d7-ae19-5dc35fbc9b4d"));

            migrationBuilder.DeleteData(
                table: "MediaTypes",
                keyColumn: "Id",
                keyValue: new Guid("e9956f89-e020-4ae4-a190-f95d4afa5788"));

            migrationBuilder.DeleteData(
                table: "MediaTypes",
                keyColumn: "Id",
                keyValue: new Guid("ee0c7412-c291-4adc-b503-02746c3a60d9"));

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
        }
    }
}
