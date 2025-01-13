namespace Nicopolis_Ad_Istrum.Models.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string City { get; set; }  = string.Empty;
        public string Address { get; set; } = string.Empty;
        public List<string> SelectedRoles { get; set; } = new List<string>();
        public List<string> AllRoles { get; set; } = new List<string>();
    }
}
