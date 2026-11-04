using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireUp.DAL.Models
{
    public class Application
    {
        public int Id { get; set; }

        [ForeignKey("JobSeeker")]
        public string? JobSeekerId { get; set; } 
        public JobSeeker JobSeeker { get; set; }

        [ForeignKey("Job")]
        public int? JobId { get; set; }
        public Job Job { get; set; }

        public string Status { get; set; } = "Pending";
        public DateTime AppliedAt { get; set; } = DateTime.Now;
    }
}
