using HireUp.DAL.Models;

namespace HireUp.PL.ViewModels
{
    public class ApplyViewModel
    {
        public Job Job { get; set; }
        public JobSeeker JobSeeker { get; set; }
        public  Company Company { get; set; }
    }
}
