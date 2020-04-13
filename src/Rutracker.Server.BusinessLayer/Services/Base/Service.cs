using System.Linq;
using System.Threading.Tasks;
using Rutracker.Server.BusinessLayer.Collections;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Interfaces.Base;
using Rutracker.Shared.Infrastructure.Collections;
using Rutracker.Shared.Infrastructure.Filters;

namespace Rutracker.Server.BusinessLayer.Services.Base
{
    public abstract class Service
    {
        protected readonly IUnitOfWork<RutrackerContext> _unitOfWork;

        protected Service(IUnitOfWork<RutrackerContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected async Task<IPagedList<TResult>> ApplyFilterAsync<TResult>(IQueryable<TResult> query, IPagedFilter filter)
        {
            var pagedList = new PagedList<TResult>(query, filter.Page, filter.PageSize, filter.PageIndexFrom);

            return await Task.FromResult(pagedList);
        }
    }
}