using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CochainAPI.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                table: "Log",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "QueryString",
                table: "Log",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Cookies",
                table: "Log",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0359f305-34a5-4f35-ba06-4179c5dae900", "d11406f3-efd9-459a-a127-2fe2d65b61e9" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ca09e44b-dab2-456f-8e6b-debe64745571", "7dac4623-6f34-4c79-9a16-f6eb80d64005" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "316c1607-43b8-4f93-96ab-6ce31cdefd99", "967b5046-f7e9-43a3-b913-54dd86b546b6" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9f782138-3a9d-4423-b004-92dbd920ec20", "8a304382-1b10-463d-b8ef-f6d483bb7bd5" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "157bb555-d6a2-4a06-a928-9b74ebb9cb88", "f8b77f53-8c75-4c76-bcdd-b49343dee146" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "CertificationAuthorityId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "SupplyChainPartnerId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ad00648b-a031-432d-b007-6a0829cf5292", 0, null, "85d445b3-6c45-440a-91b6-4ef479d799a6", null, false, "System", true, "System", false, null, null, null, null, null, null, false, null, "b22f9512-bcff-4f97-b5dc-208a5e40db15", new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"), false, "admin@cochain.com" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8e342ad6-78d9-4aee-abe5-245b1fae6c4a", "ad00648b-a031-432d-b007-6a0829cf5292" });

            migrationBuilder.AddCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User",
                sql: "(\"SupplyChainPartnerId\" IS NOT NULL AND \"CertificationAuthorityId\" IS NULL)\n                    OR (\"SupplyChainPartnerId\" IS NULL AND \"CertificationAuthorityId\" IS NOT NULL)");
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
                keyValues: new object[] { "8e342ad6-78d9-4aee-abe5-245b1fae6c4a", "ad00648b-a031-432d-b007-6a0829cf5292" });

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "ad00648b-a031-432d-b007-6a0829cf5292");

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                table: "Log",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QueryString",
                table: "Log",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cookies",
                table: "Log",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "14538df9-eff1-49b0-87bf-c8b097167ba1", "a7ba693b-93a8-47f4-b3a7-7eb18cb181d5" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f97094ca-a26e-49b6-8bf8-949a8c13e912", "73861d6c-6a19-46c5-92b4-a6d1527b1aff" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "29c57c37-62df-45ef-9c17-6f2dee08e6eb", "fa9baacb-fad0-400c-bb6d-039fd74f4f27" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d9c313f2-39dd-4986-b54a-370db69b4279", "a496e98f-b6f1-4961-8c41-0ea8d20267da" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d58ebf1b-2fae-4bff-ae54-fa42aeb55d19", "6c098cdb-2122-49c2-ab93-169cfe3cb0ec" });

            migrationBuilder.AddCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User",
                sql: "(\"SupplyChainPartnerId\" IS NOT NULL AND \"CertificationAuthorityId\" IS NULL)\r\n                    OR (\"SupplyChainPartnerId\" IS NULL AND \"CertificationAuthorityId\" IS NOT NULL)");
        }
    }
}
