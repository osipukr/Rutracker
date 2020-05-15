using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Interfaces;
using Rutracker.Server.DataAccessLayer.Repositories;
using Rutracker.Server.DataAccessLayer.Services.Base;

namespace Rutracker.Server.DataAccessLayer.Services
{
    public class RutrackerUnitOfWork : UnitOfWork<RutrackerContext>
    {
        public RutrackerUnitOfWork(RutrackerContext context) : base(context)
        {
            AddRepository<ICategoryRepository, CategoryRepository>();
            AddRepository<ISubcategoryRepository, SubcategoryRepository>();
            AddRepository<ITorrentRepository, TorrentRepository>();
            AddRepository<IFileRepository, FileRepository>();
            AddRepository<ICommentRepository, CommentRepository>();
            AddRepository<ILikeRepository, LikeRepository>();
        }
    }
}