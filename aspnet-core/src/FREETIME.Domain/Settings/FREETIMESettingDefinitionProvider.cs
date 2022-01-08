using Volo.Abp.Settings;

namespace FREETIME.Settings
{
    public class FREETIMESettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(FREETIMESettings.MySetting1));
        }
    }
}
