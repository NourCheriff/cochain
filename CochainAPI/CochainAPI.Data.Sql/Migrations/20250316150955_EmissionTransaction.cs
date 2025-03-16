using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CochainAPI.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class EmissionTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmissionTransactionId",
                table: "ProductLifeCycle",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmissionProcessed",
                table: "ProductLifeCycle",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EmissionTransactionId",
                table: "CarbonOffsettingAction",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "CarbonOffsettingAction",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmissionTransactionId",
                table: "ProductLifeCycle");

            migrationBuilder.DropColumn(
                name: "IsEmissionProcessed",
                table: "ProductLifeCycle");

            migrationBuilder.DropColumn(
                name: "EmissionTransactionId",
                table: "CarbonOffsettingAction");

            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "CarbonOffsettingAction");

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
    }
}
