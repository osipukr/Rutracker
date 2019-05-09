using System;

namespace Rutracker.Server.Core.Entities
{
    public class BaseEntity<TPrimaryKey> 
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}  