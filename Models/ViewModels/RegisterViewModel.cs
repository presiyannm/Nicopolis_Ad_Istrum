using System.ComponentModel.DataAnnotations;

namespace Nicopolis_Ad_Istrum.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Паролата е задължителна.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Потвърдете паролата.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Първото име е задължително.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Фамилията е задължителна.")]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Моля, въведете валиден имейл.")]
        public string EmailAddress { get; set; } = string.Empty;
    }
}
