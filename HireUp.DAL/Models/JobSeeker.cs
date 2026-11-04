using HireUp.DAL.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace HireUp.DAL.Models
{
    public class JobSeeker : ApplicationUser
    {
        [RegularExpression(@"^\d{14}$", ErrorMessage = "National ID must be 14 digits")]
        public string NationalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Bio { get; set; }
        //public string? SkillsJson { get; set; }

        //[NotMapped]
        //public List<string>? Skills
        //{
        //    get => string.IsNullOrEmpty(SkillsJson)
        //        ? new List<string>()
        //        : System.Text.Json.JsonSerializer.Deserialize<List<string>>(SkillsJson);
        //    set => SkillsJson = System.Text.Json.JsonSerializer.Serialize(value);
        //}
        public decimal? ExperinceYears { get; set; }
        public string? Education { get; set; }
        public string? CVPath { get; set; }
        public string? ProfileImageUrl { get; set; }

        public List<Application>? Applications { get; set; } = new List<Application>();

    }
}
