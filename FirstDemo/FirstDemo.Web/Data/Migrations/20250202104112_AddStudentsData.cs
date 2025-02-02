using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FirstDemo.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "Cgpa", "Name" },
                values: new object[,]
                {
                    { new Guid("18587525-9ecc-42e1-bf5a-511cc1fa4a0e"), "Natore", 3.5, "Student 4" },
                    { new Guid("92fb217d-8026-4ebb-bce9-631e08576c5c"), "Dhaka", 3.7000000000000002, "Student 3" },
                    { new Guid("ccc3c3a7-c923-4505-90fe-0a4f4355187d"), "Dhaka", 3.0, "Student 1" },
                    { new Guid("d786c18d-9c46-4afb-b117-95076d2bd435"), "Rangpur", 3.5, "Student 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("18587525-9ecc-42e1-bf5a-511cc1fa4a0e"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("92fb217d-8026-4ebb-bce9-631e08576c5c"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("ccc3c3a7-c923-4505-90fe-0a4f4355187d"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d786c18d-9c46-4afb-b117-95076d2bd435"));
        }
    }
}
