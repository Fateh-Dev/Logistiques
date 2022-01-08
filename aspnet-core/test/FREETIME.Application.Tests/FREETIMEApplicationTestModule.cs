using Volo.Abp.Modularity;

namespace FREETIME
{
    [DependsOn(
        typeof(FREETIMEApplicationModule),
        typeof(FREETIMEDomainTestModule)
        )]
    public class FREETIMEApplicationTestModule : AbpModule
    {

    }
}