using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Nicopolis_Ad_Istrum.Models.ViewModels
{
    public class AddEventViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително.")]
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } = string.Empty;

    }
}
