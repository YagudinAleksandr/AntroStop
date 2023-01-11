using AntroStop.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AntroStop.DAL.Entities
{
    [Index(nameof(ID))]
    public class User : StringEntity
    {
        [Required]
        [JsonIgnore]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        public Role Role{ get; set; }
        public bool Status { get; set; }
    }
}
