using System;

namespace AntroStop.Interfaces.Base.Entities
{
    public interface IIntEntity
    {
        int ID { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset UpdatedAt { get; set; }
    }
}
