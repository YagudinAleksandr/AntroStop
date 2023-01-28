using AntroStop.DAL.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntroStop.DAL.Entities
{
    public class Element : GuidEntity
    {
        [Required]
        public string FileName { get; set; }
        [Required]
        public string Type { get; set; }
        [ForeignKey("Violation")]
        public Guid ViolationID { get; set; }
        public Violation Violation { get; set; }
    }
}
