using Microsoft.EntityFrameworkCore;
using CochainAPI.Model.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Product;
using CochainAPI.Model.CarbonOffset;
using CochainAPI.Model.Documents;
using CochainAPI.Model.Utils;

namespace CochainAPI.Data.Sql
{
    public class CochainDBContext : IdentityDbContext<User>
    {
        public CochainDBContext(DbContextOptions<CochainDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserTemporaryPassword> UserTemporaryPassword { get; set; }
        public DbSet<CertificationAuthority> CertificationAuthority { get; set; }
        public DbSet<SupplyChainPartner> SupplyChainPartner { get; set; }
        public DbSet<SupplyChainPartnerType> SupplyChainPartnerType { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductInfo> ProductInfo { get; set; }
        public DbSet<ProductIngredient> ProductIngredient { get; set; }
        public DbSet<ProductLifeCycleCategory> ProductLifeCycleCategory { get; set; }
        public DbSet<ProductLifeCycle> ProductLifeCycle { get; set; }
        public DbSet<CarbonOffsettingAction> CarbonOffsettingAction { get; set; }
        public DbSet<Contract> Contract { get; set; }
        public DbSet<ProductDocument> ProductDocument { get; set; }
        public DbSet<ProductLifeCycleDocument> ProductLifeCycleDocument { get; set; }
        public DbSet<SupplyChainPartnerCertificate> SupplyChainPartnerCertificate { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Transaction> Transaction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(r => new { r.UserId, r.RoleId });
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(r => new { r.Value, r.UserId });
            });

            modelBuilder.Entity<User>().HasOne(x => x.SupplyChainPartner).WithMany().HasForeignKey(x => x.SupplyChainPartnerId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasOne(x => x.CertificationAuthority).WithMany().HasForeignKey(x => x.CertificationAuthorityId);
            modelBuilder.Entity<User>().HasMany(x => x.TemporaryPasswords).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<User>().HasMany(x => x.UserRoles).WithOne().HasForeignKey(x => x.UserId);
            modelBuilder.Entity<User>().HasMany(x => x.UserClaims).WithOne().HasForeignKey(x => x.UserId);
            modelBuilder.Entity<User>().HasMany(x => x.Logs).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<User>().ToTable(tb =>
            {
                tb.HasCheckConstraint(
                    "CK_User_PartnerOrAuthority",
                    @"(""SupplyChainPartnerId"" IS NOT NULL AND ""CertificationAuthorityId"" IS NULL)
                    OR (""SupplyChainPartnerId"" IS NULL AND ""CertificationAuthorityId"" IS NOT NULL)");
            });

            modelBuilder.Entity<SupplyChainPartner>().HasOne(x => x.SupplyChainPartnerType).WithMany().HasForeignKey(x => x.SupplyChainPartnerTypeId);

            modelBuilder.Entity<Product>().HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId);
            modelBuilder.Entity<ProductInfo>().HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<ProductInfo>().HasOne(x => x.SupplyChainPartner).WithMany().HasForeignKey(x => x.SupplyChainPartnerId);
            modelBuilder.Entity<ProductInfo>().HasMany(x => x.Ingredients).WithOne(x => x.ProductInfo).HasForeignKey(x => x.ProductInfoId);
            modelBuilder.Entity<ProductInfo>().HasMany(x => x.ProductLifeCycles).WithOne(x => x.ProductInfo).HasForeignKey(x => x.ProductInfoId);
            modelBuilder.Entity<ProductLifeCycle>().HasOne(x => x.ProductLifeCycleCategory).WithMany().HasForeignKey(x => x.ProductLifeCycleCategoryId);
            modelBuilder.Entity<ProductLifeCycle>().HasOne(x => x.SupplyChainPartner).WithMany().HasForeignKey(x => x.SupplyChainPartnerId);

            modelBuilder.Entity<ProductLifeCycle>()
                        .Property(c => c.IsEmissionProcessed)
                        .HasDefaultValue(false);
            
            modelBuilder.Entity<ProductIngredient>(entity =>
            {
                entity.HasKey(r => new { r.ProductInfoId, r.IngredientId });
                entity.HasOne(x => x.Ingredient).WithMany().HasForeignKey(x => x.IngredientId);
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(d => d.UserEmitter).WithMany(u => u.EmittedContract).HasForeignKey(d => d.UserEmitterId);
                entity.HasOne(d => d.SupplyChainPartnerReceiver).WithMany(u => u.ReceivedContract).HasForeignKey(d => d.SupplyChainPartnerReceiverId);
            });

            modelBuilder.Entity<ProductDocument>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(d => d.UserEmitter).WithMany(u => u.EmittedProductDocument).HasForeignKey(d => d.UserEmitterId);
                entity.HasOne(d => d.SupplyChainPartnerReceiver).WithMany(u => u.ReceivedProductDocument).HasForeignKey(d => d.SupplyChainPartnerReceiverId);
                entity.HasOne(d => d.ProductInfo).WithMany(x => x.ProductDocuments).HasForeignKey(x => x.ProductInfoId);
            });

