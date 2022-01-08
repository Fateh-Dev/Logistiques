using System;
using AbpDz.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore.Modeling;


namespace Volo.Docs.EntityFrameworkCore
{

    public static class AbpDzDbContextModelBuilderExtensions
    {

        public static void ConfigureAbpDz(
          this ModelBuilder builder,
            Action<AbpModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new AbpModelBuilderConfigurationOptions(
                AbpCommonDbProperties.DbTablePrefix,
                AbpCommonDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            builder.Entity<AbpDzEnum>(b =>
            {
                b.ToTable(nameof(AbpDzEnum), options.Schema);
                b.HasIndex(k => k.ParrentId);

                b.HasIndex(k => k.Code);
                b.ConfigureByConvention();


            });
            builder.Entity<AbpDzCollection>(b =>
                       {
                           b.ToTable(nameof(AbpDzCollection), options.Schema);
                           b.HasIndex(k => k.ParrentId);
                           b.HasIndex(k => k.ChildId);

                           b.HasIndex(k => k.CollectionCode);
                           b.ConfigureByConvention();


                       });

        }
    }
}