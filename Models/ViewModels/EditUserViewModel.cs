using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Nicopolis_Ad_Istrum.Models.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Degree { get; set; } = string.Empty;

        [Required]
        public string Specialty { get; set; } = string.Empty;

        [Required]
        public string City { get; set; }  = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        public List<string> SelectedRoles { get; set; } = new List<string>();
        public List<string> AllRoles { get; set; } = new List<string>();
    }
}
