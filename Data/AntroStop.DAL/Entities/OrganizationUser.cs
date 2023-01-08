using AntroStop.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;

namespace AntroStop.DAL.Entities
{
    [Index(nameof(ID))]
    public class OrganizationUser : IntEntity
    {
        public Organization Organization { get; set; }
        public User User { get; set; }
    }
}
