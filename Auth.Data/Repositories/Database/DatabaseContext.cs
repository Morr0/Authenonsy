using System.Reflection;
using Auth.Core.Models;
using Auth.Core.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data.Repositories.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opts)
        : base (opts)
        {
            
        }

        public DbSet<User> User { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<UserApplicationAccess> UserApplicationAccess { get; set; }
        public DbSet<UserApplicationCodeRequest> UserApplicationCodeRequest { get; set; }
        public DbSet<UserApplicationSession> UserApplicationSession { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}