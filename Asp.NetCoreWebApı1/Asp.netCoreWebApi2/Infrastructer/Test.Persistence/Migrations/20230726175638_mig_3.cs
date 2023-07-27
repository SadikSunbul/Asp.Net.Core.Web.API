using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Test.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5531b9d3-3d77-4eb0-8804-ee69af95b119");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7dece90-90fa-4782-95ec-529707e205ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc99fbef-7d17-405f-b953-e276df845a28");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTie",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7db44dd3-2df2-4ebf-80b1-60bc76789459", "31d68595-b41b-48f1-8745-c6679ceefcbd", "User", "USER" },
                    { "a4b1cfd6-13a7-45f2-a7a9-253ade2f73c2", "a4f2d96f-b87c-48e2-a583-9437add273c6", "Admin", "ADMIN" },
                    { "a568d456-9449-470d-b19b-959da834ee76", "e9cc0cb6-9d26-43f6-823b-ab0762ba2263", "Editor", "EDITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7db44dd3-2df2-4ebf-80b1-60bc76789459");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4b1cfd6-13a7-45f2-a7a9-253ade2f73c2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a568d456-9449-470d-b19b-959da834ee76");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTie",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5531b9d3-3d77-4eb0-8804-ee69af95b119", "71c38cd8-8f6f-4256-a61e-e6024a54a1df", "Editor", "EDITOR" },
                    { "d7dece90-90fa-4782-95ec-529707e205ee", "7d91b093-4511-45fe-8257-af85cd798463", "Admin", "ADMIN" },
                    { "dc99fbef-7d17-405f-b953-e276df845a28", "c11b2eda-a0d0-4c52-b434-b212ff3f38aa", "User", "USER" }
                });
        }
    }
}
