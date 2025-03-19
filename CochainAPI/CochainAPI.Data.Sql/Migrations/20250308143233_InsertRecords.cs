using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CochainAPI.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class InsertRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User");

            migrationBuilder.DeleteData(
                table: "UserTemporaryPassword",
                keyColumn: "Id",
                keyValue: new Guid("1ff10a28-9a6d-41c3-a06b-627998b4f56a"));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "SecurityStamp", "UserName" },
                values: new object[] { "5b5d162b-c36c-43f7-83f3-1fe56247f549", null, "Paolo", "Roselli", "acd64430-4aad-4e6e-bc11-2ac9090b6974", "paolo.roselli02@gmail.com" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "CertificationAuthorityId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "SupplyChainPartnerId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0a317b04-2f87-4b08-8ad8-597103527584", 0, null, "139f339d-0d98-474b-bd5b-44e2854bb25f", null, false, "Matteo", true, "Spiga", false, null, null, null, null, null, null, false, null, "bd31288c-06df-4691-8700-4cbc2444a482", new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"), false, "matteospiga2002@gmail.com" },
                    { "3542da56-0de3-4797-a059-effff257f63d", 0, null, "8f82f99a-b553-478f-ae6b-bcbf2dfb6692", null, false, "Mattia", true, "Mandorlini", false, null, null, null, null, null, null, false, null, "1ad06333-d3b9-48c2-8bc6-7e9ca97ec186", new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"), false, "mando3228@gmail.com" },
                    { "a12c3708-0486-4603-b1a5-46d252e79082", 0, null, "6cd6f153-c1e0-480b-a577-3b77fee235c9", null, false, "Cherif", true, "Nour", false, null, null, null, null, null, null, false, null, "e0792042-4c4f-4700-bd28-ec8ee4b64b80", new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"), false, "nourcherif.pitos25@gmail.com" },
                    { "f4242b5f-4b39-45fc-802e-391293414546", 0, null, "fa335c4b-340e-4fd2-a53e-7be897b94dd1", null, false, "Samuele", true, "Sacchetti", false, null, null, null, null, null, null, false, null, "d19f6e36-df8d-4c25-b5d1-8dc9a5d3132e", new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"), false, "sacchettisamuele@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "8e342ad6-78d9-4aee-abe5-245b1fae6c4a", "0a317b04-2f87-4b08-8ad8-597103527584" },
                    { "8e342ad6-78d9-4aee-abe5-245b1fae6c4a", "3542da56-0de3-4797-a059-effff257f63d" },
                    { "8e342ad6-78d9-4aee-abe5-245b1fae6c4a", "a12c3708-0486-4603-b1a5-46d252e79082" },
                    { "8e342ad6-78d9-4aee-abe5-245b1fae6c4a", "f4242b5f-4b39-45fc-802e-391293414546" }
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User",
                sql: "(\"SupplyChainPartnerId\" IS NOT NULL AND \"CertificationAuthorityId\" IS NULL)\r\n                    OR (\"SupplyChainPartnerId\" IS NULL AND \"CertificationAuthorityId\" IS NOT NULL)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8e342ad6-78d9-4aee-abe5-245b1fae6c4a", "0a317b04-2f87-4b08-8ad8-597103527584" });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8e342ad6-78d9-4aee-abe5-245b1fae6c4a", "3542da56-0de3-4797-a059-effff257f63d" });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8e342ad6-78d9-4aee-abe5-245b1fae6c4a", "a12c3708-0486-4603-b1a5-46d252e79082" });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8e342ad6-78d9-4aee-abe5-245b1fae6c4a", "f4242b5f-4b39-45fc-802e-391293414546" });

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "SecurityStamp", "UserName" },
                values: new object[] { "b94e20dc-fc57-414f-81d3-78ed52bd4be5", "System", "System", "System", "fa39f5f4-9b52-4a21-8730-eab3be5fe1a7", "System" });

            migrationBuilder.InsertData(
                table: "UserTemporaryPassword",
                columns: new[] { "Id", "ExpirationDate", "IsUsed", "Password", "UserId" },
                values: new object[] { new Guid("1ff10a28-9a6d-41c3-a06b-627998b4f56a"), new DateTime(2027, 3, 8, 12, 3, 48, 288, DateTimeKind.Utc).AddTicks(3906), false, "System", "5e4b0ca8-aa85-417a-af23-035ac1b555cd" });

            migrationBuilder.AddCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User",
                sql: "\"SupplyChainPartnerId\" IS NOT NULL OR \"CertificationAuthorityId\" IS NOT NULL");
        }
    }
}
