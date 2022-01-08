using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace FREETIME
{
    [Dependency(ReplaceServices = true)]
    public class FREETIMEBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "FREETIME";
    }
}
