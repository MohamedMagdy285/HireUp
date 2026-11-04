using HireUp.DAL.Models;

namespace HireUp.PL.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Job>? Jobs { get; set; } = new List<Job>();
    }
}
