using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EnterpriseProject.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "14b1c451-e118-4a5b-aa50-fb867502a925");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 2, 0, "cead51ae-4e60-4b44-b205-780ba199556d", "doc@example.com", false, "Doctor", "Smith", false, null, "DOC@EXAMPLE.COM", "DOC@EXAMPLE.COM", null, null, false, null, false, "doc@example.com" },
                    { 3, 0, "1f5d26ac-ac4c-4a32-9035-3a5a8ed7ef68", "billing@example.com", false, "Bill", "Johnson", false, null, "BILLING@EXAMPLE.COM", "BILLING@EXAMPLE.COM", null, null, false, null, false, "billing@example.com" },
                    { 4, 0, "53765dc3-cc0b-46ed-b2b9-8d16d78571d5", "client@example.com", false, "Client", "User", false, null, "CLIENT@EXAMPLE.COM", "CLIENT@EXAMPLE.COM", null, null, false, null, false, "client@example.com" }
                });

            migrationBuilder.UpdateData(
                table: "Practitioners",
                keyColumn: "PractitionerId",
                keyValue: 1,
                column: "UserId",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a357738f-3d46-4f20-9c0c-26c8e2e17c9b");

            migrationBuilder.UpdateData(
                table: "Practitioners",
                keyColumn: "PractitionerId",
                keyValue: 1,
                column: "UserId",
                value: 1);
        }
    }
}
