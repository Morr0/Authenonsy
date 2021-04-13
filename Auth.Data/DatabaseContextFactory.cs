using System.IO;
using Auth.Data.Repositories.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Auth.Data
{
    // Refer to https://garywoodfine.com/using-ef-core-in-a-separate-class-library-project/
    // Because this is a separate project and thus requires this
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var directory = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            builder.UseNpgsql(configuration.GetSection("DatabaseConnection").Value);

            return new DatabaseContext(builder.Options);
        }
    }
}