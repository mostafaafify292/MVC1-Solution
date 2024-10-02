using MVC1__DAL_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC__BLL_.Interfacies
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}
