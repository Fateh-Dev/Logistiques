using System;
using System.Linq;
using System.Threading.Tasks;
using AbpDz.Models;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Data;
using AbpDz.Core;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Identity;
using System.Linq.Dynamic.Core;
namespace AbpDz.Notifications
{

    [RemoteService]
    [Authorize]
    [ApiExplorerSettings(GroupName = "Notification")]
    [Route("/api/identity/security-logs/[action]")]
    public class IdentitySecurityLogController : AbpController, Volo.Abp.DependencyInjection.ITransientDependency
    {
        public IIdentitySecurityLogRepository Repository { get; }


        public IdentitySecurityLogController(
            IIdentitySecurityLogRepository repository
            )
        {
            Repository = repository;
        }
        [HttpGet]
        public async Task<PagedResultDto<IdentitySecurityLog>> GetAll(EventFilterDto f)
        {
            if (CurrentUser.Id != f.UserId && !await AuthorizationService.IsGrantedAsync("AbpIdentity.Users.Create"))
            {
                f.UserId = CurrentUser.Id;
            }

            if (string.IsNullOrWhiteSpace(f.Sorting))
            {
                f.Sorting = nameof(IdentitySecurityLog.CreationTime) + " desc";
            }
            var filterIsNull = string.IsNullOrWhiteSpace(f.Filter);
            if (!filterIsNull)
            {
                f.Filter = f.Filter.Trim();
            }
            var urlIsNull = string.IsNullOrWhiteSpace(f.Url);
            var ipIsNull = string.IsNullOrWhiteSpace(f.Ip);
            if (!ipIsNull)
            {
                f.Ip = f.Ip.Trim();
            }
            if (CurrentUser.Id != f.UserId && !await AuthorizationService.IsGrantedAsync("AbpIdentity.Users.Create"))
            {
                f.UserId = CurrentUser.Id;
            }
            var userIsNull = !f.UserId.HasValue;

            var query = (await Repository.GetDbSetAsync()).Where(k =>
               (filterIsNull || (k.BrowserInfo.Contains(f.Filter) || k.UserName.ToLower() == f.Filter.ToLower())) &&
               (ipIsNull || (k.ClientIpAddress == f.Ip)) &&
               (userIsNull || (k.UserId == f.UserId)) &&
               (f.StartDate.HasValue == false || (k.CreationTime >= f.StartDate)) &&
               (f.EndDate.HasValue == false || (k.CreationTime <= f.EndDate))

               );
            return new PagedResultDto<IdentitySecurityLog>(
                await query.CountAsync(),
                await query.OrderBy(f.Sorting).Skip(f.SkipCount).Take(f.MaxResultCount).ToListAsync()
            );
        }
    }
}