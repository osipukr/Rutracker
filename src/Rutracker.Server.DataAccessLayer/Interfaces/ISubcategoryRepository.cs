﻿using Rutracker.Server.DataAccessLayer.Entities;
using Rutracker.Server.DataAccessLayer.Interfaces.Base;

namespace Rutracker.Server.DataAccessLayer.Interfaces
{
    public interface ISubcategoryRepository : IRepository<Subcategory, int>
    {
    }
}