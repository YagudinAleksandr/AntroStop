using System.ComponentModel.DataAnnotations;

namespace AntroStop.Domain.Base.AuthModels
{
    public class UserForRegistrationDto
    {
        [Required(ErrorMessage = "Поле E-mail должно быть заполнено")]
        [EmailAddress(ErrorMessage = "Не валидный E-mail")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле пароль должно быть заполнено")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле Фамилия Имя должно быть заполнено")]
        public string Name { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Вы должны принять условия АИС AntroStop")]
        public bool IsAgree { get; set; }
    }
}
