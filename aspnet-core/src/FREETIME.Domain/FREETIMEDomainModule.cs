using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FREETIME.MultiTenancy;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Emailing;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.IdentityServer;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using System.Threading.Tasks;

namespace FREETIME
{

#if DEBUG
    public class ConsoleEmailSender : NullEmailSender
    {
        public ConsoleEmailSender(IEmailSenderConfiguration configuration, IBackgroundJobManager backgroundJobManager) : base(configuration, backgroundJobManager)
        {

        }

        protected override async Task SendEmailAsync(System.Net.Mail.MailMessage mail)
        {
            System.Console.WriteLine("*********** Sending Email **************");
            System.Console.WriteLine(mail.Subject.Replace("</h1>", "").Replace("<h1>", ""));
            System.Console.WriteLine(mail.Body.Replace("</h1>", "").Replace("<h1>", ""));
            await base.SendEmailAsync(mail);
        }
    }
#endif
    [DependsOn(
        typeof(FREETIMEDomainSharedModule),
        typeof(AbpAuditLoggingDomainModule),
        typeof(AbpBackgroundJobsDomainModule),
        typeof(AbpFeatureManagementDomainModule),
        typeof(AbpIdentityDomainModule),
        typeof(AbpPermissionManagementDomainIdentityModule),
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpPermissionManagementDomainIdentityServerModule),
        typeof(AbpSettingManagementDomainModule),
        typeof(AbpTenantManagementDomainModule),
        typeof(AbpEmailingModule)
    )]
    public class FREETIMEDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });

#if DEBUG
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, ConsoleEmailSender>());
#endif
        }
    }
}
