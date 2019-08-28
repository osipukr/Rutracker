using System;
using System.Linq;
using System.Threading.Tasks;
using Rutracker.Server.DataAccessLayer.Contexts;
using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces;

namespace Rutracker.Server.DataAccessLayer.Repositories
{
    public class CommentRepository : Repository<Comment, int>, ICommentRepository
    {
        public CommentRepository(RutrackerContext context) : base(context)
        {
        }

        public IQueryable<Comment> GetAll(int torrentId)
        {
            return GetAll(x => x.TorrentId == torrentId);
        }

        public IQueryable<Comment> GetAll(string userId)
        {
            return GetAll(x => x.UserId == userId);
        }

        public async Task<Comment> GetAsync(int commentId, string userId)
        {
            return await GetAsync(x => x.Id == commentId && x.UserId == userId);
        }

        public override Task AddAsync(Comment entity)
        {
            entity.CreatedAt = DateTime.Now;

            return base.AddAsync(entity);
        }

        public override void Update(Comment entity)
        {
            entity.IsModified = true;
            entity.LastModifiedAt = DateTime.Now;

            base.Update(entity);
        }
    }
}