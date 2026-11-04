using HireUp.BLL.Services;
using HireUp.DAL.Contexts;
using HireUp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireUp.BLL.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly HireUpDbContext _hireUpDbContext;

        public JobRepository(HireUpDbContext hireUpDbContext)
        {
            _hireUpDbContext = hireUpDbContext;
        }

        #region CRUD
        public void Add(Job item)
        {
            _hireUpDbContext.Job.Add(item);
        }
        public void Delete(Job item)
        {
            _hireUpDbContext.Job.Remove(item);
        }
        public void Update(Job item)
        {
            _hireUpDbContext.Job.Update(item);
        }
        public List<Job> GetAll()
        {
            return _hireUpDbContext.Job.Include(c => c.Category).Include(c => c.Company).ToList();
        }
        public Job GetById(int id)
        {
            return _hireUpDbContext.Job.Include(c => c.Category).Include(c => c.Company).FirstOrDefault(j => j.Id == id);
        }
        
        public List<Job> GetByCategory(int categoryId)
        {
            return _hireUpDbContext.Job.Include(j => j.Category).Include(j => j.Company).Where(j => j.CategoryId == categoryId).ToList();
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
