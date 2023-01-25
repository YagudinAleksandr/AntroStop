using AntroStop.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntroStop.DAL.Entities
{
    public class Violation : GuidEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Coordinates { get; set; }
        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }
        public string Status { get; set; }
        public string Answer { get; set; }
    }
}
