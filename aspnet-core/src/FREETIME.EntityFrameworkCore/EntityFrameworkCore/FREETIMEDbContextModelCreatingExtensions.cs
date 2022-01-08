using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Docs.EntityFrameworkCore;

namespace FREETIME.EntityFrameworkCore
{
    public static class FREETIMEDbContextModelCreatingExtensions
    {
        public static void ConfigureFREETIME(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));    

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(FREETIMEConsts.DbTablePrefix + "YourEntities", FREETIMEConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //}); 
            builder.ConfigureNotifications();
            builder.ConfigureAbpDz();
 
        }
    }
}