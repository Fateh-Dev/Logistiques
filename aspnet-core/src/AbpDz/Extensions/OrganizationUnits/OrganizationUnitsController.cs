using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbpDz.Core;
using AbpDz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace AbpDz.Notifications
{

    [RemoteService]
    [Authorize]
    [ApiExplorerSettings(GroupName = "Notification")]
    [Route("/api/identity/organization-units/")]
    public class OrganizationUnitsController : AbpController, Volo.Abp.DependencyInjection.ITransientDependency
    {
        public IRepository<OrganizationUnit> Repository { get; }

        public OrganizationUnitManager organizationUnitManager { get; }

        public OrganizationUnitsController(
            IRepository<OrganizationUnit> repository
            , OrganizationUnitManager organizationUnitManager)
        {
            Repository = repository;
            this.organizationUnitManager = organizationUnitManager;
        }

        // [HttpPost]
        // public async Task AddRoleToOrganizationUnitAsync(Guid id, Guid roleId)
        // {
        //     await organizationUnitManager.AddRoleToOrganizationUnitAsync(roleId, id);
        // }
        [HttpDelete]
        public async Task Delete(Guid id)
        {
            await organizationUnitManager.DeleteAsync(id);
        }
        [HttpPost]
        public async Task<OrganizationUnit> Create(OrganizationUnit org)
        {
            // var context = await this.Repository.GetDbContextAsync();
            // var dbset = await this.Repository.GetDbSetAsync();
            if (org.Id == Guid.Empty)
            {
                // dbset.Add(org);

                await this.organizationUnitManager.CreateAsync(org);
            }
            else
            {
                // context.Entry(org).State = EntityState.Modified;
                await this.organizationUnitManager.UpdateAsync(org);
            }
            // await context.SaveChangesAsync();
            return org;
        }

        [HttpGet("all")]
        public async Task<PagedResultDto<OrganizationUnit>> All(EventFilterDto f)
        {
            var parrentIsNull = string.IsNullOrWhiteSpace(f.Parent);
            Guid parrent = Guid.Empty;
            if (!parrentIsNull && Guid.TryParse(f.Parent, out parrent)) { parrentIsNull = false; }
            var fv = string.IsNullOrWhiteSpace(f.Filter);
            var query = (await Repository.GetDbSetAsync()).Where(k =>
                (parrentIsNull || k.ParentId == parrent)
             && (fv || k.DisplayName.ToLower().Contains(f.Filter.ToLower()))
            );


            var r = await query.ToListAsync();
            return new PagedResultDto<OrganizationUnit>(r.Count, r);
        }
    }
}