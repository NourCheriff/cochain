using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CochainAPI.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CertificationAuthority_CompanyType_CompanyTypeId",
                table: "CertificationAuthority");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_User_UserEmitterId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDocument_User_UserEmitterId",
                table: "ProductDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductIngredient_Product_IngredientId",
                table: "ProductIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLifeCycleDocument_User_UserEmitterId",
                table: "ProductLifeCycleDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplyChainPartner_CompanyType_CompanyTypeId",
                table: "SupplyChainPartner");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplyChainPartnerCertificate_User_UserEmitterId",
                table: "SupplyChainPartnerCertificate");

            migrationBuilder.DropTable(
                name: "CompanyType");

            migrationBuilder.DropIndex(
                name: "IX_SupplyChainPartner_CompanyTypeId",
                table: "SupplyChainPartner");

            migrationBuilder.DropIndex(
                name: "IX_CertificationAuthority_CompanyTypeId",
                table: "CertificationAuthority");

            migrationBuilder.DropColumn(
                name: "CompanyTypeId",
                table: "SupplyChainPartner");

            migrationBuilder.DropColumn(
                name: "CompanyTypeId",
                table: "CertificationAuthority");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmitterId",
                table: "SupplyChainPartnerCertificate",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "SupplyChainPartnerCertificate",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "QualityStandard",
                table: "SupplyChainPartnerCertificate",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "SupplyChainPartnerCertificate",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmitterId",
                table: "ProductLifeCycleDocument",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ProductLifeCycleDocument",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "ProductLifeCycleDocument",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmitterId",
                table: "ProductDocument",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ProductDocument",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "ProductDocument",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmitterId",
                table: "Contract",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Contract",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Contract",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"),
                column: "Name",
                value: "Trasportatore");

            migrationBuilder.InsertData(
                table: "SupplyChainPartnerType",
                columns: new[] { "Id", "Baseline", "Name" },
                values: new object[,]
                {
                    { new Guid("ab2e7db4-760e-4515-9aa0-bda314266e87"), 1000f, "Stoccaggio" },
                    { new Guid("e1117db4-760e-4515-9aa0-11a3fa766e87"), 1000f, "Materia Prima" },
                    { new Guid("eaae7124-761e-4515-9aa0-bda3fc7aee87"), 1000f, "Grossista" },
                    { new Guid("ef01b3b4-760e-4515-9aa0-bdab7c766e87"), 1000f, "Trasformazione" },
                    { new Guid("ef0e7124-744e-1115-9ba0-bda3fc766e87"), 1000f, "Rivenditore Dettaglio" }
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_User_UserEmitterId",
                table: "Contract",
                column: "UserEmitterId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDocument_User_UserEmitterId",
                table: "ProductDocument",
                column: "UserEmitterId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIngredient_ProductInfo_IngredientId",
                table: "ProductIngredient",
                column: "IngredientId",
                principalTable: "ProductInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLifeCycleDocument_User_UserEmitterId",
                table: "ProductLifeCycleDocument",
                column: "UserEmitterId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplyChainPartnerCertificate_User_UserEmitterId",
                table: "SupplyChainPartnerCertificate",
                column: "UserEmitterId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_User_UserEmitterId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDocument_User_UserEmitterId",
                table: "ProductDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductIngredient_ProductInfo_IngredientId",
                table: "ProductIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLifeCycleDocument_User_UserEmitterId",
                table: "ProductLifeCycleDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplyChainPartnerCertificate_User_UserEmitterId",
                table: "SupplyChainPartnerCertificate");

            migrationBuilder.DeleteData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("ab2e7db4-760e-4515-9aa0-bda314266e87"));

            migrationBuilder.DeleteData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("e1117db4-760e-4515-9aa0-11a3fa766e87"));

            migrationBuilder.DeleteData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("eaae7124-761e-4515-9aa0-bda3fc7aee87"));

            migrationBuilder.DeleteData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("ef01b3b4-760e-4515-9aa0-bdab7c766e87"));

            migrationBuilder.DeleteData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("ef0e7124-744e-1115-9ba0-bda3fc766e87"));

            migrationBuilder.AlterColumn<string>(
                name: "UserEmitterId",
                table: "SupplyChainPartnerCertificate",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "SupplyChainPartnerCertificate",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QualityStandard",
                table: "SupplyChainPartnerCertificate",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "SupplyChainPartnerCertificate",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyTypeId",
                table: "SupplyChainPartner",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "UserEmitterId",
                table: "ProductLifeCycleDocument",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ProductLifeCycleDocument",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "ProductLifeCycleDocument",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserEmitterId",
                table: "ProductDocument",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ProductDocument",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "ProductDocument",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserEmitterId",
                table: "Contract",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Contract",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Contract",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyTypeId",
                table: "CertificationAuthority",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CompanyType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CompanyType",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("6173d450-c48a-4f24-82f6-f012413ff6f4"), "SCP" });

            migrationBuilder.UpdateData(
                table: "SupplyChainPartner",
                keyColumn: "Id",
                keyValue: new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"),
                column: "CompanyTypeId",
                value: new Guid("6173d450-c48a-4f24-82f6-f012413ff6f4"));

            migrationBuilder.UpdateData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"),
                column: "Name",
                value: "Trasporto");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "139f339d-0d98-474b-bd5b-44e2854bb25f", "bd31288c-06df-4691-8700-4cbc2444a482" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8f82f99a-b553-478f-ae6b-bcbf2dfb6692", "1ad06333-d3b9-48c2-8bc6-7e9ca97ec186" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5b5d162b-c36c-43f7-83f3-1fe56247f549", "acd64430-4aad-4e6e-bc11-2ac9090b6974" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6cd6f153-c1e0-480b-a577-3b77fee235c9", "e0792042-4c4f-4700-bd28-ec8ee4b64b80" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "fa335c4b-340e-4fd2-a53e-7be897b94dd1", "d19f6e36-df8d-4c25-b5d1-8dc9a5d3132e" });

            migrationBuilder.CreateIndex(
                name: "IX_SupplyChainPartner_CompanyTypeId",
                table: "SupplyChainPartner",
                column: "CompanyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificationAuthority_CompanyTypeId",
                table: "CertificationAuthority",
                column: "CompanyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CertificationAuthority_CompanyType_CompanyTypeId",
                table: "CertificationAuthority",
                column: "CompanyTypeId",
                principalTable: "CompanyType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_User_UserEmitterId",
                table: "Contract",
                column: "UserEmitterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDocument_User_UserEmitterId",
                table: "ProductDocument",
                column: "UserEmitterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIngredient_Product_IngredientId",
                table: "ProductIngredient",
                column: "IngredientId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLifeCycleDocument_User_UserEmitterId",
                table: "ProductLifeCycleDocument",
                column: "UserEmitterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplyChainPartner_CompanyType_CompanyTypeId",
                table: "SupplyChainPartner",
                column: "CompanyTypeId",
                principalTable: "CompanyType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplyChainPartnerCertificate_User_UserEmitterId",
                table: "SupplyChainPartnerCertificate",
                column: "UserEmitterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
