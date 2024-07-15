using System.ComponentModel.DataAnnotations;

namespace CRMSystem.Authentication
{
    public class RegisterModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Недопустимое имя")]
        public string Login { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Некорректный пароль")]
        public string Password { get; set; } = string.Empty;

        [EmailAddress]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Некорректный email")]
        public string Email { get; set; } = string.Empty;
    }
}
