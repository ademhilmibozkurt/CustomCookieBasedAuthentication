using CustomCookieBased.Configurations;
using CustomCookieBased.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomCookieBased.Context
{
    public class CookieContext : DbContext
    {
        public DbSet<AppUser> Users { get; set; }   
        public DbSet<AppRole> Roles { get; set; }   
        public DbSet<AppUserRole> UserRoles { get; set; }   

        public CookieContext(DbContextOptions<CookieContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserRoleConfiguration());
        }
    }
}
