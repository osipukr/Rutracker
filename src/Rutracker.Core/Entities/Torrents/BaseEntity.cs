using System;

namespace Rutracker.Core.Entities.Torrents
{
    public abstract class BaseEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}