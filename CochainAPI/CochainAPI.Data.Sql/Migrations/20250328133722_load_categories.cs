using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CochainAPI.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class load_categories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("12345678-9012-3456-abcd-90ef12345678"), "Oil, vinegar, salt, pepper, and other spices.", "Condiments & Spices" },
                    { new Guid("78901234-5678-9012-abcd-34ef56789012"), "Dry, fresh, whole wheat pasta, and different types of rice.", "Pasta & Rice" },
                    { new Guid("89012345-6789-0123-abcd-56ef78901234"), "Soft drinks, fruit juices, water, and alcoholic beverages.", "Beverages" },
                    { new Guid("90123456-7890-1234-abcd-78ef90123456"), "Chocolate, candies, chips, and other sweet and salty snacks.", "Sweets & Snacks" },
                    { new Guid("a1b2c3d4-e5f6-7890-ab12-cd34ef567890"), "Fresh and processed meat including beef, pork, chicken, and more.", "Meat" },
                    { new Guid("b2c3d4e5-f678-9012-abcd-34ef56789012"), "Fresh, frozen, and processed seafood products.", "Fish" },
                    { new Guid("c3d4e5f6-7890-1234-abcd-56ef78901234"), "Fresh, organic, and frozen vegetables.", "Vegetables" },
                    { new Guid("d4e5f678-9012-3456-abcd-78ef90123456"), "Fresh, dried, and packaged fruits.", "Fruits" },
                    { new Guid("e5f67890-1234-5678-abcd-90ef12345678"), "Milk, cheese, yogurt, and other dairy products.", "Dairy" },
                    { new Guid("f6789012-3456-7890-abcd-12ef34567890"), "Fresh bread, biscuits, breadsticks, and other baked goods.", "Bakery Products" }
                });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("a4c2d7e6-6f5e-42f9-b7c1-1234567890ef"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Implementation of efficient irrigation systems to reduce water and energy consumption.", "Sustainable Irrigation" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("a9d12b5f-1e2d-45c9-bb5d-3d8a7c2b4a33"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Product manufacturing activities.", "Production" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("b1e2d3f4-5678-90ab-cdef-1234567890ab"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Raw material processing activities.", "Processing" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("b5d3e8f7-7e6f-43d0-c8d2-0987654321ba"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Use of natural or slow-release fertilizers to minimize greenhouse gas emissions.", "Low-Impact Fertilization" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("c1d2e3f4-6789-0abc-def1-234567890abc"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Product sales activities.", "Sales" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("c6e4f9a8-8f70-44e1-d9e3-1029384756cd"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Adoption of integrated pest control practices, reducing chemical pesticide use and environmental impact.", "Pest Management" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("d2e3f4a5-7890-1bcd-ef12-34567890abcd"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Post-sales customer assistance.", "Customer Support" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("d7f50ab9-9a81-45f2-eaf4-5647382910ef"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Optimized harvesting processes to minimize energy consumption and emissions from internal transport.", "Harvesting" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("e3a18178-8db7-48f2-a76b-9ad329bba5f2"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Plowing and soil preparation activities using techniques aimed at minimizing the use of heavy machinery to reduce emissions.", "Soil Preparation" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("e8a61bca-ab92-46f3-fb05-6758493021f0"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Selection, washing, and storage activities using low-energy impact techniques to maintain product quality.", "Post-Harvest & Storage" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("ef94c672-c755-449b-8ee8-327a12bed7ef"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Product transportation.", "Transport" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("f3b19128-0edc-4f59-8a27-6a8d3509876c"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Precision seeding activities to optimize resource use and reduce environmental impact.", "Seeding" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("f9b72cdb-bc03-47f4-0c16-78695a4132f1"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Use of recyclable materials and low-impact processes to reduce the carbon footprint of packaging.", "Eco-Friendly Packaging" });

            migrationBuilder.UpdateData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("ab2e7db4-760e-4515-9aa0-bda314266e87"),
                column: "Name",
                value: "Storage");

            migrationBuilder.UpdateData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("e1117db4-760e-4515-9aa0-11a3fa766e87"),
                column: "Name",
                value: "Raw Material Supplier");

            migrationBuilder.UpdateData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("eaae7124-761e-4515-9aa0-bda3fc7aee87"),
                column: "Name",
                value: "Wholesaler");

            migrationBuilder.UpdateData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("ef01b3b4-760e-4515-9aa0-bdab7c766e87"),
                column: "Name",
                value: "Processing");

            migrationBuilder.UpdateData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("ef0e7124-744e-1115-9ba0-bda3fc766e87"),
                column: "Name",
                value: "Retailer");

            migrationBuilder.UpdateData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"),
                column: "Name",
                value: "Transporter");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumn: "Id",
                keyValue: new Guid("12345678-9012-3456-abcd-90ef12345678"));

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumn: "Id",
                keyValue: new Guid("78901234-5678-9012-abcd-34ef56789012"));

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumn: "Id",
                keyValue: new Guid("89012345-6789-0123-abcd-56ef78901234"));

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumn: "Id",
                keyValue: new Guid("90123456-7890-1234-abcd-78ef90123456"));

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-ab12-cd34ef567890"));

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f678-9012-abcd-34ef56789012"));

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-7890-1234-abcd-56ef78901234"));

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f678-9012-3456-abcd-78ef90123456"));

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumn: "Id",
                keyValue: new Guid("e5f67890-1234-5678-abcd-90ef12345678"));

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumn: "Id",
                keyValue: new Guid("f6789012-3456-7890-abcd-12ef34567890"));

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("a4c2d7e6-6f5e-42f9-b7c1-1234567890ef"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Implementazione di sistemi di irrigazione efficienti per ridurre il consumo idrico e l'energia necessaria.", "Irrigazione Sostenibile" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("a9d12b5f-1e2d-45c9-bb5d-3d8a7c2b4a33"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Attività di produzione del prodotto.", "Produzione" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("b1e2d3f4-5678-90ab-cdef-1234567890ab"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Attività di lavorazione della materia prima.", "Lavorazione" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("b5d3e8f7-7e6f-43d0-c8d2-0987654321ba"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Utilizzo di fertilizzanti naturali o a rilascio controllato per minimizzare le emissioni di gas serra.", "Fertilizzazione a Basso Impatto" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("c1d2e3f4-6789-0abc-def1-234567890abc"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Attività di vendita del prodotto.", "Vendita" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("c6e4f9a8-8f70-44e1-d9e3-1029384756cd"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Adozione di pratiche integrate per il controllo dei parassiti, riducendo l'uso di pesticidi chimici e l'impatto ambientale.", "Gestione dei Parassiti" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("d2e3f4a5-7890-1bcd-ef12-34567890abcd"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Attività di assistenza post vendita.", "Assistenza" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("d7f50ab9-9a81-45f2-eaf4-5647382910ef"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Processi di raccolta ottimizzati per minimizzare il consumo energetico e le emissioni dovute al trasporto interno.", "Raccolta" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("e3a18178-8db7-48f2-a76b-9ad329bba5f2"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Attività di aratura e lavorazione del suolo, con tecniche volte a minimizzare l'uso di macchinari pesanti per ridurre le emissioni.", "Preparazione del Terreno" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("e8a61bca-ab92-46f3-fb05-6758493021f0"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Attività di selezione, lavaggio e conservazione con tecniche a basso impatto energetico per mantenere la qualità del prodotto.", "Post-Raccolta e Conservazione" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("ef94c672-c755-449b-8ee8-327a12bed7ef"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Trasporto del prodotto.", "Trasporto" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("f3b19128-0edc-4f59-8a27-6a8d3509876c"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Attività di semina utilizzando metodi di precisione per ottimizzare l'utilizzo di risorse e ridurre l'impatto ambientale.", "Semina" });

            migrationBuilder.UpdateData(
                table: "ProductLifeCycleCategory",
                keyColumn: "Id",
                keyValue: new Guid("f9b72cdb-bc03-47f4-0c16-78695a4132f1"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Utilizzo di materiali riciclabili e processi a basso impatto per ridurre la carbon footprint del packaging.", "Imballaggio Eco-Sostenibile" });

            migrationBuilder.UpdateData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("ab2e7db4-760e-4515-9aa0-bda314266e87"),
                column: "Name",
                value: "Stoccaggio");

            migrationBuilder.UpdateData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("e1117db4-760e-4515-9aa0-11a3fa766e87"),
                column: "Name",
                value: "Materia Prima");

            migrationBuilder.UpdateData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("eaae7124-761e-4515-9aa0-bda3fc7aee87"),
                column: "Name",
                value: "Grossista");

            migrationBuilder.UpdateData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("ef01b3b4-760e-4515-9aa0-bdab7c766e87"),
                column: "Name",
                value: "Trasformazione");

            migrationBuilder.UpdateData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("ef0e7124-744e-1115-9ba0-bda3fc766e87"),
                column: "Name",
                value: "Rivenditore Dettaglio");

            migrationBuilder.UpdateData(
                table: "SupplyChainPartnerType",
                keyColumn: "Id",
                keyValue: new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"),
                column: "Name",
                value: "Trasportatore");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "0a317b04-2f87-4b08-8ad8-597103527584",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4477a6fe-6e97-42b2-821e-1a8e9e90e57d", "0197b436-8934-4303-a76b-c08901a8a937" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "3542da56-0de3-4797-a059-effff257f63d",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9acef675-7e18-4edb-a92d-0fa0f14d44ec", "472c1738-2bde-4fe3-a565-46fb79923734" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "5e4b0ca8-aa85-417a-af23-035ac1b555cd",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "82de85ba-29fb-4b4c-8062-b6830ed8e235", "793c618e-33f2-4864-94e6-ae43f515b19d" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "a12c3708-0486-4603-b1a5-46d252e79082",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "76df9b7b-27ce-45c3-91e1-32ac697fc987", "638b6bc3-24ba-40d0-a16c-606d14aa94e0" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "ad00648b-a031-432d-b007-6a0829cf5292",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "02fbcb6c-0220-431f-920e-95f3773ceeb0", "f71733ba-bb32-4cf9-a4f3-416062f41c83" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "f4242b5f-4b39-45fc-802e-391293414546",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d14133fe-6de9-47c0-8c28-99f9f4e7ea9f", "3c8c525a-b50e-41be-967e-351ea43f6e08" });
        }
    }
}
