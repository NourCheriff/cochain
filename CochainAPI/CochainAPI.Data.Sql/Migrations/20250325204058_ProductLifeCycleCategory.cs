using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CochainAPI.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class ProductLifeCycleCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_SupplyChainPartner_SupplyChainPartnerReceiverId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDocument_SupplyChainPartner_SupplyChainPartnerReceiv~",
                table: "ProductDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLifeCycleDocument_SupplyChainPartner_SupplyChainPart~",
                table: "ProductLifeCycleDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplyChainPartnerCertificate_SupplyChainPartner_SupplyChai~",
                table: "SupplyChainPartnerCertificate");

            migrationBuilder.DropCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User");

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplyChainPartnerReceiverId",
                table: "SupplyChainPartnerCertificate",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplyChainPartnerReceiverId",
                table: "ProductLifeCycleDocument",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "EmissionTransactionId",
                table: "ProductLifeCycle",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplyChainPartnerReceiverId",
                table: "ProductDocument",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplyChainPartnerReceiverId",
                table: "Contract",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.InsertData(
                table: "ProductLifeCycleCategory",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("a4c2d7e6-6f5e-42f9-b7c1-1234567890ef"), "Implementazione di sistemi di irrigazione efficienti per ridurre il consumo idrico e l'energia necessaria.", "Irrigazione Sostenibile" },
                    { new Guid("a9d12b5f-1e2d-45c9-bb5d-3d8a7c2b4a33"), "Attività di produzione del prodotto.", "Produzione" },
                    { new Guid("b1e2d3f4-5678-90ab-cdef-1234567890ab"), "Attività di lavorazione della materia prima.", "Lavorazione" },
                    { new Guid("b5d3e8f7-7e6f-43d0-c8d2-0987654321ba"), "Utilizzo di fertilizzanti naturali o a rilascio controllato per minimizzare le emissioni di gas serra.", "Fertilizzazione a Basso Impatto" },
                    { new Guid("c1d2e3f4-6789-0abc-def1-234567890abc"), "Attività di vendita del prodotto.", "Vendita" },
                    { new Guid("c6e4f9a8-8f70-44e1-d9e3-1029384756cd"), "Adozione di pratiche integrate per il controllo dei parassiti, riducendo l'uso di pesticidi chimici e l'impatto ambientale.", "Gestione dei Parassiti" },
                    { new Guid("d2e3f4a5-7890-1bcd-ef12-34567890abcd"), "Attività di assistenza post vendita.", "Assistenza" },
                    { new Guid("d7f50ab9-9a81-45f2-eaf4-5647382910ef"), "Processi di raccolta ottimizzati per minimizzare il consumo energetico e le emissioni dovute al trasporto interno.", "Raccolta" },
                    { new Guid("e3a18178-8db7-48f2-a76b-9ad329bba5f2"), "Attività di aratura e lavorazione del suolo, con tecniche volte a minimizzare l'uso di macchinari pesanti per ridurre le emissioni.", "Preparazione del Terreno" },
                    { new Guid("e8a61bca-ab92-46f3-fb05-6758493021f0"), "Attività di selezione, lavaggio e conservazione con tecniche a basso impatto energetico per mantenere la qualità del prodotto.", "Post-Raccolta e Conservazione" },
                    { new Guid("ef94c672-c755-449b-8ee8-327a12bed7ef"), "Trasporto del prodotto.", "Trasporto" },
                    { new Guid("f3b19128-0edc-4f59-8a27-6a8d3509876c"), "Attività di semina utilizzando metodi di precisione per ottimizzare l'utilizzo di risorse e ridurre l'impatto ambientale.", "Semina" },
                    { new Guid("f9b72cdb-bc03-47f4-0c16-78695a4132f1"), "Utilizzo di materiali riciclabili e processi a basso impatto per ridurre la carbon footprint del packaging.", "Imballaggio Eco-Sostenibile" }
                });

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

            migrationBuilder.AddCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User",
                sql: "(\"SupplyChainPartnerId\" IS NOT NULL AND \"CertificationAuthorityId\" IS NULL)\r\n                    OR (\"SupplyChainPartnerId\" IS NULL AND \"CertificationAuthorityId\" IS NOT NULL)");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_SupplyChainPartner_SupplyChainPartnerReceiverId",
                table: "Contract",
                column: "SupplyChainPartnerReceiverId",
                principalTable: "SupplyChainPartner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDocument_SupplyChainPartner_SupplyChainPartnerReceiv~",
                table: "ProductDocument",
                column: "SupplyChainPartnerReceiverId",
                principalTable: "SupplyChainPartner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLifeCycleDocument_SupplyChainPartner_SupplyChainPart~",
                table: "ProductLifeCycleDocument",
                column: "SupplyChainPartnerReceiverId",
                principalTable: "SupplyChainPartner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplyChainPartnerCertificate_SupplyChainPartner_SupplyChai~",
                table: "SupplyChainPartnerCertificate",
                column: "SupplyChainPartnerReceiverId",
                principalTable: "SupplyChainPartner",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_SupplyChainPartner_SupplyChainPartnerReceiverId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductDocument_SupplyChainPartner_SupplyChainPartnerReceiv~",
                table: "ProductDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductLifeCycleDocument_SupplyChainPartner_SupplyChainPart~",
                table: "ProductLifeCycleDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplyChainPartnerCertificate_SupplyChainPartner_SupplyChai~",
                table: "SupplyChainPartnerCertificate");

            migrationBuilder.DropCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User");

            migrationBuilder.DeleteData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("a4c2d7e6-6f5e-42f9-b7c1-1234567890ef"));

            migrationBuilder.DeleteData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("a9d12b5f-1e2d-45c9-bb5d-3d8a7c2b4a33"));

            migrationBuilder.DeleteData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("b1e2d3f4-5678-90ab-cdef-1234567890ab"));

            migrationBuilder.DeleteData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("b5d3e8f7-7e6f-43d0-c8d2-0987654321ba"));

            migrationBuilder.DeleteData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("c1d2e3f4-6789-0abc-def1-234567890abc"));

            migrationBuilder.DeleteData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("c6e4f9a8-8f70-44e1-d9e3-1029384756cd"));

            migrationBuilder.DeleteData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("d2e3f4a5-7890-1bcd-ef12-34567890abcd"));

            migrationBuilder.DeleteData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("d7f50ab9-9a81-45f2-eaf4-5647382910ef"));

            migrationBuilder.DeleteData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("e3a18178-8db7-48f2-a76b-9ad329bba5f2"));

            migrationBuilder.DeleteData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("e8a61bca-ab92-46f3-fb05-6758493021f0"));

            migrationBuilder.DeleteData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("ef94c672-c755-449b-8ee8-327a12bed7ef"));

            migrationBuilder.DeleteData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("f3b19128-0edc-4f59-8a27-6a8d3509876c"));

            migrationBuilder.DeleteData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("f9b72cdb-bc03-47f4-0c16-78695a4132f1"));

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplyChainPartnerReceiverId",
                table: "SupplyChainPartnerCertificate",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplyChainPartnerReceiverId",
                table: "ProductLifeCycleDocument",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmissionTransactionId",
                table: "ProductLifeCycle",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplyChainPartnerReceiverId",
                table: "ProductDocument",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplyChainPartnerReceiverId",
                table: "Contract",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

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

            migrationBuilder.AddCheckConstraint(
                name: "CK_User_PartnerOrAuthority",
                table: "User",
                sql: "(\"SupplyChainPartnerId\" IS NOT NULL AND \"CertificationAuthorityId\" IS NULL)\n                    OR (\"SupplyChainPartnerId\" IS NULL AND \"CertificationAuthorityId\" IS NOT NULL)");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_SupplyChainPartner_SupplyChainPartnerReceiverId",
                table: "Contract",
                column: "SupplyChainPartnerReceiverId",
                principalTable: "SupplyChainPartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDocument_SupplyChainPartner_SupplyChainPartnerReceiv~",
                table: "ProductDocument",
                column: "SupplyChainPartnerReceiverId",
                principalTable: "SupplyChainPartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLifeCycleDocument_SupplyChainPartner_SupplyChainPart~",
                table: "ProductLifeCycleDocument",
                column: "SupplyChainPartnerReceiverId",
                principalTable: "SupplyChainPartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplyChainPartnerCertificate_SupplyChainPartner_SupplyChai~",
                table: "SupplyChainPartnerCertificate",
                column: "SupplyChainPartnerReceiverId",
                principalTable: "SupplyChainPartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
