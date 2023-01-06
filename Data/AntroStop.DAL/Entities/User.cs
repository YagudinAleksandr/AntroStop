using AntroStop.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AntroStop.DAL.Entities
{
    [Index(nameof(Username))]
    public class User : StringEntity
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public int RoleID { get; set; }
    }
}
