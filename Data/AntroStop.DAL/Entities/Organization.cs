using AntroStop.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AntroStop.DAL.Entities
{
    [Index(nameof(ID))]
    public class Organization : IntEntity
    {
        [Required]
        [MaxLength(300)]
        public string OrganizationName { get; set; }
        [Required]
        public int Itn { get; set; } //ИНН
        [Required]
        public int Msrn { get; set; } //ОГРН
        [Required]
        public string Address { get; set; }
    }
}
