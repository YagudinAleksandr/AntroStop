using System;

namespace AntroStop.Interfaces.Base.Entities
{
    public interface IStringEntity
    {
        string ID { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset UpdatedAt { get; set; }
    }
}
