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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly HireUpDbContext _hireUpDbContext;

        public CategoryRepository(HireUpDbContext hireUpDbContext)
        {
            _hireUpDbContext = hireUpDbContext;
        }
        #region CRUD

        public void Add(Category item)
        {
            _hireUpDbContext.Category.Add(item);
        }
        public void Delete(Category item)
        {
            _hireUpDbContext.Category.Remove(item);
        }
        public void Update(Category item)
        {
            _hireUpDbContext.Category.Update(item);
        }
        public List<Category> GetAll()
        {
            return _hireUpDbContext.Category.Include(j=>j.Jobs).ToList();
        }
        public Category GetById(int id)
        {
            return _hireUpDbContext.Category.Include(j => j.Jobs).FirstOrDefault(i=>i.Id==id);
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
