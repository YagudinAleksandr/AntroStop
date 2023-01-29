using AntroStop.Interfaces.Base.Entities;
using System;

namespace AntroStop.Domain.Base.Models
{
    public class ViolationsInfo : IGuidEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string Description { get; set; }
        public string Coordinates { get; set; }
        public string Status { get; set; }
    }
}
