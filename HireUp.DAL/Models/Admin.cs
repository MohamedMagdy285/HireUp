using HireUp.DAL.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireUp.DAL.Models
{
    public class Admin : ApplicationUser
    {
        public List<JobSeeker>? JobSeekers { get; set; }
        public List<Company>? Companies { get; set; }
    }
}
