using FREETIME.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace FREETIME.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class FREETIMEController : AbpController
    {
        protected FREETIMEController()
        {
            LocalizationResource = typeof(FREETIMEResource);
        }
    }
}