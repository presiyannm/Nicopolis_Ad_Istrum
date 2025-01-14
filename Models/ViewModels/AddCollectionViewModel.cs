using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Nicopolis_Ad_Istrum.Models.ViewModels
{
    public class AddCollectionViewModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int EraId {  get; set; }

        [Required]
        public int LocationId { get; set; }
        public ICollection<SelectListItem> Eras { get; set; } = new List<SelectListItem>();
        public ICollection<SelectListItem> Locations { get; set; } = new List<SelectListItem>();

    }
}
