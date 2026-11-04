using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireUp.DAL.Models
{
    public class Job 
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
        public string? SkillsJson { get; set; }

        [NotMapped]
        public List<string>? Skills
        {
            get => string.IsNullOrEmpty(SkillsJson)
                ? new List<string>()
                : System.Text.Json.JsonSerializer.Deserialize<List<string>>(SkillsJson);
            set => SkillsJson = System.Text.Json.JsonSerializer.Serialize(value);
        }
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
