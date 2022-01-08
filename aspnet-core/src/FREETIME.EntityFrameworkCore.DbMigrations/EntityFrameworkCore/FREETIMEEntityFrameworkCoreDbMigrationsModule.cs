using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace FREETIME.EntityFrameworkCore
{
    [DependsOn(
        typeof(FREETIMEEntityFrameworkCoreModule)
        )]
    public class FREETIMEEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<FREETIMEMigrationsDbContext>();
        }
    }
}
