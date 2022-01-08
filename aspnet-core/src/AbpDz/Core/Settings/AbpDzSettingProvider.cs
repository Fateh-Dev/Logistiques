using System;
using System.Threading.Tasks;
using Volo.Abp.Settings;

namespace AbpDz.Core
{

    public class AbpDzSettingProvider : SettingDefinitionProvider
    {
        public const string EnableResetPassword = "Abp.Identity.Password.EnableReset";
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
            new SettingDefinition(EnableResetPassword, "false", isVisibleToClients: true)
            // new SettingDefinition("Smtp.Port", "25"),
            // new SettingDefinition("Smtp.UserName"),
            // new SettingDefinition("Smtp.Password", isEncrypted: true),
            // new SettingDefinition("Smtp.EnableSsl", "false")
            );
        }
    }

    public class AbpDzSetting
    {
        private readonly ISettingProvider _settingProvider;

        //Inject ISettingProvider in the constructor
        public AbpDzSetting(ISettingProvider settingProvider)
        {
            _settingProvider = settingProvider;
        }

        public async Task<bool> IsEnableResetPassword()
        {

            //Get a bool value and fallback to the default value (false) if not set.
            bool EnableResetPassword = await _settingProvider.GetAsync<bool>(AbpDzSettingProvider.EnableResetPassword);
            return EnableResetPassword;
        }
    }
}
