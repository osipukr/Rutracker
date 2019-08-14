using System;

namespace Rutracker.Server.DataAccessLayer.Entities.Base
{
    public abstract class BaseEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }
    }
}