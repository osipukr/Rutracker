using System.Collections.Generic;
using Rutracker.Server.DataAccessLayer.Entities.Base;

namespace Rutracker.Server.DataAccessLayer.Entities
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}