            modelBuilder.Entity<ProductLifeCycleDocument>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(d => d.UserEmitter).WithMany(u => u.EmittedProductLifeCycleDocument).HasForeignKey(d => d.UserEmitterId);
                entity.HasOne(d => d.SupplyChainPartnerReceiver).WithMany(u => u.ReceivedProductLifeCycleDocument).HasForeignKey(d => d.SupplyChainPartnerReceiverId);
            });

            modelBuilder.Entity<SupplyChainPartnerCertificate>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(d => d.UserEmitter).WithMany(u => u.EmittedSupplyChainPartnerCertificate).HasForeignKey(d => d.UserEmitterId);
                entity.HasOne(d => d.SupplyChainPartnerReceiver).WithMany(u => u.ReceivedSupplyChainPartnerCertificate).HasForeignKey(d => d.SupplyChainPartnerReceiverId);
            });

            modelBuilder.Entity<Contract>().HasOne(x => x.ProductLifeCycleCategory).WithMany().HasForeignKey(x => x.ProductLifeCycleCategoryId);
            modelBuilder.Entity<ProductDocument>().HasOne(x => x.ProductInfo).WithMany(x => x.ProductDocuments).HasForeignKey(x => x.ProductInfoId);
            modelBuilder.Entity<ProductLifeCycleDocument>().HasOne(x => x.ProductLifeCycle).WithMany(x => x.ProductLifeCycleDocuments).HasForeignKey(x => x.ProductLifeCycleId);

            modelBuilder.Entity<CarbonOffsettingAction>().HasOne(x => x.SupplyChainPartner).WithMany(x => x.CarbonOffsettingActions).HasForeignKey(x => x.SupplyChainPartnerId);
            modelBuilder.Entity<CarbonOffsettingAction>()
                        .Property(c => c.IsProcessed)
                        .HasDefaultValue(false);

            modelBuilder.Entity<Transaction>().HasOne(x => x.SupplyChainPartnerEmitter).WithMany(x => x.EmittedTransactions).HasForeignKey(x => x.WalletIdEmitter).HasPrincipalKey(x => x.WalletId);
            modelBuilder.Entity<Transaction>().HasOne(x => x.SupplyChainPartnerReceiver).WithMany(x => x.ReceivedTransactions).HasForeignKey(x => x.WalletIdReceiver).HasPrincipalKey(x => x.WalletId);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = Guid.Parse("8e342ad6-78d9-4aee-abe5-245b1fae6c4a").ToString(),
                    Name = "SystemAdmin",
                    NormalizedName = "SystemAdmin".ToUpper(),
                },
                new IdentityRole
                {
                    Id = Guid.Parse("cb5b1ae5-43db-4096-9bd6-2afb90fb20c5").ToString(),
                    Name = "AdminSCP",
                    NormalizedName = "AdminSCP".ToUpper(),
                },
                new IdentityRole
                {
                    Id = Guid.Parse("f6e7ea1d-a99e-4a5c-9a23-2274ba2c62ea").ToString(),
                    Name = "AdminCA",
                    NormalizedName = "AdminCA".ToUpper(),
                },
                new IdentityRole
                {
                    Id = Guid.Parse("a0e8b03e-0cd8-4458-a147-1a4b88df2997").ToString(),
                    Name = "UserSCP",
                    NormalizedName = "UserSCP".ToUpper(),
                },
                new IdentityRole
                {
                    Id = Guid.Parse("3318013d-0cdd-4749-9ab3-6fdca9b64564").ToString(),
                    Name = "UserCA",
                    NormalizedName = "UserCA".ToUpper(),
                },
                new IdentityRole
                {
                    Id = Guid.Parse("916a3160-05e5-4821-88fe-9e46a43d157c").ToString(),
                    Name = "SCPTransporter",
                    NormalizedName = "SCPTransporter".ToUpper(),
                },
                new IdentityRole
                {
                    Id = Guid.Parse("cccf28ca-b2f8-477f-a2c1-2436cd83ec0c").ToString(),
                    Name = "SCPRawMaterial",
                    NormalizedName = "SCPRawMaterial".ToUpper(),
                },
                new IdentityRole
                {
                    Id = Guid.Parse("f08d90db-ac61-4c92-a229-ef803b672e60").ToString(),
                    Name = "SCPTransformator",
                    NormalizedName = "SCPTransformator".ToUpper(),
                });

            modelBuilder.Entity<SupplyChainPartner>().HasData(
                new SupplyChainPartner
                {
                    Id = new Guid("81124c04-840a-49c1-8929-073af4cee139"),
                    Name = "Test Company",
                    Email = "company@test.com",
                    Phone = "33309090909",
                    Credits = 100,
                    SupplyChainPartnerTypeId = new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"),
                    WalletId = "0xfe3b557e8fb62b89f4916b721be55ceb828dbd73"
                },
                new SupplyChainPartner
                {
                    Id = new Guid("3a9f1b7c-5d2e-4a4f-8a6c-0e7d3b5a2f9c"),
                    Name = "Raw Material Supplier",
                    Email = "raw.material.supplier@test.com",
                    Phone = "3669045897",
                    Credits = 40,
                    SupplyChainPartnerTypeId = new Guid("e1117db4-760e-4515-9aa0-11a3fa766e87"),
                    WalletId = "0x627306090abab3a6e1400e9345bc60c78a8bef57"
                },
                new SupplyChainPartner
                {
                    Id = new Guid("db2c2af0-5227-4d3c-b3eb-daf45118aeff"),
                    Name = "Retailer",
                    Email = "retailer@test.com",
                    Phone = "3669045897",
                    Credits = 20,
                    SupplyChainPartnerTypeId = new Guid("eaae7124-761e-4515-9aa0-bda3fc7aee87"),
                    WalletId = "0xf17f52151ebef6c7334fad080c5704d77216b732"
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("ad00648b-a031-432d-b007-6a0829cf5292").ToString(),
                    FirstName = "System",
                    LastName = "System",
                    UserName = "admin@cochain.com",
                    SupplyChainPartnerId = new Guid("81124c04-840a-49c1-8929-073af4cee139"),
                    IsActive = true
                },
                new User
                {
                    Id = Guid.Parse("5e4b0ca8-aa85-417a-af23-035ac1b555cd").ToString(),
                    FirstName = "Paolo",
                    LastName = "Roselli",
                    UserName = "paolo.roselli02@gmail.com",
                    SupplyChainPartnerId = new Guid("81124c04-840a-49c1-8929-073af4cee139"),
                    IsActive = true
                },
                new User
                {
                    Id = Guid.Parse("0a317b04-2f87-4b08-8ad8-597103527584").ToString(),
                    FirstName = "Matteo",
                    LastName = "Spiga",
                    UserName = "matteospiga2002@gmail.com",
                    SupplyChainPartnerId = new Guid("81124c04-840a-49c1-8929-073af4cee139"),
                    IsActive = true
                },
                new User
                {
                    Id = Guid.Parse("a12c3708-0486-4603-b1a5-46d252e79082").ToString(),
                    FirstName = "Cherif",
                    LastName = "Nour",
                    UserName = "nourcherif.pitos25@gmail.com",
                    SupplyChainPartnerId = new Guid("81124c04-840a-49c1-8929-073af4cee139"),
                    IsActive = true
                },
                new User
                {
                    Id = Guid.Parse("3542da56-0de3-4797-a059-effff257f63d").ToString(),
                    FirstName = "Mattia",
                    LastName = "Mandorlini",
                    UserName = "mando3228@gmail.com",
                    SupplyChainPartnerId = new Guid("81124c04-840a-49c1-8929-073af4cee139"),
                    IsActive = true
                },
                new User
                {
                    Id = Guid.Parse("f4242b5f-4b39-45fc-802e-391293414546").ToString(),
                    FirstName = "Samuele",
                    LastName = "Sacchetti",
                    UserName = "sacchettisamuele@gmail.com",
                    SupplyChainPartnerId = new Guid("81124c04-840a-49c1-8929-073af4cee139"),
                    IsActive = true
                },
                new User
                {
                    Id = Guid.Parse("5a6c9915-bdca-4a68-b452-da1f4e8b422f").ToString(),
                    FirstName = "Luca",
                    LastName = "Spalazzi",
                    UserName = "l.spalazzi@staff.univpm.it",
                    SupplyChainPartnerId = new Guid("81124c04-840a-49c1-8929-073af4cee139"),
                    IsActive = true
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = Guid.Parse("ad00648b-a031-432d-b007-6a0829cf5292").ToString(),
                    RoleId = Guid.Parse("8e342ad6-78d9-4aee-abe5-245b1fae6c4a").ToString()
                },
                new IdentityUserRole<string>
                {
                    UserId = Guid.Parse("5e4b0ca8-aa85-417a-af23-035ac1b555cd").ToString(),
                    RoleId = Guid.Parse("8e342ad6-78d9-4aee-abe5-245b1fae6c4a").ToString()
                },
                new IdentityUserRole<string>
                {
                    UserId = Guid.Parse("0a317b04-2f87-4b08-8ad8-597103527584").ToString(),
                    RoleId = Guid.Parse("8e342ad6-78d9-4aee-abe5-245b1fae6c4a").ToString()
                },
                new IdentityUserRole<string>
                {
                    UserId = Guid.Parse("a12c3708-0486-4603-b1a5-46d252e79082").ToString(),
                    RoleId = Guid.Parse("8e342ad6-78d9-4aee-abe5-245b1fae6c4a").ToString()
                },
                new IdentityUserRole<string>
                {
                    UserId = Guid.Parse("3542da56-0de3-4797-a059-effff257f63d").ToString(),
                    RoleId = Guid.Parse("8e342ad6-78d9-4aee-abe5-245b1fae6c4a").ToString()
                },
                new IdentityUserRole<string>
                {
                    UserId = Guid.Parse("f4242b5f-4b39-45fc-802e-391293414546").ToString(),
                    RoleId = Guid.Parse("8e342ad6-78d9-4aee-abe5-245b1fae6c4a").ToString()
                });

            modelBuilder.Entity<SupplyChainPartnerType>().HasData(
                new SupplyChainPartnerType
                {
                    Id = new Guid("e1117db4-760e-4515-9aa0-11a3fa766e87"),
                    Name = "Raw Material Supplier",
                    Baseline = 1000.0f
                },
                new SupplyChainPartnerType
                {
                    Id = new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"),
                    Name = "Transporter",
                    Baseline = 1000.0f
                },
                new SupplyChainPartnerType
                {
                    Id = new Guid("ef01b3b4-760e-4515-9aa0-bdab7c766e87"),
                    Name = "Processing",
                    Baseline = 1000.0f
                },
                new SupplyChainPartnerType
                {
                    Id = new Guid("ab2e7db4-760e-4515-9aa0-bda314266e87"),
                    Name = "Storage",
                    Baseline = 1000.0f
                },
                new SupplyChainPartnerType
                {
                    Id = new Guid("ef0e7124-744e-1115-9ba0-bda3fc766e87"),
                    Name = "Retailer",
                    Baseline = 1000.0f
                },
                new SupplyChainPartnerType
                {
                    Id = new Guid("eaae7124-761e-4515-9aa0-bda3fc7aee87"),
                    Name = "Wholesaler",
                    Baseline = 1000.0f
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = new Guid("111aaa22-bb33-cc44-dd55-ee66ff778899"),
                    Name = "Chicken Breast",
                    Description = "Boneless, skinless chicken breast.",
                    CategoryId = new Guid("a1b2c3d4-e5f6-7890-ab12-cd34ef567890") // Meat
                },
                new Product
                {
                    Id = new Guid("222bbb33-cc44-dd55-ee66-ff778899aabb"),
                    Name = "Salmon Fillet",
                    Description = "Fresh Atlantic salmon fillet.",
                    CategoryId = new Guid("b2c3d4e5-f678-9012-abcd-34ef56789012") // Fish
                },
                new Product
                {
                    Id = new Guid("333ccc44-dd55-ee66-ff77-8899aabbccdd"),
                    Name = "Carrots",
                    Description = "Organic fresh carrots.",
                    CategoryId = new Guid("c3d4e5f6-7890-1234-abcd-56ef78901234") // Vegetables
                },
                new Product
                {
                    Id = new Guid("444ddd55-ee66-ff77-8899-aabbccddeeff"),
                    Name = "Bananas",
                    Description = "Sweet and ripe bananas.",
                    CategoryId = new Guid("d4e5f678-9012-3456-abcd-78ef90123456") // Fruits
                },
                new Product
                {
                    Id = new Guid("555eee66-ff77-8899-aabb-ccddeeff0011"),
                    Name = "Whole Milk",
                    Description = "Pasteurized whole milk.",
                    CategoryId = new Guid("e5f67890-1234-5678-abcd-90ef12345678") // Dairy
                },
                new Product
                {
                    Id = new Guid("666fff77-8899-aabb-ccdd-eeff00112233"),
                    Name = "Baguette",
                    Description = "Traditional French bread.",
                    CategoryId = new Guid("f6789012-3456-7890-abcd-12ef34567890") // Bakery Products
                },
                new Product
                {
                    Id = new Guid("77711188-999a-bbcc-ddee-ff0011223344"),
                    Name = "Spaghetti",
                    Description = "Italian durum wheat spaghetti.",
                    CategoryId = new Guid("78901234-5678-9012-abcd-34ef56789012") // Pasta & Rice
                },
                new Product
                {
                    Id = new Guid("88822299-aabb-ccdd-eeff-001122334455"),
                    Name = "Orange Juice",
                    Description = "100% fresh squeezed orange juice.",
                    CategoryId = new Guid("89012345-6789-0123-abcd-56ef78901234") // Beverages
                },
                new Product
                {
                    Id = new Guid("999333aa-bbcc-ddee-ff00-112233445566"),
                    Name = "Milk Chocolate Bar",
                    Description = "Creamy milk chocolate bar.",
                    CategoryId = new Guid("90123456-7890-1234-abcd-78ef90123456") // Sweets & Snacks
                },
                new Product
                {
                    Id = new Guid("aaa444bb-ccdd-eeff-0011-223344556677"),
                    Name = "Olive Oil",
                    Description = "Extra virgin olive oil.",
                    CategoryId = new Guid("12345678-9012-3456-abcd-90ef12345678") // Condiments & Spices
                }
            );

            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory
                {
                    Id = new Guid("a1b2c3d4-e5f6-7890-ab12-cd34ef567890"),
                    Name = "Meat",
                    Description = "Fresh and processed meat including beef, pork, chicken, and more."
                },
                new ProductCategory
                {
                    Id = new Guid("b2c3d4e5-f678-9012-abcd-34ef56789012"),
                    Name = "Fish",
                    Description = "Fresh, frozen, and processed seafood products."
                },
                new ProductCategory
                {
                    Id = new Guid("c3d4e5f6-7890-1234-abcd-56ef78901234"),
                    Name = "Vegetables",
                    Description = "Fresh, organic, and frozen vegetables."
                },
                new ProductCategory
                {
                    Id = new Guid("d4e5f678-9012-3456-abcd-78ef90123456"),
                    Name = "Fruits",
                    Description = "Fresh, dried, and packaged fruits."
                },
                new ProductCategory
                {
                    Id = new Guid("e5f67890-1234-5678-abcd-90ef12345678"),
                    Name = "Dairy",
                    Description = "Milk, cheese, yogurt, and other dairy products."
                },
                new ProductCategory
                {
                    Id = new Guid("f6789012-3456-7890-abcd-12ef34567890"),
                    Name = "Bakery Products",
                    Description = "Fresh bread, biscuits, breadsticks, and other baked goods."
                },
                new ProductCategory
                {
                    Id = new Guid("78901234-5678-9012-abcd-34ef56789012"),
                    Name = "Pasta & Rice",
                    Description = "Dry, fresh, whole wheat pasta, and different types of rice."
                },
                new ProductCategory
                {
                    Id = new Guid("89012345-6789-0123-abcd-56ef78901234"),
                    Name = "Beverages",
                    Description = "Soft drinks, fruit juices, water, and alcoholic beverages."
                },
                new ProductCategory
                {
                    Id = new Guid("90123456-7890-1234-abcd-78ef90123456"),
                    Name = "Sweets & Snacks",
                    Description = "Chocolate, candies, chips, and other sweet and salty snacks."
                },
                new ProductCategory
                {
                    Id = new Guid("12345678-9012-3456-abcd-90ef12345678"),
                    Name = "Condiments & Spices",
                    Description = "Oil, vinegar, salt, pepper, and other spices."
                }
            );


            modelBuilder.Entity<ProductLifeCycleCategory>().HasData(
                new ProductLifeCycleCategory
                {
                    Id = new Guid("ef94c672-c755-449b-8ee8-327a12bed7ef"),
                    Name = "Transport",
                    Description = "Product transportation."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("a9d12b5f-1e2d-45c9-bb5d-3d8a7c2b4a33"),
                    Name = "Production",
                    Description = "Product manufacturing activities."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("b1e2d3f4-5678-90ab-cdef-1234567890ab"),
                    Name = "Processing",
                    Description = "Raw material processing activities."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("c1d2e3f4-6789-0abc-def1-234567890abc"),
                    Name = "Sales",
                    Description = "Product sales activities."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("d2e3f4a5-7890-1bcd-ef12-34567890abcd"),
                    Name = "Customer Support",
                    Description = "Post-sales customer assistance."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("e3a18178-8db7-48f2-a76b-9ad329bba5f2"),
                    Name = "Soil Preparation",
                    Description = "Plowing and soil preparation activities using techniques aimed at minimizing the use of heavy machinery to reduce emissions."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("f3b19128-0edc-4f59-8a27-6a8d3509876c"),
                    Name = "Seeding",
                    Description = "Precision seeding activities to optimize resource use and reduce environmental impact."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("a4c2d7e6-6f5e-42f9-b7c1-1234567890ef"),
                    Name = "Sustainable Irrigation",
                    Description = "Implementation of efficient irrigation systems to reduce water and energy consumption."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("b5d3e8f7-7e6f-43d0-c8d2-0987654321ba"),
                    Name = "Low-Impact Fertilization",
                    Description = "Use of natural or slow-release fertilizers to minimize greenhouse gas emissions."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("c6e4f9a8-8f70-44e1-d9e3-1029384756cd"),
                    Name = "Pest Management",
                    Description = "Adoption of integrated pest control practices, reducing chemical pesticide use and environmental impact."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("d7f50ab9-9a81-45f2-eaf4-5647382910ef"),
                    Name = "Harvesting",
                    Description = "Optimized harvesting processes to minimize energy consumption and emissions from internal transport."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("e8a61bca-ab92-46f3-fb05-6758493021f0"),
                    Name = "Post-Harvest & Storage",
                    Description = "Selection, washing, and storage activities using low-energy impact techniques to maintain product quality."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("f9b72cdb-bc03-47f4-0c16-78695a4132f1"),
                    Name = "Eco-Friendly Packaging",
                    Description = "Use of recyclable materials and low-impact processes to reduce the carbon footprint of packaging."
                });
        }
    }
}
