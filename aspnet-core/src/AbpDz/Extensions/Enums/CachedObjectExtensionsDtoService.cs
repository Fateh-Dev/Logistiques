
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AbpDz.Models;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending;
using Volo.Abp.Domain.Repositories;
using DependencyAttribute = Volo.Abp.DependencyInjection.DependencyAttribute;

namespace AbpDz
{
    [Dependency(ReplaceServices = true)]

    public class EnumsCachedObjectExtensionsDtoService : CachedObjectExtensionsDtoService
    {
        public IRepository<AbpDzEnum> Repository { get; set; }
        public EnumsCachedObjectExtensionsDtoService(IExtensionPropertyAttributeDtoFactory extensionPropertyAttributeDtoFactory, IRepository<AbpDzEnum> repository)
            : base(extensionPropertyAttributeDtoFactory)
        {
            Repository = repository;
        }
        public override ObjectExtensionsDto Get()
        {
            var b = base.Get();
            object l = Array.Empty<AbpDzEnum>();
            try
            {
                l = Repository.AsNoTracking().ToListAsync().Result.Select(k => k.ToDto(CultureInfo.CurrentCulture.Name));
            }
            catch (System.Exception)
            {

                throw;
            }
            if (b.Enums == null)
            {
                b.Enums = new Dictionary<string, ExtensionEnumDto>();
            }
            if (!b.Enums.ContainsKey("abpEnums"))
            {
                b.Enums.Add("abpEnums", new ExtensionEnumDto() { Fields = new List<ExtensionEnumFieldDto>() { } });
            }
            else
            {
                b.Enums["abpEnums"].Fields.Clear();
            }
            b.Enums["abpEnums"].Fields.Add(new ExtensionEnumFieldDto()
            {
                Name = "Enums",
                Value = l
            });
            return b;
        }
    }
}