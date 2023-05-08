using AntroStop.Interfaces.Base.Entities;
using System;

namespace AntroStop.Domain.Base.Models
{
    public class ElementsInfo : IGuidEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid ViolationID { get; set; }
        public string FileName { get; set; }
        public string Type { get; set; }
    }
}
