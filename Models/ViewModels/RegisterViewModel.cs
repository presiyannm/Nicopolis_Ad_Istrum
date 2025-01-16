using System.ComponentModel.DataAnnotations;

namespace Nicopolis_Ad_Istrum.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;
    }
}
