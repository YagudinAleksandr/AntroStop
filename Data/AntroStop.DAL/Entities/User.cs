using AntroStop.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AntroStop.DAL.Entities
{
    [Index(nameof(ID))]
    public class User : StringEntity
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        public Role Role{ get; set; }
        public bool Status { get; set; }
    }
}
