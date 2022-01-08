using FREETIME.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace FREETIME.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(FREETIMEEntityFrameworkCoreDbMigrationsModule),
        typeof(FREETIMEApplicationContractsModule)
        )]
    public class FREETIMEDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
