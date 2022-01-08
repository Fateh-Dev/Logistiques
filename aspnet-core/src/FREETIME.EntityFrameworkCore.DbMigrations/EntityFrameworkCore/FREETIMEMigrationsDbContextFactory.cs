using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FREETIME.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class FREETIMEMigrationsDbContextFactory : IDesignTimeDbContextFactory<FREETIMEMigrationsDbContext>
    {
        public FREETIMEMigrationsDbContext CreateDbContext(string[] args)
        {
            FREETIMEEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            DbContextOptionsBuilder<FREETIMEMigrationsDbContext> builder = null;
            var cs = configuration.GetConnectionString("Default");
            if (cs.Contains("DataSource="))
            {
                builder = new DbContextOptionsBuilder<FREETIMEMigrationsDbContext>()
                    .UseSqlite(cs);
            }
            else if (cs.ToLower().Contains("sslmode="))
            {
                builder = new DbContextOptionsBuilder<FREETIMEMigrationsDbContext>()
                    .UseNpgsql(cs);
            }
            else // if (cs.Contains("Server="))
            {
                builder = new DbContextOptionsBuilder<FREETIMEMigrationsDbContext>()
                  //     .UseSqlServer(cs);
                  .UseSqlite(cs);
            }
            return new FREETIMEMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../FREETIME.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
