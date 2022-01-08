using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FREETIME.Data;
using Volo.Abp.DependencyInjection;

namespace FREETIME.EntityFrameworkCore
{
    public class EntityFrameworkCoreFREETIMEDbSchemaMigrator
        : IFREETIMEDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreFREETIMEDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the FREETIMEMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<FREETIMEMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}