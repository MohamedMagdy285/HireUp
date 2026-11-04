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
    public class CompanyRepository : ICompanyRepository
    {

        private readonly HireUpDbContext _hireUpDbContext;

        public CompanyRepository(HireUpDbContext hireUpDbContext)
        {
            _hireUpDbContext = hireUpDbContext;
        }

        #region CRUD

        public void Add(Company item)
        {
            _hireUpDbContext.Company.Add(item);
        }
        public void Delete(Company item)
        {
            _hireUpDbContext.Company.Remove(item);
        }
        public void Update(Company item)
        {
            _hireUpDbContext.Company.Update(item);
        }
        public List<Company> GetAll()
        {
            return _hireUpDbContext.Company.Include(j => j.Jobs).ToList();
        }
        public Company GetById(string id)
        {
            return _hireUpDbContext.Company.Include(j => j.Jobs).FirstOrDefault(c => c.Id == id);
        }
        public List<Company> GetByCategory(int categoryId)
        {
            return _hireUpDbContext
                .Company.Include(j => j.Category)               
                .Where(j => j.CategoryId == categoryId)
                .ToList();
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
