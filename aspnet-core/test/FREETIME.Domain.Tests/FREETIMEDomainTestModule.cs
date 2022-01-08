using FREETIME.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace FREETIME
{
    [DependsOn(
        typeof(FREETIMEEntityFrameworkCoreTestModule)
        )]
    public class FREETIMEDomainTestModule : AbpModule
    {

    }
}