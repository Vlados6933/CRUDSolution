using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTINColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_TIM",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "TaxIdentificationNumber",
                table: "Persons");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonID",
                keyValue: new Guid("89e5f445-d89f-4e12-94e0-5ad5b235d704"),
                column: "Gender",
                value: "Male");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CountryID",
                table: "Persons",
                column: "CountryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Countries_CountryID",
                table: "Persons",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Countries_CountryID",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_CountryID",
                table: "Persons");

            migrationBuilder.AddColumn<string>(
                name: "TaxIdentificationNumber",
                table: "Persons",
                type: "varchar(8)",
                nullable: true,
                defaultValue: "ABC12345");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonID",
                keyValue: new Guid("012107df-862f-4f16-ba94-e5c16886f005"),
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonID",
                keyValue: new Guid("28d11936-9466-4a4b-b9c5-2f0a8e0cbde9"),
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonID",
                keyValue: new Guid("29339209-63f5-492f-8459-754943c74abf"),
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonID",
                keyValue: new Guid("2a6d3738-9def-43ac-9279-0310edc7ceca"),
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonID",
                keyValue: new Guid("89e5f445-d89f-4e12-94e0-5ad5b235d704"),
                column: "Gender",
                value: "Gender");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonID",
                keyValue: new Guid("a3b9833b-8a4d-43e9-8690-61e08df81a9a"),
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonID",
                keyValue: new Guid("ac660a73-b0b7-4340-abc1-a914257a6189"),
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonID",
                keyValue: new Guid("c03bbe45-9aeb-4d24-99e0-4743016ffce9"),
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonID",
                keyValue: new Guid("c3abddbd-cf50-41d2-b6c4-cc7d5a750928"),
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonID",
                keyValue: new Guid("c6d50a47-f7e6-4482-8be0-4ddfc057fa6e"),
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonID",
                keyValue: new Guid("cb035f22-e7cf-4907-bd07-91cfee5240f3"),
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "PersonID",
                keyValue: new Guid("d15c6d9f-70b4-48c5-afd3-e71261f1a9be"),
                columns: new string[0],
                values: new object[0]);

            migrationBuilder.AddCheckConstraint(
                name: "CHK_TIM",
                table: "Persons",
                sql: "len([TaxIdentificationNumber]) = 8");
        }
    }
}
