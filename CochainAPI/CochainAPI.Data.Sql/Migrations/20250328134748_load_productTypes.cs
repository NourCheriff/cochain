using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CochainAPI.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class load_productTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("111aaa22-bb33-cc44-dd55-ee66ff778899"), new Guid("a1b2c3d4-e5f6-7890-ab12-cd34ef567890"), "Boneless, skinless chicken breast.", "Chicken Breast" },
                    { new Guid("222bbb33-cc44-dd55-ee66-ff778899aabb"), new Guid("b2c3d4e5-f678-9012-abcd-34ef56789012"), "Fresh Atlantic salmon fillet.", "Salmon Fillet" },
                    { new Guid("333ccc44-dd55-ee66-ff77-8899aabbccdd"), new Guid("c3d4e5f6-7890-1234-abcd-56ef78901234"), "Organic fresh carrots.", "Carrots" },
                    { new Guid("444ddd55-ee66-ff77-8899-aabbccddeeff"), new Guid("d4e5f678-9012-3456-abcd-78ef90123456"), "Sweet and ripe bananas.", "Bananas" },
                    { new Guid("555eee66-ff77-8899-aabb-ccddeeff0011"), new Guid("e5f67890-1234-5678-abcd-90ef12345678"), "Pasteurized whole milk.", "Whole Milk" },
                    { new Guid("666fff77-8899-aabb-ccdd-eeff00112233"), new Guid("f6789012-3456-7890-abcd-12ef34567890"), "Traditional French bread.", "Baguette" },
                    { new Guid("77711188-999a-bbcc-ddee-ff0011223344"), new Guid("78901234-5678-9012-abcd-34ef56789012"), "Italian durum wheat spaghetti.", "Spaghetti" },
                    { new Guid("88822299-aabb-ccdd-eeff-001122334455"), new Guid("89012345-6789-0123-abcd-56ef78901234"), "100% fresh squeezed orange juice.", "Orange Juice" },
                    { new Guid("999333aa-bbcc-ddee-ff00-112233445566"), new Guid("90123456-7890-1234-abcd-78ef90123456"), "Creamy milk chocolate bar.", "Milk Chocolate Bar" },
                    { new Guid("aaa444bb-ccdd-eeff-0011-223344556677"), new Guid("12345678-9012-3456-abcd-90ef12345678"), "Extra virgin olive oil.", "Olive Oil" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "55e9bde7-31b8-47bc-832b-acde6638cbb9", "bef6ede0-94ec-422f-a46a-9ae7ae77c416" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e8cd186c-afa8-42b7-ba91-9d2806011508", "23a00c64-cf84-40c1-a60a-efd69748aaf1" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0c88b83e-7337-423f-9962-2b91561da18b", "2fbe9e94-0541-40f2-ba3c-42bec2cc3239" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e5d3c406-14bf-47cc-b2d2-e6a0a1be1f9c", "00f3c321-cc17-4402-a101-8292956b46b3" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "ad00648b-a031-432d-b007-6a0829cf5292",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "529dfb39-677c-44d5-b79a-c00f8e46e105", "77b76eb3-6192-4dbc-a87b-9a0030113a57" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "eccedfa3-6bca-433d-b21c-acea13ebc8ce", "f555eede-1f29-404c-9fd4-f600cc657bc8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("111aaa22-bb33-cc44-dd55-ee66ff778899"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("222bbb33-cc44-dd55-ee66-ff778899aabb"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("333ccc44-dd55-ee66-ff77-8899aabbccdd"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("444ddd55-ee66-ff77-8899-aabbccddeeff"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("555eee66-ff77-8899-aabb-ccddeeff0011"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("666fff77-8899-aabb-ccdd-eeff00112233"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("77711188-999a-bbcc-ddee-ff0011223344"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("88822299-aabb-ccdd-eeff-001122334455"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("999333aa-bbcc-ddee-ff00-112233445566"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("aaa444bb-ccdd-eeff-0011-223344556677"));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "03d0508b-8c51-4fe2-8d8c-5151b25e702a", "8a239603-6acb-442e-9f77-e3a68f219b2d" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bfb91710-780f-477c-81b9-3af0cdcddcaa", "185c1747-ab0f-4e54-957f-51d17c3574ce" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "be5f762d-ce5d-4a78-af91-acb1986d9dab", "e79710b5-038c-4cd9-b672-5ddfcc6e3902" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3fda29f1-81bc-4473-a232-71fcaf96e4a1", "04cd7d49-106b-41d2-a7b3-6744b144015e" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "ad00648b-a031-432d-b007-6a0829cf5292",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cf37881c-5f29-45b4-a479-4f1ef8f44ae3", "0c995831-81fa-4451-8ecb-73341dba50e4" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d68fd0ac-0425-42fe-bca6-3550a11b0f5c", "c86e4389-e859-404e-aa46-2efc742d91d6" });
        }
    }
}
