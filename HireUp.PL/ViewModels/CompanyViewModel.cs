using HireUp.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireUp.PL.ViewModels
{
    public class CompanyViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
        public IFormFile? LogoURL { get; set; }
        public string? Description { get; set; }
        public string? WebsiteURL { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<Job>? Jobs { get; set; } = new List<Job>();
    }
}
