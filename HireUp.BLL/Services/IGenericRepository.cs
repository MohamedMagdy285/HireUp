using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireUp.BLL.Services
{
    public interface IGenericRepository<T,D>
    {
        List<T> GetAll();
        T GetById(D id);
        void Add(T item);
        void Update(T item);
        void Delete(T item);

        void Save();
    }
}
