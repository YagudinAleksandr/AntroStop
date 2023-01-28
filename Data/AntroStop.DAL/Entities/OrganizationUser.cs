using AntroStop.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntroStop.DAL.Entities
{
    [Index(nameof(ID))]
    public class OrganizationUser : IntEntity
    {
        [ForeignKey("Organization")]
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }
    }
}
