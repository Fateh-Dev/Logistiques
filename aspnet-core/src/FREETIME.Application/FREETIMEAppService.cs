using System;
using System.Collections.Generic;
using System.Text;
using FREETIME.Localization;
using Volo.Abp.Application.Services;

namespace FREETIME
{
    /* Inherit your application services from this class.
     */
    public abstract class FREETIMEAppService : ApplicationService
    {
        protected FREETIMEAppService()
        {
            LocalizationResource = typeof(FREETIMEResource);
        }
    }
}
