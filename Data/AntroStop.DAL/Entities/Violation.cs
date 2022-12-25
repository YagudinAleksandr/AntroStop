using AntroStop.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace AntroStop.DAL.Entities
{
    [Index(nameof(Id))]
    public class Violation : GuidEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Coordinates { get; set; }
        [Required]
        public string Email { get; set; }
        public string Status { get; set; }
        public string Answer { get; set; }
    }
}
