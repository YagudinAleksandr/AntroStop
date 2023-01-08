using AntroStop.Interfaces.Base.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace AntroStop.DAL.Entities.Base
{
    public class IntEntity : IIntEntity
    {
        [Key]
        public int ID { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
