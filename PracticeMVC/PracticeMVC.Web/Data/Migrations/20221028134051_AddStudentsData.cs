using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeMVC.Web.Data.Migrations
{
    public partial class AddStudentsData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "Cgpa", "Name" },
                values: new object[,]
                {
                    { new Guid("2a8681cb-92c6-4c5c-97f7-5a08833fe6e9"), "Rangpur", 3.5, "Student 2" },
                    { new Guid("3a2df08f-9801-4af8-bce5-bb5eb8937188"), "Dhaka", 3.7000000000000002, "Student 3" },
                    { new Guid("6a969bc5-3f01-4444-90c9-696b230f48ea"), "Dhaka", 3.0, "Student 1" },
                    { new Guid("d2831a6e-cffe-4a82-849d-a0db9800a741"), "Natore", 3.5, "Student 4" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("2a8681cb-92c6-4c5c-97f7-5a08833fe6e9"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("3a2df08f-9801-4af8-bce5-bb5eb8937188"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("6a969bc5-3f01-4444-90c9-696b230f48ea"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("d2831a6e-cffe-4a82-849d-a0db9800a741"));
        }
    }
}
