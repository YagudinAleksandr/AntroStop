using AntroStop.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;

namespace AntroStop.DAL.Entities
{
    [Index(nameof(ID))]
    public class Role : IntEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
