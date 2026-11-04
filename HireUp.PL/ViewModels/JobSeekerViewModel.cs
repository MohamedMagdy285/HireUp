using HireUp.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireUp.PL.ViewModels
{
    public class JobSeekerViewModel
    {
        public int Id { get; set; }
        [RegularExpression(@"^\d{14}$", ErrorMessage = "National ID must be 14 digits")]
        public string NationalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Bio { get; set; }
        //public List<string>? Skills { get; set; }
        public decimal? ExperinceYears { get; set; }
        public string? Education { get; set; }
        public string? CVPath { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public List<Application>? Applications { get; set; } = new List<Application>();
    }
}
