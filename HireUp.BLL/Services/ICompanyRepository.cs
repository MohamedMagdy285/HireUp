
using HireUp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireUp.BLL.Services
{
    public interface ICompanyRepository : IGenericRepository<Company, string>
    {
        List<Company> GetByCategory(int categoryId);
    }
}
