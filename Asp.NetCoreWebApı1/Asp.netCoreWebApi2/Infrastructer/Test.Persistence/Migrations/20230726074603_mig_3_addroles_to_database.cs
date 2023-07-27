using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Test.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_3_addroles_to_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
