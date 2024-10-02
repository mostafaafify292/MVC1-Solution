using MVC__BLL_.Interfacies;
using MVC1__DAL_.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC__BLL_.Repositories
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            EmployeeRepository = new EmployeeRepository(_dbContext);
            DepartmentRepository = new DepartmentRepository(_dbContext);
            
        }
        public IEmployeeRepository EmployeeRepository { get ; set ; }
        public IDepartmentRepository DepartmentRepository { get ; set ; }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
         _dbContext.Dispose();
        }
    }
}
