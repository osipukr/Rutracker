using System;
using Rutracker.Server.BusinessLayer.Interfaces;

namespace Rutracker.Server.BusinessLayer.Services
{
    public class DateService : IDateService
    {
        public DateTime Now()
        {
            return DateTime.UtcNow;
        }
    }
}