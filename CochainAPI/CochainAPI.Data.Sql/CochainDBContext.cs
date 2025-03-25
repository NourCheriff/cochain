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

            modelBuilder.Entity<Transaction>().HasOne(x => x.supplyChainPartnerEmitter).WithMany(x => x.EmittedTransactions).HasForeignKey(x => x.WalletIdEmitter).HasPrincipalKey(x => x.WalletId);
            modelBuilder.Entity<Transaction>().HasOne(x => x.supplyChainPartnerReceiver).WithMany(x => x.ReceivedTransactions).HasForeignKey(x => x.WalletIdReceiver).HasPrincipalKey(x => x.WalletId);

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
                    Id = new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"),
                    Name = "Prova company",
                    Email = "company@prova.com",
                    Phone = "33309090909",
                    Credits = 0,
                    SupplyChainPartnerTypeId = new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"),
                    WalletId = "0x3a9f1b7c5d2e8a4f6c0e7d3b5a2f9c1"
                },
                new SupplyChainPartner
                {
                    Id = new Guid("3a9f1b7c-5d2e-4a4f-8a6c-0e7d3b5a2f9c"),
                    Name = "Prova company2",
                    Email = "company2@prova.com",
                    Phone = "3669045897",
                    Credits = 0,
                    SupplyChainPartnerTypeId = new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"),
                    WalletId = "0x7c5d1a3f9b2e6f0d8c4a7e3b5c2f9d1"
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("ad00648b-a031-432d-b007-6a0829cf5292").ToString(),
                    FirstName = "System",
                    LastName = "System",
                    UserName = "admin@cochain.com",
                    SupplyChainPartnerId = new Guid("d65e685f-8bdd-470b-a6b8-c9a62e39f095"),
                    IsActive = true
                },
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
                    Name = "Materia Prima",
                    Baseline = 1000.0f
                });

            modelBuilder.Entity<SupplyChainPartnerType>().HasData(
                new SupplyChainPartnerType
                {
                    Id = new Guid("ef0e7db4-760e-4515-9aa0-bda3fc766e87"),
                    Name = "Trasportatore",
                    Baseline = 1000.0f
                });

            modelBuilder.Entity<SupplyChainPartnerType>().HasData(
                new SupplyChainPartnerType
                {
                    Id = new Guid("ef01b3b4-760e-4515-9aa0-bdab7c766e87"),
                    Name = "Trasformazione",
                    Baseline = 1000.0f
                });

            modelBuilder.Entity<SupplyChainPartnerType>().HasData(
                new SupplyChainPartnerType
                {
                    Id = new Guid("ab2e7db4-760e-4515-9aa0-bda314266e87"),
                    Name = "Stoccaggio",
                    Baseline = 1000.0f
                });


            modelBuilder.Entity<SupplyChainPartnerType>().HasData(
                new SupplyChainPartnerType
                {
                    Id = new Guid("ef0e7124-744e-1115-9ba0-bda3fc766e87"),
                    Name = "Rivenditore Dettaglio",
                    Baseline = 1000.0f
                });

            modelBuilder.Entity<SupplyChainPartnerType>().HasData(
                new SupplyChainPartnerType
                {
                    Id = new Guid("eaae7124-761e-4515-9aa0-bda3fc7aee87"),
                    Name = "Grossista",
                    Baseline = 1000.0f
                });

            modelBuilder.Entity<ProductLifeCycleCategory>().HasData(
                new ProductLifeCycleCategory
                {
                    Id = new Guid("ef94c672-c755-449b-8ee8-327a12bed7ef"),
                    Name = "Trasporto",
                    Description = "Trasporto del prodotto."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("a9d12b5f-1e2d-45c9-bb5d-3d8a7c2b4a33"),
                    Name = "Produzione",
                    Description = "Attività di produzione del prodotto."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("b1e2d3f4-5678-90ab-cdef-1234567890ab"),
                    Name = "Lavorazione",
                    Description = "Attività di lavorazione della materia prima."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("c1d2e3f4-6789-0abc-def1-234567890abc"),
                    Name = "Vendita",
                    Description = "Attività di vendita del prodotto."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("d2e3f4a5-7890-1bcd-ef12-34567890abcd"),
                    Name = "Assistenza",
                    Description = "Attività di assistenza post vendita."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("e3a18178-8db7-48f2-a76b-9ad329bba5f2"),
                    Name = "Preparazione del Terreno",
                    Description = "Attività di aratura e lavorazione del suolo, con tecniche volte a minimizzare l'uso di macchinari pesanti per ridurre le emissioni."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("f3b19128-0edc-4f59-8a27-6a8d3509876c"),
                    Name = "Semina",
                    Description = "Attività di semina utilizzando metodi di precisione per ottimizzare l'utilizzo di risorse e ridurre l'impatto ambientale."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("a4c2d7e6-6f5e-42f9-b7c1-1234567890ef"),
                    Name = "Irrigazione Sostenibile",
                    Description = "Implementazione di sistemi di irrigazione efficienti per ridurre il consumo idrico e l'energia necessaria."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("b5d3e8f7-7e6f-43d0-c8d2-0987654321ba"),
                    Name = "Fertilizzazione a Basso Impatto",
                    Description = "Utilizzo di fertilizzanti naturali o a rilascio controllato per minimizzare le emissioni di gas serra."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("c6e4f9a8-8f70-44e1-d9e3-1029384756cd"),
                    Name = "Gestione dei Parassiti",
                    Description = "Adozione di pratiche integrate per il controllo dei parassiti, riducendo l'uso di pesticidi chimici e l'impatto ambientale."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("d7f50ab9-9a81-45f2-eaf4-5647382910ef"),
                    Name = "Raccolta",
                    Description = "Processi di raccolta ottimizzati per minimizzare il consumo energetico e le emissioni dovute al trasporto interno."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("e8a61bca-ab92-46f3-fb05-6758493021f0"),
                    Name = "Post-Raccolta e Conservazione",
                    Description = "Attività di selezione, lavaggio e conservazione con tecniche a basso impatto energetico per mantenere la qualità del prodotto."
                },
                new ProductLifeCycleCategory
                {
                    Id = new Guid("f9b72cdb-bc03-47f4-0c16-78695a4132f1"),
                    Name = "Imballaggio Eco-Sostenibile",
                    Description = "Utilizzo di materiali riciclabili e processi a basso impatto per ridurre la carbon footprint del packaging."
                });
        }

    }
}
