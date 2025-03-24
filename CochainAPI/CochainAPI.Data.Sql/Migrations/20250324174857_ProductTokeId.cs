using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CochainAPI.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class ProductTokeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TokenId",
                table: "ProductInfo",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a39e3c59-3be8-419d-bb4c-6825fa2dd08b", "0b1c7fa0-e2fc-49c5-9382-10b33124e1c7" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f584cfc3-00ce-48d6-b190-73694e1d13d7", "70f29dff-5274-4614-a854-f51cd295f41f" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2a7a3984-13ed-4c6a-9115-7158c9e6ef47", "7c8cc965-021d-4996-bb7f-512c05ca7746" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1954d3ca-34b6-4d76-8e0f-73fd2f27bac4", "513fdbbf-5350-4ca8-8b9e-93bd25d50e99" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "ad00648b-a031-432d-b007-6a0829cf5292",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bc7e4820-7af6-4a10-9661-59c7fc12c296", "7718bb67-ba21-4e4c-b0bd-c34176b9162f" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d1466892-9fd3-4b6a-9de7-bdbd705641be", "df05452d-33a8-4500-a875-b6d864294245" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenId",
                table: "ProductInfo");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ed39c1f5-f184-4417-abd5-10171df59d52", "46f38c01-cf77-44ea-a862-5fef17c0f2ff" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "12497287-6bfd-45ac-89c7-afa21dc7795a", "833972ff-40af-47ad-a4a5-a35b39e8d4f0" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "06e67265-7ecf-4ca5-8653-d680478a892d", "99a66675-8247-43c1-a3c5-1e78919e065c" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "397732a5-62d5-407d-ac16-95b9181f3b6e", "679ba12b-add5-4344-bab3-3df177e1ee1f" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "ad00648b-a031-432d-b007-6a0829cf5292",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8a7b76df-4ae0-4dc0-969f-863011236e33", "d7a75005-24b2-4260-b878-8b9caddc4898" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e42261bc-c140-4bbe-aaa9-15c91da9ce75", "248a261d-be12-4741-aeec-0df20cef4698" });
        }
    }
}
