using System.ComponentModel.DataAnnotations;

namespace HireUp.PL.ViewModels
{
    public class JobSeekerProfileViewModel
    {
        public string Id { get; set; }        
        public string NationalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Bio { get; set; }
        //public List<string>? Skills { get; set; }
        public decimal? ExperinceYears { get; set; }
        public string? Education { get; set; }
        public IFormFile? CVPath { get; set; }
        public string? CVPathForDisplay { get; set; }
        public IFormFile? ProfileImageUrl { get; set; }
        public string? ProfileImageUrlForDisplay { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int? ApplicationId { get; set; }
    }
}
