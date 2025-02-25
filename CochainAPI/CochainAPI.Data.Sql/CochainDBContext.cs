using Microsoft.EntityFrameworkCore;
using CochainAPI.Model.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CochainAPI.Data.Sql
{
    public class CochainDBContext: IdentityDbContext<User>
    {
        public CochainDBContext(DbContextOptions<CochainDBContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<UserTemporaryPassword> UserTemporaryPassword { get; set; }
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

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = "ID1",
                    FirstName = "System",
                    LastName = "System",
                    Email = "System",
                    UserName = "System",
                    Password = "System",
                }
            );
        }
    }
}
