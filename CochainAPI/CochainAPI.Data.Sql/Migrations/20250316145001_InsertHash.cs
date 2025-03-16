using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CochainAPI.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class InsertHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "SupplyChainPartnerCertificate",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "ProductLifeCycleDocument",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "ProductDocument",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "Contract",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c6ed9301-c93c-4c55-bbf7-d3e990a19df7", "4470464e-1418-499e-a268-ed55f86bde28" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "93adc910-f361-42fa-ab3a-9099c225f3e2", "d680b303-6377-48d3-ae8a-eaff4ac72299" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "05252f9f-5081-4409-9bb3-dbcee371abc0", "73ac03e0-557f-493e-aeae-f2a55241acbe" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "db8c8777-6c1c-4bc0-82c6-fb058c3bc872", "cbb791ff-aed8-4fc5-b016-dc000d8718fb" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "53f4335e-638b-4f83-a311-864f26570b36", "3d673bbb-a9f6-43c7-8162-b61365e878a1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hash",
                table: "SupplyChainPartnerCertificate");

            migrationBuilder.DropColumn(
                name: "Hash",
                table: "ProductLifeCycleDocument");

            migrationBuilder.DropColumn(
                name: "Hash",
                table: "ProductDocument");

            migrationBuilder.DropColumn(
                name: "Hash",
                table: "Contract");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1cee7157-2b99-4c68-98c5-9febacb1f62c", "6c0c5e6d-0774-4564-a395-358efb4205ea" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "61c6af4e-f98d-4bf8-8133-5584f0701c00", "b39bf647-f3c7-4b51-a000-d1186cbf32b5" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "101bd15b-31b8-43f5-a839-9f4926f9b32d", "6ae56f79-d2b6-4259-8bfc-cdb702fab30d" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "93ccd881-dbbe-404e-8cf9-337be05b1875", "337396b7-0223-4b88-bc70-1047eb1595c0" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "da75566a-778b-44ca-b77d-ca65b9eca250", "5da343ab-6f08-4ba2-bbf7-ea70780440c1" });
        }
    }
}
