using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace codefirst.Migrations
{
    /// <inheritdoc />
    public partial class Adddefaultdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "IdDoctor", "Email", "FirstName", "LastName" },
                values: new object[] { 1, "albert.dough@doctor.com", "Albert", "Dough" });

            migrationBuilder.InsertData(
                table: "Medicament",
                columns: new[] { "Id", "Details", "Name", "Type" },
                values: new object[] { 1, "Medicament for asthma", "Ventolin", "Non-steroid" });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "IdPatient", "BirthDate", "FirstName", "LastName" },
                values: new object[] { 1, new DateTime(2002, 3, 2, 4, 21, 21, 0, DateTimeKind.Unspecified), "Boris", "Kabul" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 1);
        }
    }
}
