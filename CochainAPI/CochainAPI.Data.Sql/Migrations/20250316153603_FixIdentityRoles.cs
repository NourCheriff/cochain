using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CochainAPI.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class FixIdentityRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3318013d-0cdd-4749-9ab3-6fdca9b64564",
                column: "NormalizedName",
                value: "USERCA");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8e342ad6-78d9-4aee-abe5-245b1fae6c4a",
                column: "NormalizedName",
                value: "SYSTEMADMIN");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "916a3160-05e5-4821-88fe-9e46a43d157c",
                column: "NormalizedName",
                value: "SCPTRANSPORTER");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a0e8b03e-0cd8-4458-a147-1a4b88df2997",
                column: "NormalizedName",
                value: "USERSCP");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "cb5b1ae5-43db-4096-9bd6-2afb90fb20c5",
                column: "NormalizedName",
                value: "ADMINSCP");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "cccf28ca-b2f8-477f-a2c1-2436cd83ec0c",
                column: "NormalizedName",
                value: "SCPRAWMATERIAL");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f08d90db-ac61-4c92-a229-ef803b672e60",
                column: "NormalizedName",
                value: "SCPTRANSFORMATOR");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f6e7ea1d-a99e-4a5c-9a23-2274ba2c62ea",
                column: "NormalizedName",
                value: "ADMINCA");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3318013d-0cdd-4749-9ab3-6fdca9b64564",
                column: "NormalizedName",
                value: "User Certification Authority");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8e342ad6-78d9-4aee-abe5-245b1fae6c4a",
                column: "NormalizedName",
                value: "System Administrator");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "916a3160-05e5-4821-88fe-9e46a43d157c",
                column: "NormalizedName",
                value: "Supply Chain Partner Transporter");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a0e8b03e-0cd8-4458-a147-1a4b88df2997",
                column: "NormalizedName",
                value: "User Supply Chain Partner");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "cb5b1ae5-43db-4096-9bd6-2afb90fb20c5",
                column: "NormalizedName",
                value: "Admin Supply Chain Partner");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "cccf28ca-b2f8-477f-a2c1-2436cd83ec0c",
                column: "NormalizedName",
                value: "Supply Chain Partner Raw Material");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f08d90db-ac61-4c92-a229-ef803b672e60",
                column: "NormalizedName",
                value: "Supply Chain Partner Transformator");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f6e7ea1d-a99e-4a5c-9a23-2274ba2c62ea",
                column: "NormalizedName",
                value: "Admin Certification Authority");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c5ea026a-d48a-45c1-bf50-b2017c28411b", "71d4b6b0-a07f-4148-8d40-d26b9c49ef1e" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1de02694-b1ba-4033-87ba-5c1fe516f422", "be205751-7a25-42ab-8533-392859863d3b" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ee2706db-04cb-433f-beea-1553b12b4c85", "5e95be0c-0665-4c5e-ae24-364a2656fef5" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c8bc3767-0cfa-416f-b781-322df2fd0a1c", "b4c7d4a0-41a0-4025-8a43-0b22efdda954" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "88a1d738-3df4-4f02-953a-30d4bb6f244f", "8a395fa8-fedd-4477-abee-7e10e5ad2aa7" });

            migrationBuilder.AddCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User",
                sql: "(\"SupplyChainPartnerId\" IS NOT NULL AND \"CertificationAuthorityId\" IS NULL)\r\n                    OR (\"SupplyChainPartnerId\" IS NULL AND \"CertificationAuthorityId\" IS NOT NULL)");
        }
    }
}
