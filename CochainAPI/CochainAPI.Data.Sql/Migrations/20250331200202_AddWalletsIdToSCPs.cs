using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CochainAPI.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AddWalletsIdToSCPs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_SupplyChainPartner_SupplyChainPartnerId",
                table: "User");

            migrationBuilder.DropCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User");

            migrationBuilder.DeleteData(
                table: "SupplyChainPartner",
                keyColumn: "Id",
                keyValue: new Guid("3a9f1b7c-5d2e-4a4f-8a6c-0e7d3b5a2f9c"));

            migrationBuilder.DeleteData(
                table: "SupplyChainPartner",
                keyColumn: "Id",
                keyValue: new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"));

            migrationBuilder.InsertData(
                table: "SupplyChainPartner",
                columns: new[] { "Id", "Credits", "Email", "Name", "Phone", "SupplyChainPartnerTypeId", "WalletId" },
                values: new object[,]
                {
                    { new Guid("3a9f1b7c-5d2e-4a4f-8a6c-0e7d3b5a2f9c"), 40f, "raw.material.supplier@test.com", "Raw Material Supplier", "3669045897", new Guid("e1117db4-760e-4515-9aa0-11a3fa766e87"), "0x627306090abab3a6e1400e9345bc60c78a8bef57" },
                    { new Guid("81124c04-840a-49c1-8929-073af4cee139"), 100f, "company@test.com", "Test Company", "33309090909", new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"), "0xfe3b557e8fb62b89f4916b721be55ceb828dbd73" },
                    { new Guid("db2c2af0-5227-4d3c-b3eb-daf45118aeff"), 20f, "retailer@test.com", "Retailer", "3669045897", new Guid("eaae7124-761e-4515-9aa0-bda3fc7aee87"), "0xf17f52151ebef6c7334fad080c5704d77216b732" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "SupplyChainPartnerId" },
                values: new object[] { "bfe17f0c-209e-470b-ba35-dc1431f1db47", "86dcc932-f1ac-4ea5-948f-599dd9f46e8d", new Guid("81124c04-840a-49c1-8929-073af4cee139") });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "SupplyChainPartnerId" },
                values: new object[] { "953bd6bb-b165-469b-bcbd-beb4ff6e58ec", "3b41d70a-1f5b-43ac-ad78-57f6fdf1c781", new Guid("81124c04-840a-49c1-8929-073af4cee139") });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "SupplyChainPartnerId" },
                values: new object[] { "8865b237-5a12-4d3f-8567-42d2c02a6d08", "459cae55-031d-4c6c-86f4-f8b7e72f7b5c", new Guid("81124c04-840a-49c1-8929-073af4cee139") });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "SupplyChainPartnerId" },
                values: new object[] { "10e233a2-bae8-462f-a5be-3e9888ac2038", "81a59af1-a3e3-4138-b87c-ce59343c5846", new Guid("81124c04-840a-49c1-8929-073af4cee139") });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "ad00648b-a031-432d-b007-6a0829cf5292",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "SupplyChainPartnerId" },
                values: new object[] { "c326c424-dcef-457d-9ca4-ae1f5124bc42", "0f8abf78-3649-425e-b19f-90016bba0c29", new Guid("81124c04-840a-49c1-8929-073af4cee139") });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "SupplyChainPartnerId" },
                values: new object[] { "20372d8c-d553-4809-bf21-2ff20ac9a5fa", "19f2827a-d1e0-41a3-af16-3d8f3d636263", new Guid("81124c04-840a-49c1-8929-073af4cee139") });

            migrationBuilder.AddCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User",
                sql: "(\"SupplyChainPartnerId\" IS NOT NULL AND \"CertificationAuthorityId\" IS NULL)\n                    OR (\"SupplyChainPartnerId\" IS NULL AND \"CertificationAuthorityId\" IS NOT NULL)");

            migrationBuilder.AddForeignKey(
                name: "FK_User_SupplyChainPartner_SupplyChainPartnerId",
                table: "User",
                column: "SupplyChainPartnerId",
                principalTable: "SupplyChainPartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_SupplyChainPartner_SupplyChainPartnerId",
                table: "User");

            migrationBuilder.DropCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User");

            migrationBuilder.DeleteData(
                table: "SupplyChainPartner",
                keyColumn: "Id",
                keyValue: new Guid("3a9f1b7c-5d2e-4a4f-8a6c-0e7d3b5a2f9c"));

            migrationBuilder.DeleteData(
                table: "SupplyChainPartner",
                keyColumn: "Id",
                keyValue: new Guid("81124c04-840a-49c1-8929-073af4cee139"));

            migrationBuilder.DeleteData(
                table: "SupplyChainPartner",
                keyColumn: "Id",
                keyValue: new Guid("db2c2af0-5227-4d3c-b3eb-daf45118aeff"));

            migrationBuilder.InsertData(
                table: "SupplyChainPartner",
                columns: new[] { "Id", "Credits", "Email", "Name", "Phone", "SupplyChainPartnerTypeId", "WalletId" },
                values: new object[,]
                {
                    { new Guid("3a9f1b7c-5d2e-4a4f-8a6c-0e7d3b5a2f9c"), 0f, "company2@prova.com", "Prova company2", "3669045897", new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"), "0x7c5d1a3f9b2e6f0d8c4a7e3b5c2f9d1" },
                    { new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"), 0f, "company@prova.com", "Prova company", "33309090909", new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"), "0x3a9f1b7c5d2e8a4f6c0e7d3b5a2f9c1" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "SupplyChainPartnerId" },
                values: new object[] { "55e9bde7-31b8-47bc-832b-acde6638cbb9", "bef6ede0-94ec-422f-a46a-9ae7ae77c416", new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095") });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "SupplyChainPartnerId" },
                values: new object[] { "e8cd186c-afa8-42b7-ba91-9d2806011508", "23a00c64-cf84-40c1-a60a-efd69748aaf1", new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095") });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "SupplyChainPartnerId" },
                values: new object[] { "0c88b83e-7337-423f-9962-2b91561da18b", "2fbe9e94-0541-40f2-ba3c-42bec2cc3239", new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095") });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "SupplyChainPartnerId" },
                values: new object[] { "e5d3c406-14bf-47cc-b2d2-e6a0a1be1f9c", "00f3c321-cc17-4402-a101-8292956b46b3", new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095") });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "ad00648b-a031-432d-b007-6a0829cf5292",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "SupplyChainPartnerId" },
                values: new object[] { "529dfb39-677c-44d5-b79a-c00f8e46e105", "77b76eb3-6192-4dbc-a87b-9a0030113a57", new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095") });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "SupplyChainPartnerId" },
                values: new object[] { "eccedfa3-6bca-433d-b21c-acea13ebc8ce", "f555eede-1f29-404c-9fd4-f600cc657bc8", new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095") });

            migrationBuilder.AddCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User",
                sql: "(\"SupplyChainPartnerId\" IS NOT NULL AND \"CertificationAuthorityId\" IS NULL)\r\n                    OR (\"SupplyChainPartnerId\" IS NULL AND \"CertificationAuthorityId\" IS NOT NULL)");

            migrationBuilder.AddForeignKey(
                name: "FK_User_SupplyChainPartner_SupplyChainPartnerId",
                table: "User",
                column: "SupplyChainPartnerId",
                principalTable: "SupplyChainPartner",
                principalColumn: "Id");
        }
    }
}
