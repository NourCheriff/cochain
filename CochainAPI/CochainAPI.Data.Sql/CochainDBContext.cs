using Microsoft.EntityFrameworkCore;
using CochainAPI.Model.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CochainAPI.Model.CompanyEntities;
using CochainAPI.Model.Product;
using CochainAPI.Model.CarbonOffset;
using CochainAPI.Model.Documents;
using System.Security.Cryptography;

namespace CochainAPI.Data.Sql
{
    public class CochainDBContext : IdentityDbContext<User>
    {
        public CochainDBContext(DbContextOptions<CochainDBContext> options) : base(options) { }

        public override DbSet<User> Users { get; set; }
        public DbSet<UserTemporaryPassword> UserTemporaryPassword { get; set; }
        public DbSet<CertificationAuthority> CertificationAuthority { get; set; }
        public DbSet<CompanyType> CompanyType { get; set; }
        public DbSet<SupplyChainPartner> SupplyChainPartner { get; set; }
        public DbSet<SupplyChainPartnerType> SupplyChainPartnerType { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductInfo> ProductInfo { get; set; }
        public DbSet<ProductIngredient> ProductIngredient { get; set; }
        public DbSet<ProductLifeCycleCategory> ProductLifeCycleCategory { get; set; }
        public DbSet<ProductLifeCycle> ProductLifeCycle { get; set; }
        public DbSet<ProductLifeCycleDocument> ProductLifeCycleDocument { get; set; }
        

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

            modelBuilder.Entity<User>().HasMany(x => x.TemporaryPasswords).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<User>().HasMany(x => x.UserRoles).WithOne().HasForeignKey(x => x.UserId);
            modelBuilder.Entity<User>().HasMany(x => x.UserClaims).WithOne().HasForeignKey(x => x.UserId);

            modelBuilder.Entity<CertificationAuthority>().HasOne(x => x.CompanyType).WithMany().HasForeignKey(x => x.CompanyTypeId);
            modelBuilder.Entity<SupplyChainPartner>().HasOne(x => x.CompanyType).WithMany().HasForeignKey(x => x.CompanyTypeId);
            modelBuilder.Entity<SupplyChainPartner>().HasOne(x => x.SupplyChainPartnerType).WithMany().HasForeignKey(x => x.SupplyChainPartnerTypeId);

            modelBuilder.Entity<Product>().HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId);
            modelBuilder.Entity<ProductInfo>().HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<ProductInfo>().HasOne(x => x.SupplyChainPartner).WithMany().HasForeignKey(x => x.SupplyChainPartnerId);
            modelBuilder.Entity<ProductInfo>().HasMany(x => x.Ingredients).WithOne(x => x.ProductInfo).HasForeignKey(x => x.ProductInfoId);
            modelBuilder.Entity<ProductInfo>().HasMany(x => x.ProductLifeCycle).WithOne(x => x.ProductInfo).HasForeignKey(x => x.ProductInfoId);
            modelBuilder.Entity<ProductLifeCycle>().HasOne(x => x.ProductLifeCycleCategory).WithMany().HasForeignKey(x => x.ProductLifeCycleCategoryId);
            modelBuilder.Entity<ProductLifeCycle>().HasOne(x => x.SupplyChainPartner).WithMany().HasForeignKey(x => x.SupplyChainPartnerId);
            modelBuilder.Entity<ProductIngredient>(entity =>
            {
                entity.HasKey(r => new { r.ProductInfoId, r.IngredientId });
                entity.HasOne(x => x.ProductInfo).WithMany().HasForeignKey(x => x.ProductInfoId);
                entity.HasOne(x => x.Ingredient).WithMany().HasForeignKey(x => x.IngredientId);
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(d => d.UserEmitter).WithMany(u => u.EmittedDocuments).HasForeignKey(d => d.UserEmitterId);
                entity.HasOne(d => d.UserReceiver).WithMany(u => u.ReceivedDocuments).HasForeignKey(d => d.UserReceiverId);
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(d => d.UserEmitter).WithMany(u => u.EmittedContract).HasForeignKey(d => d.UserEmitterId);
                entity.HasOne(d => d.UserReceiver).WithMany(u => u.ReceivedContract).HasForeignKey(d => d.UserReceiverId);
            });

            modelBuilder.Entity<ProductDocument>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(d => d.UserEmitter).WithMany(u => u.EmittedProductDocument).HasForeignKey(d => d.UserEmitterId);
                entity.HasOne(d => d.UserReceiver).WithMany(u => u.ReceivedProductDocument).HasForeignKey(d => d.UserReceiverId);
            });

            modelBuilder.Entity<ProductLifeCycleDocument>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(d => d.UserEmitter).WithMany(u => u.EmittedProductLifeCycleDocument).HasForeignKey(d => d.UserEmitterId);
                entity.HasOne(d => d.UserReceiver).WithMany(u => u.ReceivedProductLifeCycleDocument).HasForeignKey(d => d.UserReceiverId);
            });

            modelBuilder.Entity<SupplyChainPartnerCertificate>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(d => d.UserEmitter).WithMany(u => u.EmittedSupplyChainPartnerCertificate).HasForeignKey(d => d.UserEmitterId);
                entity.HasOne(d => d.UserReceiver).WithMany(u => u.ReceivedSupplyChainPartnerCertificate).HasForeignKey(d => d.UserReceiverId);
            });

            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("8e342ad6-78d9-4aee-abe5-245b1fae6c4a"),
                    Name = "Admin",
                    NormalizedName = "Admin"
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("cb5b1ae5-43db-4096-9bd6-2afb90fb20c5"),
                    Name = "SupplyChainPartner",
                    NormalizedName = "SupplyChainPartner"
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("f6e7ea1d-a99e-4a5c-9a23-2274ba2c62ea"),
                    Name = "Certifier",
                    NormalizedName = "Certifier"
                });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("5e4b0ca8-aa85-417a-af23-035ac1b555cd").ToString(),
                    FirstName = "System",
                    LastName = "System",
                    Email = "System",
                    UserName = "System"
                }
            );

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    UserId = Guid.Parse("5e4b0ca8-aa85-417a-af23-035ac1b555cd"),
                    RoleId = Guid.Parse("8e342ad6-78d9-4aee-abe5-245b1fae6c4a")
                });

            modelBuilder.Entity<UserTemporaryPassword>().HasData(
                new UserTemporaryPassword
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("5e4b0ca8-aa85-417a-af23-035ac1b555cd").ToString(),
                    Password = "System",
                    ExpirationDate = DateTime.UtcNow.AddYears(2),
                    IsUsed = false
                }
            );

            modelBuilder.Entity<CompanyType>().HasData(
                new CompanyType
                {
                    Id = new Guid("6173d450-c48a-4f24-82f6-f012413ff6f4"),
                    Name = "SCP"
                }
            );

            modelBuilder.Entity<SupplyChainPartnerType>().HasData(
                new SupplyChainPartnerType
                {
                    Id = new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"),
                    Name = "Trasporto"
                }
            );

            modelBuilder.Entity<SupplyChainPartner>().HasData(
                new SupplyChainPartner
                {
                    Id = Guid.NewGuid(),
                    Name = "Prova company",
                    Email = "company@prova.com",
                    Phone = "33309090909",
                    CompanyTypeId = new Guid("6173d450-c48a-4f24-82f6-f012413ff6f4"),
                    Credits = 0,
                    SupplyChainPartnerTypeId = new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87")
                }
            );
        }
    }
}
