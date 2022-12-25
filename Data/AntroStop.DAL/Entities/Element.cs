using AntroStop.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;

namespace AntroStop.DAL.Entities
{
    [Index(nameof(Id))]
    public class Element : GuidEntity
    {
        public string Url { get; set; }
        public string Type { get; set; }
        public Violation Violation { get; set; }
        
    }
}
