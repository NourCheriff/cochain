using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CochainAPI.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class message : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {           
            migrationBuilder.AlterColumn<string>(
                name: "WalletId",
                table: "SupplyChainPartner",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "SupplyChainPartner",
                keyColumn: "Id",
                keyValue: new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"),
                columns: new[] { "WalletId" },
                values: new object[] { "0x3a9f1b7c5d2e8a4f6c0e7d3b5a2f9c1" });

            migrationBuilder.AlterColumn<string>(
                name: "EmissionTransactionId",
                table: "ProductLifeCycle",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SupplyChainPartner_WalletId",
                table: "SupplyChainPartner",
                column: "WalletId");

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionHash = table.Column<string>(type: "text", nullable: false),
                    WalletIdEmitter = table.Column<string>(type: "text", nullable: false),
                    WalletIdReceiver = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionHash);
                    table.ForeignKey(
                        name: "FK_Transaction_SupplyChainPartner_WalletIdEmitter",
                        column: x => x.WalletIdEmitter,
                        principalTable: "SupplyChainPartner",
                        principalColumn: "WalletId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_SupplyChainPartner_WalletIdReceiver",
                        column: x => x.WalletIdReceiver,
                        principalTable: "SupplyChainPartner",
                        principalColumn: "WalletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SupplyChainPartner",
                columns: new[] { "Id", "Credits", "Email", "Name", "Phone", "SupplyChainPartnerTypeId", "WalletId" },
                values: new object[,]
                {
                    { new Guid("3a9f1b7c-5d2e-4a4f-8a6c-0e7d3b5a2f9c"), 0f, "company2@prova.com", "Prova company2", "3669045897", new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"), "0x7c5d1a3f9b2e6f0d8c4a7e3b5c2f9d1" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2f36f264-2573-4f9e-94f6-c57241703bff", "ea85b501-2140-4bab-8f0c-fc690eef7281" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "85ec0148-dcf9-43d4-adb0-fd2d10d8f117", "c1b53c11-c9f4-4f53-83d6-9c38c9f86ac7" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b325f8dc-7511-4b9b-8e1b-7d98f3f77ae7", "73717c3a-32e6-4a09-a111-6873f18d0c72" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "199b5fbd-2dbf-4905-9e93-a82f4b236fd4", "0ea49741-e211-4322-b2d8-8d2dd001d775" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ffccd05a-e029-45ab-8adc-f85c30a7294d", "1646fd10-886c-4262-b349-32be3b22be34" });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_WalletIdEmitter",
                table: "Transaction",
                column: "WalletIdEmitter");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_WalletIdReceiver",
                table: "Transaction",
                column: "WalletIdReceiver");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_SupplyChainPartner_WalletId",
                table: "SupplyChainPartner");

            migrationBuilder.DeleteData(
                table: "SupplyChainPartner",
                keyColumn: "Id",
                keyValue: new Guid("3a9f1b7c-5d2e-4a4f-8a6c-0e7d3b5a2f9c"));

            migrationBuilder.AlterColumn<string>(
                name: "WalletId",
                table: "SupplyChainPartner",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "EmissionTransactionId",
                table: "ProductLifeCycle",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "SupplyChainPartner",
                keyColumn: "Id",
                keyValue: new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"),
                column: "WalletId",
                value: null);

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
        }
    }
}
