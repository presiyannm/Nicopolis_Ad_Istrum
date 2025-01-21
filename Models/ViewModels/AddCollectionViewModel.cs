using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Nicopolis_Ad_Istrum.Models.ViewModels
{
    public class AddCollectionViewModel
    {
        public int Id {  get; set; }

        [Required]
        public string AssociateId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително.")]
        public int EraId {  get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        public int LocationId { get; set; }
        public ICollection<SelectListItem> Eras { get; set; } = new List<SelectListItem>();
        public ICollection<SelectListItem> Locations { get; set; } = new List<SelectListItem>();

    }
}
