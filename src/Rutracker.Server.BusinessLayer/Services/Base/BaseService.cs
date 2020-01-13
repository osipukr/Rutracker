using Rutracker.Server.DataAccessLayer.Interfaces;

namespace Rutracker.Server.BusinessLayer.Services.Base
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}