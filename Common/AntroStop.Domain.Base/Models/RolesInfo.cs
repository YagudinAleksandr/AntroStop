using AntroStop.Interfaces.Base.Entities;
using System;

namespace AntroStop.Domain.Base.Models
{
    public class RolesInfo : IIntEntity
    {
        public int ID { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
