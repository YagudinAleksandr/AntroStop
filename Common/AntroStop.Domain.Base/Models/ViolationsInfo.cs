using AntroStop.Interfaces.Base.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace AntroStop.Domain.Base.Models
{
    public class ViolationsInfo : IGuidEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        [Required(ErrorMessage ="Заголовок заявки должен быть заполнен")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Описание должно быть заполнено")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Координаты не указаны")]
        public string Coordinates { get; set; }
        public string UserID { get; set; }
        public string Status { get; set; }
        public string Answer { get; set; }
    }
}
