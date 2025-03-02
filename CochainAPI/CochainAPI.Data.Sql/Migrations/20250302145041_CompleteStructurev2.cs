using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CochainAPI.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class CompleteStructurev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "IdentityRole<Guid>",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NormalizedName = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole<Guid>", x => x.Id);
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
                name: "CertificationAuthority",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    CompanyTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificationAuthority", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertificationAuthority_CompanyType_CompanyTypeId",
                        column: x => x.CompanyTypeId,
                        principalTable: "CompanyType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    CompanyTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyChainPartner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplyChainPartner_CompanyType_CompanyTypeId",
                        column: x => x.CompanyTypeId,
                        principalTable: "CompanyType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: true),
                    WalletId = table.Column<string>(type: "text", nullable: true),
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
                    table.CheckConstraint("CK_User_PartnerOrAuthority", "\"SupplyChainPartnerId\" IS NOT NULL OR \"CertificationAuthorityId\" IS NOT NULL");
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
                        name: "FK_ProductIngredient_ProductInfo_ProductInfoId",
                        column: x => x.ProductInfoId,
                        principalTable: "ProductInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductIngredient_Product_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Product",
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
                    Path = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    UserEmitterId = table.Column<string>(type: "text", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    URL = table.Column<string>(type: "text", nullable: false),
                    QueryString = table.Column<string>(type: "text", nullable: false),
                    Cookies = table.Column<string>(type: "text", nullable: false),
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
                    Path = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    UserEmitterId = table.Column<string>(type: "text", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplyChainPartnerCertificate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QualityStandard = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    UserEmitterId = table.Column<string>(type: "text", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false)
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
                    Path = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    UserEmitterId = table.Column<string>(type: "text", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CompanyType",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("6173d450-c48a-4f24-82f6-f012413ff6f4"), "SCP" });

            migrationBuilder.InsertData(
                table: "IdentityRole<Guid>",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("8e342ad6-78d9-4aee-abe5-245b1fae6c4a"), null, "Admin", "Admin" },
                    { new Guid("cb5b1ae5-43db-4096-9bd6-2afb90fb20c5"), null, "SupplyChainPartner", "SupplyChainPartner" },
                    { new Guid("f6e7ea1d-a99e-4a5c-9a23-2274ba2c62ea"), null, "Certifier", "Certifier" }
                });

            migrationBuilder.InsertData(
                table: "SupplyChainPartnerType",
                columns: new[] { "Id", "Baseline", "Name" },
                values: new object[] { new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"), 1000f, "Trasporto" });

            migrationBuilder.InsertData(
                table: "SupplyChainPartner",
                columns: new[] { "Id", "CompanyTypeId", "Credits", "Email", "Name", "Phone", "SupplyChainPartnerTypeId" },
                values: new object[] { new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"), new Guid("6173d450-c48a-4f24-82f6-f012413ff6f4"), 0f, "company@prova.com", "Prova company", "33309090909", new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87") });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "CertificationAuthorityId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "SupplyChainPartnerId", "TwoFactorEnabled", "UserName", "WalletId" },
                values: new object[] { "5e4b0ca8-aa85-417a-af23-035ac1b555cd", 0, null, "fad005ac-253a-41f7-aa8c-0ff9ea79f433", "System", false, "System", true, "System", false, null, null, null, null, null, null, false, null, "192518af-1e01-40b4-8ca1-87d0e760ba1f", new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"), false, "System", null });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8e342ad6-78d9-4aee-abe5-245b1fae6c4a", "5e4b0ca8-aa85-417a-af23-035ac1b555cd" });

            migrationBuilder.InsertData(
                table: "UserTemporaryPassword",
                columns: new[] { "Id", "ExpirationDate", "IsUsed", "Password", "UserId" },
                values: new object[] { new Guid("f9e210be-2ed1-4fdc-aaeb-3543916db0da"), new DateTime(2027, 3, 2, 14, 50, 40, 222, DateTimeKind.Utc).AddTicks(6146), false, "System", "5e4b0ca8-aa85-417a-af23-035ac1b555cd" });

            migrationBuilder.CreateIndex(
                name: "IX_CarbonOffsettingAction_SupplyChainPartnerId",
                table: "CarbonOffsettingAction",
                column: "SupplyChainPartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificationAuthority_CompanyTypeId",
                table: "CertificationAuthority",
                column: "CompanyTypeId");

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
                name: "IX_SupplyChainPartner_CompanyTypeId",
                table: "SupplyChainPartner",
                column: "CompanyTypeId");

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
                name: "IdentityRole<Guid>");

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
                name: "CompanyType");

            migrationBuilder.DropTable(
                name: "SupplyChainPartnerType");
        }
    }
}
