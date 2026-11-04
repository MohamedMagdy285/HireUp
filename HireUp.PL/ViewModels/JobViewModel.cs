using HireUp.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireUp.PL.ViewModels
{
    public class JobViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        [DataType(DataType.Currency)]
        public decimal SalaryMin { get; set; }
        [DataType(DataType.Currency)]
        public decimal SalaryMax { get; set; }
        public string Type { get; set; }
        public string ExperinceLevel { get; set; }
        public List<string>? Skills { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("Company")]
        public string? CompanyId { get; set; }
        public Company? Company { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Application>? Applications { get; set; } = new List<Application>();
    }
}
