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
using Volo.Abp.Auditing;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Dynamic.Core;
namespace AbpDz.Notifications
{

    [RemoteService]
    [Authorize]
    [ApiExplorerSettings(GroupName = "Notification")]
    [Route("/api/audit-logging/")]
    public class AuditLogController : AbpController, Volo.Abp.DependencyInjection.ITransientDependency
    {
        public IRepository<AuditLog, Guid> Repository { get; }


        public AuditLogController(
            IRepository<AuditLog, Guid> repository
            )
        {
            Repository = repository;
        }
        [HttpGet("audit-logs")]
        public async Task<PagedResultDto<AuditLog>> GetAll(EventFilterDto f)
        {

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
                (filterIsNull || (k.Url.Contains(f.Filter) || k.UserName.ToLower() == f.Filter.ToLower() || k.ClientName == f.Filter)) &&
                (ipIsNull || (k.ClientIpAddress == f.Ip)) &&
                (userIsNull || (k.UserId == f.UserId)) &&
                (urlIsNull || (k.Url == f.Url)) &&
                (f.StartDate.HasValue == false || (k.ExecutionTime >= f.StartDate)) &&
                (f.EndDate.HasValue == false || (k.ExecutionTime <= f.EndDate))

                );

            if (string.IsNullOrWhiteSpace(f.Sorting))
            {
                f.Sorting = nameof(AuditLog.ExecutionTime) + " desc";
            }

            return new PagedResultDto<AuditLog>(
                await query.CountAsync(),
                await query.OrderBy(f.Sorting)
                .Skip(f.SkipCount)
                .Take(f.MaxResultCount).ToListAsync()
            );
        }
    }
}