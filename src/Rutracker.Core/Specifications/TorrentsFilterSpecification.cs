using Microsoft.EntityFrameworkCore;
using Rutracker.Core.Entities;

namespace Rutracker.Core.Specifications
{
    public class TorrentsFilterSpecification : BaseSpecification<Torrent, long>
    {
        public TorrentsFilterSpecification(string search)
            : base(x => string.IsNullOrWhiteSpace(search)
                        || EF.Functions.Like(x.Title, $"%{search}%"))
        {
        }
    }
}