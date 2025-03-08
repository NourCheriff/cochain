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

            modelBuilder.Entity<User>().HasOne(x => x.SupplyChainPartner).WithMany().HasForeignKey(x => x.SupplyChainPartnerId);
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

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = Guid.Parse("8e342ad6-78d9-4aee-abe5-245b1fae6c4a").ToString(),
                    Name = "SystemAdmin",
                    NormalizedName = "System Administrator"
                },
                new IdentityRole
                {
                    Id = Guid.Parse("cb5b1ae5-43db-4096-9bd6-2afb90fb20c5").ToString(),
                    Name = "AdminSCP",
                    NormalizedName = "Admin Supply Chain Partner"
                },
                new IdentityRole
                {
                    Id = Guid.Parse("f6e7ea1d-a99e-4a5c-9a23-2274ba2c62ea").ToString(),
                    Name = "AdminCA",
                    NormalizedName = "Admin Certification Authority"
                },
                new IdentityRole
                {
                    Id = Guid.Parse("a0e8b03e-0cd8-4458-a147-1a4b88df2997").ToString(),
                    Name = "UserSCP",
                    NormalizedName = "User Supply Chain Partner"
                },
                new IdentityRole
                {
                    Id = Guid.Parse("3318013d-0cdd-4749-9ab3-6fdca9b64564").ToString(),
                    Name = "UserCA",
                    NormalizedName = "User Certification Authority"
                },
                new IdentityRole
                {
                    Id = Guid.Parse("916a3160-05e5-4821-88fe-9e46a43d157c").ToString(),
                    Name = "SCPTransporter",
                    NormalizedName = "Supply Chain Partner Transporter"
                },
                new IdentityRole
                {
                    Id = Guid.Parse("cccf28ca-b2f8-477f-a2c1-2436cd83ec0c").ToString(),
                    Name = "SCPRawMaterial",
                    NormalizedName = "Supply Chain Partner Raw Material"
                },
                new IdentityRole
                {
                    Id = Guid.Parse("f08d90db-ac61-4c92-a229-ef803b672e60").ToString(),
                    Name = "SCPTransformator",
                    NormalizedName = "Supply Chain Partner Transformator"
                });

            modelBuilder.Entity<SupplyChainPartnerType>().HasData(
                new SupplyChainPartnerType
                {
                    Id = new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"),
                    Name = "Trasporto",
                    Baseline = 1000.0f
                }
            );

            modelBuilder.Entity<SupplyChainPartner>().HasData(
                new SupplyChainPartner
                {
                    Id = new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"),
                    Name = "Prova company",
                    Email = "company@prova.com",
                    Phone = "33309090909",
                    Credits = 0,
                    SupplyChainPartnerTypeId = new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87")
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("5e4b0ca8-aa85-417a-af23-035ac1b555cd").ToString(),
                    FirstName = "Paolo",
                    LastName = "Roselli",
                    UserName = "paolo.roselli02@gmail.com",
                    SupplyChainPartnerId = new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"),
                    IsActive = true
                },
                new User
                {
                    Id = Guid.Parse("0a317b04-2f87-4b08-8ad8-597103527584").ToString(),
                    FirstName = "Matteo",
                    LastName = "Spiga",
                    UserName = "matteospiga2002@gmail.com",
                    SupplyChainPartnerId = new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"),
                    IsActive = true
                },
                new User
                {
                    Id = Guid.Parse("a12c3708-0486-4603-b1a5-46d252e79082").ToString(),
                    FirstName = "Cherif",
                    LastName = "Nour",
                    UserName = "nourcherif.pitos25@gmail.com",
                    SupplyChainPartnerId = new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"),
                    IsActive = true
                },
                new User
                {
                    Id = Guid.Parse("3542da56-0de3-4797-a059-effff257f63d").ToString(),
                    FirstName = "Mattia",
                    LastName = "Mandorlini",
                    UserName = "mando3228@gmail.com",
                    SupplyChainPartnerId = new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"),
                    IsActive = true
                },
                new User
                {
                    Id = Guid.Parse("f4242b5f-4b39-45fc-802e-391293414546").ToString(),
                    FirstName = "Samuele",
                    LastName = "Sacchetti",
                    UserName = "sacchettisamuele@gmail.com",
                    SupplyChainPartnerId = new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"),
                    IsActive = true
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
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
        }
    }
}
