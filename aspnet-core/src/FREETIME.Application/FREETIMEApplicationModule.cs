using AbpDz.Extensions;
using AbpDz.Notifications;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace FREETIME
{
    [DependsOn(
        typeof(FREETIMEDomainModule),
        typeof(AbpDzNotificationsModule),
        typeof(AbpDzExtensionsModule),
        typeof(AbpAccountApplicationModule),
        typeof(FREETIMEApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule)
        )]
    public class FREETIMEApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<FREETIMEApplicationModule>();
            });
        }
    }
}
