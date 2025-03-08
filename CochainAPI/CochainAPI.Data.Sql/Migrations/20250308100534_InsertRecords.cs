using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CochainAPI.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class InsertRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contract",
                keyColumn: "Id",
                keyValue: new Guid("c7467f1b-4739-48ea-9fcd-4762fb5090e1"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("fc02d0d2-1368-40e5-aed3-3389032f4de0"));

            migrationBuilder.DeleteData(
                table: "ProductInfo",
                keyColumn: "Id",
                keyValue: new Guid("3c06df94-1fff-4f54-b188-8326203e2a89"));

            migrationBuilder.DeleteData(
                table: "ProductLifeCycle",
                keyColumn: "Id",
                keyValue: new Guid("7de440aa-63d8-4a97-87b1-79781f7b8349"));

            migrationBuilder.DeleteData(
                table: "UserTemporaryPassword",
                keyColumn: "Id",
                keyValue: new Guid("09f67853-ff83-440c-9ab1-c6de458c6e42"));

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumn: "Id",
                keyValue: new Guid("a895a940-cd9f-42c4-a898-34f7bb5e513e"));

            migrationBuilder.DeleteData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("7de440aa-63d8-4a97-87b1-79781f7b8349"));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "79008f4b-c6fd-4ce7-94be-9a47b34bad19", "7e3e855f-7991-4fee-99e7-c0ccb41d3d4d" });

            migrationBuilder.InsertData(
                table: "UserTemporaryPassword",
                columns: new[] { "Id", "ExpirationDate", "IsUsed", "Password", "UserId" },
                values: new object[] { new Guid("b48206b7-52c7-43bc-967c-82db2ac8e226"), new DateTime(2027, 3, 8, 10, 5, 33, 374, DateTimeKind.Utc).AddTicks(8109), false, "System", "5e4b0ca8-aa85-417a-af23-035ac1b555cd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserTemporaryPassword",
                keyColumn: "Id",
                keyValue: new Guid("b48206b7-52c7-43bc-967c-82db2ac8e226"));

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("a895a940-cd9f-42c4-a898-34f7bb5e513e"), "Categoria del prodotto di prova", null });

            migrationBuilder.InsertData(
                table: "ProductInfo",
                columns: new[] { "Id", "ExpirationDate", "Name", "ProductId", "SupplyChainPartnerId" },
                values: new object[] { new Guid("3c06df94-1fff-4f54-b188-8326203e2a89"), new DateOnly(1, 1, 1), null, new Guid("20b4dadf-3bff-428c-a026-b509e6620cc0"), new Guid("9c389eba-e008-4030-a544-010232e3bbe2") });

            migrationBuilder.InsertData(
                table: "ProductLifeCycle",
                columns: new[] { "Id", "Emissions", "Name", "ProductInfoId", "ProductLifeCycleCategoryId", "SupplyChainPartnerId", "Timestamp" },
                values: new object[] { new Guid("7de440aa-63d8-4a97-87b1-79781f7b8349"), 0f, null, new Guid("20b4dadf-3bff-428c-a026-b509e6620cc0"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "ProductLifeCycleCategory",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("7de440aa-63d8-4a97-87b1-79781f7b8349"), "Categoria attività di prova", null });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "86280c4e-30e4-478e-bc4e-d9ffe6a22b71", "4da68d6c-ecdf-4f7a-9852-57f45a96057e" });

            migrationBuilder.InsertData(
                table: "UserTemporaryPassword",
                columns: new[] { "Id", "ExpirationDate", "IsUsed", "Password", "UserId" },
                values: new object[] { new Guid("09f67853-ff83-440c-9ab1-c6de458c6e42"), new DateTime(2027, 3, 5, 16, 28, 57, 362, DateTimeKind.Utc).AddTicks(330), false, "System", "5e4b0ca8-aa85-417a-af23-035ac1b555cd" });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "Name", "Path", "ProductLifeCycleCategoryId", "SupplyChainPartnerReceiverId", "Type", "UserEmitterId" },
                values: new object[] { new Guid("c7467f1b-4739-48ea-9fcd-4762fb5090e1"), null, "/home/contract1.pdf", new Guid("7de440aa-63d8-4a97-87b1-79781f7b8349"), new Guid("00000000-0000-0000-0000-000000000000"), "Contract", "5e4b0ca8-aa85-417a-af23-035ac1b555cd" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CategoryId", "Description", "Name" },
                values: new object[] { new Guid("fc02d0d2-1368-40e5-aed3-3389032f4de0"), new Guid("a895a940-cd9f-42c4-a898-34f7bb5e513e"), "Prodotto di prova", null });
        }
    }
}
