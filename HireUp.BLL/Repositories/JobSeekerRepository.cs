using HireUp.BLL.Services;
using HireUp.DAL.Contexts;
using HireUp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireUp.BLL.Repositories
{
    public class JobSeekerRepository : IJobSeekerRepository
    {
        private readonly HireUpDbContext _hireUpDbContext;

        public JobSeekerRepository(HireUpDbContext hireUpDbContext)
        {
            _hireUpDbContext = hireUpDbContext;
        }

        #region CRUD

        public void Add(JobSeeker item)
        {
            _hireUpDbContext.JobSeeker.Add(item);
        }
        public void Delete(JobSeeker item)
        {
            _hireUpDbContext.JobSeeker.Remove(item);
        }
        public void Update(JobSeeker item)
        {
            _hireUpDbContext.JobSeeker.Update(item);
        }
        public List<JobSeeker> GetAll()
        {
            return _hireUpDbContext.JobSeeker.ToList();
        }
        public JobSeeker GetById(string id)
        {
            return _hireUpDbContext.JobSeeker.Find(id);
        }
        #endregion

        #region Connections
        public void Save()
        {
            _hireUpDbContext.SaveChanges();
        }
        #endregion

    }
}
