using AntroStop.Interfaces.Base.Entities;
using System;

namespace AntroStop.DAL.Entities.Base
{
    public class GuidEntity : IGuidEntity
    {
        public Guid Id { get; set; }
    }
}
