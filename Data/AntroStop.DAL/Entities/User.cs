using AntroStop.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntroStop.DAL.Entities
{
    public class User : StringEntity
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("User")]
        public int RoleID { get; set; }
        public Role Role{ get; set; }
        public bool Status { get; set; }
    }
}
