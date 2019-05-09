using Microsoft.EntityFrameworkCore;
using Rutracker.Server.Core.Entities;

namespace Rutracker.Server.Core.Specifications
{
    public class TorrentsFilterSpecification : BaseSpecification<Torrent, long>
    {
        public TorrentsFilterSpecification(string search)
            : base(x => string.IsNullOrWhiteSpace(search) ||
                        EF.Functions.Like(x.Title, $"%{search}%"))
        {
        }
    }
}