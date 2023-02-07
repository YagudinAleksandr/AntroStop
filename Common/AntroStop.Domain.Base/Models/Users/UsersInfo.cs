using AntroStop.Interfaces.Base.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace AntroStop.Domain.Base.Models.Users
{
    public class UsersInfo : IStringEntity
    {
        [EmailAddress(ErrorMessage ="Недопустимый адрес E-mail")]
        [Required(ErrorMessage = "Не указан E-mail")]
        public string ID { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Не указано имя пользователя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указана роль пользователя")]
        [Range(1,3,ErrorMessage ="Укажите роль пользователя из списка")]
        public int RoleID { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
