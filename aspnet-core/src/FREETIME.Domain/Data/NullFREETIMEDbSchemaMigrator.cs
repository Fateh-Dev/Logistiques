using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace FREETIME.Data
{
    /* This is used if database provider does't define
     * IFREETIMEDbSchemaMigrator implementation.
     */
    public class NullFREETIMEDbSchemaMigrator : IFREETIMEDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}