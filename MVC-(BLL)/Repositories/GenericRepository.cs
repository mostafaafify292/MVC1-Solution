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


        public int Add(T item)
        {
            _DbContext.Set<T>().Add(item);
            return _DbContext.SaveChanges();
        }

        public int Delete(T item)
        {
            _DbContext.Set<T>().Remove(item);
            return _DbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _DbContext.Set<T>().AsNoTracking().ToList();
        }

        public T GetById(int id)
        {
            return _DbContext.Set<T>().Find(id);
        }

        public int Update(T item)
        {
            _DbContext.Set<T>().Update(item);
            return _DbContext.SaveChanges();
        }
    }
}
