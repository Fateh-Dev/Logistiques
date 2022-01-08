using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using AbpDz.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;



namespace AbpDz
{
    public class AbpEnumService : ISingletonDependency
    {
        private readonly ServiceProvider iocManager;

        public List<AbpDzEnum> All { get; set; }
        public List<AbpDzEnum> Root { get; set; }

        public AbpEnumService(ServiceProvider iocManager)
        {

            this.iocManager = iocManager;

        }

        public void LoadData()
        {
            All = iocManager.GetService<IRepository<AbpDzEnum>>().ToList();
            Root = All.Where(k => k.ParrentId == null).ToList();
        }

        public void RegisterEnum(AbpDzEnum en)
        {
            if (Root == null)
            {
                this.LoadData();
            }
            InsertEnum(en);


        }
        public void RegisterEnum(IEnumerable<AbpDzEnum> ens)
        {
            foreach (var item in ens)
            {
                RegisterEnum(item);
            }
        }
        private void InsertEnum(AbpDzEnum en, AbpDzEnum parrent = null, ICollection<AbpDzEnum> root = null)
        {
            var dist = en;
            if (root == null)
            {
                root = Root;
            }
            var f = string.IsNullOrEmpty(en.Code) ? root.FirstOrDefault(k => k.Code == en.Code) : root.FirstOrDefault(k => k.Value == en.Value);
            if (f != null)
            {
                dist = f;
                dist.CopyFrom(en);
                en.Id = f.Id;
            }
            else
            {
                root.Add(en);
            }
            var r = iocManager.GetService<IRepository<AbpDzEnum>>();
            if (en.Id == 0 || en.Id == -1)
            {
                r.InsertAsync(en).Wait();
                All.Add(en);
            }
            if (en.Childs != null && en.Childs.Count > 0)
            {
                foreach (var e in en.Childs.ToArray())
                {
                    InsertEnum(e, en, dist.Childs);
                }
            }

        }
    }
    public class AbpEnumAppProfile : Profile
    {
        public AbpEnumAppProfile()
        {
            CreateMap<AbpDzEnum, AbpDzEnum>();
        }
    }
    [ApiExplorerSettings(GroupName = "Enum", IgnoreApi = false)]
    [Authorize("ABPDZ.Enums")]
    public class AbpEnumAppService : CrudAppService<AbpDzEnum, AbpDzEnum, int, PagedAndSortedResultRequestDto>, IApplicationService
    {
        private readonly IRepository<AbpDzEnum> repository;


        public AbpEnumAppService(IRepository<AbpDzEnum, int> repository) : base(repository)
        {
            this.repository = repository;
            this.GetPolicyName = "ABPDZ.Enums";
            this.CreatePolicyName = "ABPDZ.Enums";
            this.DeletePolicyName = "ABPDZ.Enums";
            this.UpdatePolicyName = "ABPDZ.Enums";
            this.GetListPolicyName = "ABPDZ.Enums";
        }
        [HttpGet]
        public IEnumerable<AbpDzEnum> All()
        {
            return this.repository.ToList();
        }
        [HttpPut]
        public async Task<AbpDzEnum> RestUpdate(AbpDzEnum entity)
        {
            if (entity.Id == 0)
            {
                var ret = await repository.InsertAsync(entity);
                return ret;
            }
            else
            {
                var dbContext = await this.repository.GetDbContextAsync();

                dbContext.Attach(entity);

                var updatedEntity = dbContext.Update(entity).Entity;

                await dbContext.SaveChangesAsync();

                return updatedEntity;
            }        }

        [HttpPost]
        public void Import([FromBody] IEnumerable<AbpDzEnum> ens, [FromServices] AbpEnumService service)
        {
            var di = ens.ToDictionary(k => k.Id, k => k);
            var root = new List<AbpDzEnum>();
            foreach (var item in ens)
            {
                if (item.ParrentId != null)
                {
                    if (di.ContainsKey(item.ParrentId ?? 0))
                    {
                        var p = di[item.ParrentId ?? 0];
                        if (p.Childs == null) p.Childs = new List<AbpDzEnum>();
                        p.Childs.Add(item);
                    }
                }
                else { root.Add(item); }
                item.Id = 0;
                item.ParrentId = null;
            }
            service.RegisterEnum(root);
        }
    }

}
