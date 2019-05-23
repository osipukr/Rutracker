using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Rutracker.Core.Entities;

namespace Rutracker.Core.Specifications
{
    public class TorrentsFilterSpecification : BaseSpecification<Torrent, long>
    {
        public TorrentsFilterSpecification(string search, string titles, long? sizeFrom, long? sizeTo)
            : base(x => (string.IsNullOrWhiteSpace(search) 
                         || EF.Functions.Like(x.Title, $"%{search}%")) 
                        && (string.IsNullOrWhiteSpace(titles) 
                            || titles.Split(new []{ ',' }, StringSplitOptions.RemoveEmptyEntries).Contains(x.Forum.Title))
                        && (!sizeFrom.HasValue 
                            || x.Size >= sizeFrom)
                        && (!sizeTo.HasValue
                            || x.Size <= sizeTo))
        {
        }
    }
}