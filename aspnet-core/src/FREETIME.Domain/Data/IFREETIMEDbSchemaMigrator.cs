using System.Threading.Tasks;

namespace FREETIME.Data
{
    public interface IFREETIMEDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
