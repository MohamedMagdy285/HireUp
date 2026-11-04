using HireUp.BLL.Services;
using HireUp.DAL.Contexts;
using HireUp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireUp.BLL.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {

        private readonly HireUpDbContext _hireUpDbContext;

        public ApplicationRepository(HireUpDbContext hireUpDbContext)
        {
            _hireUpDbContext = hireUpDbContext;
        }

        #region CRUD

        public void Add(Application item)
        {
            _hireUpDbContext.Application.Add(item);
        }
        public void Delete(Application item)
        {
            _hireUpDbContext.Application.Remove(item);
        }
        public void Update(Application item)
        {
            _hireUpDbContext.Application.Update(item);
        }
        public List<Application> GetAll()
        {
            return _hireUpDbContext.Application.Include(a => a.Job).ThenInclude(j => j.Company).Include(a => a.JobSeeker).ToList();
        }
      

        public Application GetById(int id)
        {
            return _hireUpDbContext.Application.Include(a => a.Job).ThenInclude(j => j.Company).Include(a => a.JobSeeker).FirstOrDefault(a => a.Id == id);
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
