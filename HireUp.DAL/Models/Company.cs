using HireUp.DAL.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireUp.DAL.Models
{
    public class Company : ApplicationUser
    {
        public string Name { get; set; }
        public string Industry { get; set; }
        public string? LogoURL { get; set; }
        public string? Description { get; set; }
        public string? WebsiteURL { get; set; }
        public List<Job>? Jobs { get; set; } = new List<Job>();
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
