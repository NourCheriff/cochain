using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CochainAPI.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AddNewUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "34422b5c-b3e1-4ffb-a321-de3ccab0cada", "5fb4d314-e02d-4a78-abef-bf05da265125" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cd7f1236-33f1-4489-a354-9170e95da819", "a5020b4e-d48c-47b3-9f8e-fb3d43e755a1" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e445913f-6b14-4cc0-a65d-e1f60e614c42", "b8ad517e-8b6b-434d-ac96-576191593644" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "409a2c5c-d8b9-4d40-813c-79138edea40b", "bac82434-4b95-4031-96a6-f3ec8cb08eb9" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "ad00648b-a031-432d-b007-6a0829cf5292",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3ccbcfd9-634c-4a0d-b0f4-0df8d2974ca1", "80ce3e6e-0639-4976-b833-a5abfb8a241f" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6ce8c006-f8b9-4e71-a45a-dc8fb99e2b7b", "637b179c-7507-4943-9e4c-ddc3a664edc5" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "CertificationAuthorityId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "SupplyChainPartnerId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5a6c9915-bdca-4a68-b452-da1f4e8b422f", 0, null, "c4f6f1db-1555-4300-8735-319e6d68eba2", null, false, "Luca", true, "Spalazzi", false, null, null, null, null, null, null, false, null, "ce0eb992-89ee-4df6-9a60-4c5a995bceac", new Guid("81124c04-840a-49c1-8929-073af4cee139"), false, "l.spalazzi@staff.univpm.it" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5a6c9915-bdca-4a68-b452-da1f4e8b422f");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bfe17f0c-209e-470b-ba35-dc1431f1db47", "86dcc932-f1ac-4ea5-948f-599dd9f46e8d" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "953bd6bb-b165-469b-bcbd-beb4ff6e58ec", "3b41d70a-1f5b-43ac-ad78-57f6fdf1c781" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8865b237-5a12-4d3f-8567-42d2c02a6d08", "459cae55-031d-4c6c-86f4-f8b7e72f7b5c" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "10e233a2-bae8-462f-a5be-3e9888ac2038", "81a59af1-a3e3-4138-b87c-ce59343c5846" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "ad00648b-a031-432d-b007-6a0829cf5292",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c326c424-dcef-457d-9ca4-ae1f5124bc42", "0f8abf78-3649-425e-b19f-90016bba0c29" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "20372d8c-d553-4809-bf21-2ff20ac9a5fa", "19f2827a-d1e0-41a3-af16-3d8f3d636263" });
        }
    }
}
