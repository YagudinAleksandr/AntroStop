using System;

namespace AntroStop.Interfaces.Base.Entities
{
    public interface IGuidEntity
    {
        Guid Id { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset UpdatedAt { get; set;}
    }
}
