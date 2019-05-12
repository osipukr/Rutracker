using System;

namespace Rutracker.Core.Entities
{
    public class BaseEntity<TPrimaryKey> 
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}  