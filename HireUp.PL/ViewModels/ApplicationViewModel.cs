using HireUp.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireUp.PL.ViewModels
{
    public class ApplicationViewModel
    {
        public int Id { get; set; }

        [ForeignKey("JobSeeker")]
        public string? JobSeekerId { get; set; }
        public JobSeeker? JobSeeker { get; set; }
        public JobSeekerProfileViewModel? JobSeekerViewModel { get; set; }

        [ForeignKey("Job")]
        public int? JobId { get; set; }
        public Job? Job { get; set; }

        [ForeignKey("Company")]
        public string? CompanyId { get; set; }
        public Company? Company { get; set; }

        public string Status { get; set; } = "Pending";
        public DateTime AppliedAt { get; set; } = DateTime.Now;
        //public string FullName => $"{JobSeeker.FirstName} {JobSeeker.LastName}";



    }
}
