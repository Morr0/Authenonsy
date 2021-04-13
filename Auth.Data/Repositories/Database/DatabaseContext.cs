using System.Reflection;
using Auth.Core.Models;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}