using Microsoft.EntityFrameworkCore;
using MVC__BLL_.Interfacies;
using MVC1__DAL_.Data;
using MVC1__DAL_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC__BLL_.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private readonly AppDbContext _DbContext;
        public GenericRepository(AppDbContext DbContext)
        {
            _DbContext = DbContext;
        }


        public void Add(T item)
        {
            _DbContext.Set<T>().Add(item);
        }

        public void Delete(T item)
        {
            _DbContext.Set<T>().Remove(item);
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)_DbContext.employees.Include(d => d.department).AsNoTracking().ToList();
            }
            return _DbContext.Set<T>().AsNoTracking().ToList();
        }

        public T GetById(int id)
        {
            if (typeof(T) == typeof(Employee))
            {             
                return _DbContext.employees.Include(e => e.department).FirstOrDefault(e=>e.id==id) as T;
            }
            return _DbContext.Set<T>().Find(id);
        }

        public void Update(T item)
        {
            _DbContext.Set<T>().Update(item);
        }
    }
}
