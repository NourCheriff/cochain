using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CochainAPI.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class TransactionsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CertificationAuthority",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    WalletId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificationAuthority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductLifeCycleCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLifeCycleCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: true),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NormalizedName = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupplyChainPartnerType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Baseline = table.Column<float>(type: "real", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyChainPartnerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.Value, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplyChainPartner",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Credits = table.Column<float>(type: "real", nullable: false),
                    SupplyChainPartnerTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    WalletId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyChainPartner", x => x.Id);
                    table.UniqueConstraint("AK_SupplyChainPartner_WalletId", x => x.WalletId);
                    table.ForeignKey(
                        name: "FK_SupplyChainPartner_SupplyChainPartnerType_SupplyChainPartne~",
                        column: x => x.SupplyChainPartnerTypeId,
                        principalTable: "SupplyChainPartnerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarbonOffsettingAction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Offset = table.Column<float>(type: "real", nullable: false),
                    SupplyChainPartnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsProcessed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    EmissionTransactionId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarbonOffsettingAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarbonOffsettingAction_SupplyChainPartner_SupplyChainPartne~",
                        column: x => x.SupplyChainPartnerId,
                        principalTable: "SupplyChainPartner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpirationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SupplyChainPartnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInfo_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductInfo_SupplyChainPartner_SupplyChainPartnerId",
                        column: x => x.SupplyChainPartnerId,
                        principalTable: "SupplyChainPartner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: true),
                    CertificationAuthorityId = table.Column<Guid>(type: "uuid", nullable: true),
                    SupplyChainPartnerId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.CheckConstraint("CK_User_PartnerOrAuthority", "(\"SupplyChainPartnerId\" IS NOT NULL AND \"CertificationAuthorityId\" IS NULL)\n                    OR (\"SupplyChainPartnerId\" IS NULL AND \"CertificationAuthorityId\" IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_User_CertificationAuthority_CertificationAuthorityId",
                        column: x => x.CertificationAuthorityId,
                        principalTable: "CertificationAuthority",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_SupplyChainPartner_SupplyChainPartnerId",
                        column: x => x.SupplyChainPartnerId,
                        principalTable: "SupplyChainPartner",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductIngredient",
                columns: table => new
                {
                    IngredientId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductInfoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductIngredient", x => new { x.ProductInfoId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_ProductIngredient_ProductInfo_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "ProductInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductIngredient_ProductInfo_ProductInfoId",
                        column: x => x.ProductInfoId,
                        principalTable: "ProductInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductLifeCycle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Emissions = table.Column<float>(type: "real", nullable: false),
                    IsEmissionProcessed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    EmissionTransactionId = table.Column<string>(type: "text", nullable: false),
                    ProductLifeCycleCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplyChainPartnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLifeCycle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductLifeCycle_ProductInfo_ProductInfoId",
                        column: x => x.ProductInfoId,
                        principalTable: "ProductInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductLifeCycle_ProductLifeCycleCategory_ProductLifeCycleC~",
                        column: x => x.ProductLifeCycleCategoryId,
                        principalTable: "ProductLifeCycleCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductLifeCycle_SupplyChainPartner_SupplyChainPartnerId",
                        column: x => x.SupplyChainPartnerId,
                        principalTable: "SupplyChainPartner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductLifeCycleCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true),
                    Hash = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    UserEmitterId = table.Column<string>(type: "text", nullable: true),
                    SupplyChainPartnerReceiverId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_ProductLifeCycleCategory_ProductLifeCycleCategoryId",
                        column: x => x.ProductLifeCycleCategoryId,
                        principalTable: "ProductLifeCycleCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_SupplyChainPartner_SupplyChainPartnerReceiverId",
                        column: x => x.SupplyChainPartnerReceiverId,
                        principalTable: "SupplyChainPartner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_User_UserEmitterId",
                        column: x => x.UserEmitterId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Severity = table.Column<string>(type: "text", nullable: false),
                    Entity = table.Column<string>(type: "text", nullable: false),
                    EntityId = table.Column<string>(type: "text", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    URL = table.Column<string>(type: "text", nullable: true),
                    QueryString = table.Column<string>(type: "text", nullable: true),
                    Cookies = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Log_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true),
                    Hash = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    UserEmitterId = table.Column<string>(type: "text", nullable: true),
                    SupplyChainPartnerReceiverId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDocument_ProductInfo_ProductInfoId",
                        column: x => x.ProductInfoId,
                        principalTable: "ProductInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDocument_SupplyChainPartner_SupplyChainPartnerReceiv~",
                        column: x => x.SupplyChainPartnerReceiverId,
                        principalTable: "SupplyChainPartner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDocument_User_UserEmitterId",
                        column: x => x.UserEmitterId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SupplyChainPartnerCertificate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QualityStandard = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true),
                    Hash = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    UserEmitterId = table.Column<string>(type: "text", nullable: true),
                    SupplyChainPartnerReceiverId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyChainPartnerCertificate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplyChainPartnerCertificate_SupplyChainPartner_SupplyChai~",
                        column: x => x.SupplyChainPartnerReceiverId,
                        principalTable: "SupplyChainPartner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplyChainPartnerCertificate_User_UserEmitterId",
                        column: x => x.UserEmitterId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTemporaryPassword",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    Attempts = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTemporaryPassword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTemporaryPassword_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductLifeCycleDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductLifeCycleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true),
                    Hash = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    UserEmitterId = table.Column<string>(type: "text", nullable: true),
                    SupplyChainPartnerReceiverId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLifeCycleDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductLifeCycleDocument_ProductLifeCycle_ProductLifeCycleId",
                        column: x => x.ProductLifeCycleId,
                        principalTable: "ProductLifeCycle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductLifeCycleDocument_SupplyChainPartner_SupplyChainPart~",
                        column: x => x.SupplyChainPartnerReceiverId,
                        principalTable: "SupplyChainPartner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductLifeCycleDocument_User_UserEmitterId",
                        column: x => x.UserEmitterId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3318013d-0cdd-4749-9ab3-6fdca9b64564", null, "UserCA", "USERCA" },
                    { "8e342ad6-78d9-4aee-abe5-245b1fae6c4a", null, "SystemAdmin", "SYSTEMADMIN" },
                    { "916a3160-05e5-4821-88fe-9e46a43d157c", null, "SCPTransporter", "SCPTRANSPORTER" },
                    { "a0e8b03e-0cd8-4458-a147-1a4b88df2997", null, "UserSCP", "USERSCP" },
                    { "cb5b1ae5-43db-4096-9bd6-2afb90fb20c5", null, "AdminSCP", "ADMINSCP" },
                    { "cccf28ca-b2f8-477f-a2c1-2436cd83ec0c", null, "SCPRawMaterial", "SCPRAWMATERIAL" },
                    { "f08d90db-ac61-4c92-a229-ef803b672e60", null, "SCPTransformator", "SCPTRANSFORMATOR" },
                    { "f6e7ea1d-a99e-4a5c-9a23-2274ba2c62ea", null, "AdminCA", "ADMINCA" }
                });

            migrationBuilder.InsertData(
                table: "SupplyChainPartnerType",
                columns: new[] { "Id", "Baseline", "Name" },
                values: new object[,]
                {
                    { new Guid("ab2e7db4-760e-4515-9aa0-bda314266e87"), 1000f, "Stoccaggio" },
                    { new Guid("e1117db4-760e-4515-9aa0-11a3fa766e87"), 1000f, "Materia Prima" },
                    { new Guid("eaae7124-761e-4515-9aa0-bda3fc7aee87"), 1000f, "Grossista" },
                    { new Guid("ef01b3b4-760e-4515-9aa0-bdab7c766e87"), 1000f, "Trasformazione" },
                    { new Guid("ef0e7124-744e-1115-9ba0-bda3fc766e87"), 1000f, "Rivenditore Dettaglio" },
                    { new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"), 1000f, "Trasportatore" }
                });

            migrationBuilder.InsertData(
                table: "SupplyChainPartner",
                columns: new[] { "Id", "Credits", "Email", "Name", "Phone", "SupplyChainPartnerTypeId", "WalletId" },
                values: new object[,]
                {
                    { new Guid("3a9f1b7c-5d2e-4a4f-8a6c-0e7d3b5a2f9c"), 0f, "company2@prova.com", "Prova company2", "3669045897", new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"), "0x7c5d1a3f9b2e6f0d8c4a7e3b5c2f9d1" },
                    { new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"), 0f, "company@prova.com", "Prova company", "33309090909", new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"), "0x3a9f1b7c5d2e8a4f6c0e7d3b5a2f9c1" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "CertificationAuthorityId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "SupplyChainPartnerId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0a317b04-2f87-4b08-8ad8-597103527584", 0, null, "ed39c1f5-f184-4417-abd5-10171df59d52", null, false, "Matteo", true, "Spiga", false, null, null, null, null, null, null, false, null, "46f38c01-cf77-44ea-a862-5fef17c0f2ff", new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"), false, "matteospiga2002@gmail.com" },
                    { "3542da56-0de3-4797-a059-effff257f63d", 0, null, "12497287-6bfd-45ac-89c7-afa21dc7795a", null, false, "Mattia", true, "Mandorlini", false, null, null, null, null, null, null, false, null, "833972ff-40af-47ad-a4a5-a35b39e8d4f0", new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"), false, "mando3228@gmail.com" },
                    { "5e4b0ca8-aa85-417a-af23-035ac1b555cd", 0, null, "06e67265-7ecf-4ca5-8653-d680478a892d", null, false, "Paolo", true, "Roselli", false, null, null, null, null, null, null, false, null, "99a66675-8247-43c1-a3c5-1e78919e065c", new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"), false, "paolo.roselli02@gmail.com" },
                    { "a12c3708-0486-4603-b1a5-46d252e79082", 0, null, "397732a5-62d5-407d-ac16-95b9181f3b6e", null, false, "Cherif", true, "Nour", false, null, null, null, null, null, null, false, null, "679ba12b-add5-4344-bab3-3df177e1ee1f", new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"), false, "nourcherif.pitos25@gmail.com" },
                    { "ad00648b-a031-432d-b007-6a0829cf5292", 0, null, "8a7b76df-4ae0-4dc0-969f-863011236e33", null, false, "System", true, "System", false, null, null, null, null, null, null, false, null, "d7a75005-24b2-4260-b878-8b9caddc4898", new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"), false, "admin@cochain.com" },
                    { "f4242b5f-4b39-45fc-802e-391293414546", 0, null, "e42261bc-c140-4bbe-aaa9-15c91da9ce75", null, false, "Samuele", true, "Sacchetti", false, null, null, null, null, null, null, false, null, "248a261d-be12-4741-aeec-0df20cef4698", new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"), false, "sacchettisamuele@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "8e342ad6-78d9-4aee-abe5-245b1fae6c4a", "0a317b04-2f87-4b08-8ad8-597103527584" },
                    { "8e342ad6-78d9-4aee-abe5-245b1fae6c4a", "3542da56-0de3-4797-a059-effff257f63d" },
                    { "8e342ad6-78d9-4aee-abe5-245b1fae6c4a", "5e4b0ca8-aa85-417a-af23-035ac1b555cd" },
                    { "8e342ad6-78d9-4aee-abe5-245b1fae6c4a", "a12c3708-0486-4603-b1a5-46d252e79082" },
                    { "8e342ad6-78d9-4aee-abe5-245b1fae6c4a", "ad00648b-a031-432d-b007-6a0829cf5292" },
                    { "8e342ad6-78d9-4aee-abe5-245b1fae6c4a", "f4242b5f-4b39-45fc-802e-391293414546" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarbonOffsettingAction_SupplyChainPartnerId",
                table: "CarbonOffsettingAction",
                column: "SupplyChainPartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ProductLifeCycleCategoryId",
                table: "Contract",
                column: "ProductLifeCycleCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_SupplyChainPartnerReceiverId",
                table: "Contract",
                column: "SupplyChainPartnerReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_UserEmitterId",
                table: "Contract",
                column: "UserEmitterId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_UserId",
                table: "Log",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDocument_ProductInfoId",
                table: "ProductDocument",
                column: "ProductInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDocument_SupplyChainPartnerReceiverId",
                table: "ProductDocument",
                column: "SupplyChainPartnerReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDocument_UserEmitterId",
                table: "ProductDocument",
                column: "UserEmitterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInfo_ProductId",
                table: "ProductInfo",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInfo_SupplyChainPartnerId",
                table: "ProductInfo",
                column: "SupplyChainPartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIngredient_IngredientId",
                table: "ProductIngredient",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLifeCycle_ProductInfoId",
                table: "ProductLifeCycle",
                column: "ProductInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLifeCycle_ProductLifeCycleCategoryId",
                table: "ProductLifeCycle",
                column: "ProductLifeCycleCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLifeCycle_SupplyChainPartnerId",
                table: "ProductLifeCycle",
                column: "SupplyChainPartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLifeCycleDocument_ProductLifeCycleId",
                table: "ProductLifeCycleDocument",
                column: "ProductLifeCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLifeCycleDocument_SupplyChainPartnerReceiverId",
                table: "ProductLifeCycleDocument",
                column: "SupplyChainPartnerReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLifeCycleDocument_UserEmitterId",
                table: "ProductLifeCycleDocument",
                column: "UserEmitterId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplyChainPartner_SupplyChainPartnerTypeId",
                table: "SupplyChainPartner",
                column: "SupplyChainPartnerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplyChainPartnerCertificate_SupplyChainPartnerReceiverId",
                table: "SupplyChainPartnerCertificate",
                column: "SupplyChainPartnerReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplyChainPartnerCertificate_UserEmitterId",
                table: "SupplyChainPartnerCertificate",
                column: "UserEmitterId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_WalletIdEmitter",
                table: "Transaction",
                column: "WalletIdEmitter");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_WalletIdReceiver",
                table: "Transaction",
                column: "WalletIdReceiver");

            migrationBuilder.CreateIndex(
                name: "IX_User_CertificationAuthorityId",
                table: "User",
                column: "CertificationAuthorityId");

            migrationBuilder.CreateIndex(
                name: "IX_User_SupplyChainPartnerId",
                table: "User",
                column: "SupplyChainPartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTemporaryPassword_UserId",
                table: "UserTemporaryPassword",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarbonOffsettingAction");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "ProductDocument");

            migrationBuilder.DropTable(
                name: "ProductIngredient");

            migrationBuilder.DropTable(
                name: "ProductLifeCycleDocument");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "SupplyChainPartnerCertificate");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTemporaryPassword");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "ProductLifeCycle");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ProductInfo");

            migrationBuilder.DropTable(
                name: "ProductLifeCycleCategory");

            migrationBuilder.DropTable(
                name: "CertificationAuthority");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "SupplyChainPartner");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "SupplyChainPartnerType");
        }
    }
}
