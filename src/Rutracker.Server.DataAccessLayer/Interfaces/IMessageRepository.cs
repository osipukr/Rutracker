﻿using Rutracker.Server.DataAccessLayer.Entities;

namespace Rutracker.Server.DataAccessLayer.Interfaces
{
    public interface IMessageRepository : IRepository<Message, int>
    {
    }
}