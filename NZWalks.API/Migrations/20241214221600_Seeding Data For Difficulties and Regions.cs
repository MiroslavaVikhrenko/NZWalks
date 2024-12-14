using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataForDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("46bc4828-7685-488d-b826-5ed16efbca6c"), "Medium" },
                    { new Guid("5ec8e717-d5be-4a45-b71f-01cc9820eee5"), "Hard" },
                    { new Guid("d7219cdc-4e85-417d-97c6-93783b9ac5d5"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("0777347e-0ead-472f-92c9-710d8202d862"), "WGN", "Wellington", "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("68547f6d-8c96-4ba4-b1ac-ae95ea8316b9"), "BOP", "Bay Of Plenty", null },
                    { new Guid("895ae5a8-a93a-4a56-a2ed-98496cdeb148"), "NSN", "Nelson", "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("a0630efb-f69d-4329-9511-232f47c3784f"), "AKL", "Auckland", "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" },
                    { new Guid("f1856a5e-28ed-42ef-b186-29cd10a40238"), "STL", "Southland", null },
                    { new Guid("f96449cf-dc46-4cf9-9f25-b4b47100f799"), "NTL", "Northland", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("46bc4828-7685-488d-b826-5ed16efbca6c"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("5ec8e717-d5be-4a45-b71f-01cc9820eee5"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("d7219cdc-4e85-417d-97c6-93783b9ac5d5"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("0777347e-0ead-472f-92c9-710d8202d862"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("68547f6d-8c96-4ba4-b1ac-ae95ea8316b9"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("895ae5a8-a93a-4a56-a2ed-98496cdeb148"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a0630efb-f69d-4329-9511-232f47c3784f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f1856a5e-28ed-42ef-b186-29cd10a40238"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f96449cf-dc46-4cf9-9f25-b4b47100f799"));
        }
    }
}
