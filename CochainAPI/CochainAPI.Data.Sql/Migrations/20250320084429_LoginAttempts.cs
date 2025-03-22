using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CochainAPI.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class LoginAttempts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "Attempts",
                table: "UserTemporaryPassword",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Attempts",
                table: "UserTemporaryPassword");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b9c15a89-2582-4664-95e3-fe9553e3bb15", "fc849eb3-e235-4e5a-b7b3-07f29c8b13ce" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "589d7fc2-a4af-4534-a583-5388c1d31280", "ba6cfcfa-304c-4849-8c71-bcd8bebfd00a" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "616ff929-70ad-443a-a157-d53f26a40ac0", "37def6ff-400f-4b52-995a-ec71d3ffeacc" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6b7b727b-8134-4270-9966-f2dc4332ccf5", "d0958d8e-0d03-4c49-a94a-c4fa2383013a" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "28fdc8c5-0039-46aa-9bdd-b84adb99f293", "46935ed2-16e5-4b5e-825d-d26b74933ab6" });

            migrationBuilder.AddCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User",
                sql: "(\"SupplyChainPartnerId\" IS NOT NULL AND \"CertificationAuthorityId\" IS NULL)\n                    OR (\"SupplyChainPartnerId\" IS NULL AND \"CertificationAuthorityId\" IS NOT NULL)");
        }
    }
}
