using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CochainAPI.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class FixEnvironment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "37405a69-34a9-464d-bb90-80c11d125afc", "20f41d95-4661-4b46-9daf-3bd8798ed937" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1b5318a6-5168-45ba-a1ce-71d4defa4149", "05b33175-5ab0-4a42-8696-69e1307a1f9f" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1a9f3bab-d803-4674-a3db-f8fecd961909", "527312f8-0c37-4107-b835-9c7693a3df78" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c844b808-1114-4ccd-be89-6692debf9961", "78b45438-07ac-4624-9c71-5467d95127f0" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "ad00648b-a031-432d-b007-6a0829cf5292",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7c847829-33da-4929-be42-e54d3509efa9", "d3cf83b0-8ec5-4d3d-951f-35af9282128e" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "435599aa-177e-4abc-b522-babb60e2f572", "e19a4db3-2525-4e1a-8e16-01dd157c51ae" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e6414826-9c27-40dd-9a42-7f511091bb78", "87b65384-3c80-488b-93da-f571593c05e2" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ce7439e9-e866-4006-9af3-d1dacecdb75d", "16af89b8-1442-40e8-9687-b9824371f370" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7f185257-ff78-4734-a1b3-9c3c7d02be72", "2bf190e9-045a-4639-9d64-d26c6eec84dd" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d24348d9-4cf3-457b-85a8-550f5deb0164", "65d88542-315e-4999-8d2c-a22615a10c58" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "ad00648b-a031-432d-b007-6a0829cf5292",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e475cd48-c416-4542-819f-3dfb9aec7b16", "da77e533-dcd4-4adb-9be7-d7e9349f0f86" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1ee352e4-4a1f-4526-beee-162d80fe01f8", "d01cd4e3-f34e-4ad4-9ea9-ac1cf95a2a09" });
        }
    }
}
