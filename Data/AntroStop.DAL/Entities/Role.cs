using AntroStop.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace AntroStop.DAL.Entities
{
    public class Role : IntEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
