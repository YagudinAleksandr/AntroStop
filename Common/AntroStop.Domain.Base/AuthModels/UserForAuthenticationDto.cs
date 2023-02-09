using System.ComponentModel.DataAnnotations;

namespace AntroStop.Domain.Base.AuthModels
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "E-mail должен быть заполнен")]
        [EmailAddress(ErrorMessage ="Не валидный адрес E-mail")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Поле пароль должно быть заполнено")]
        public string Password { get; set; }
    }
}
