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
    public class EmployeeRepository: GenericRepository<Employee> ,IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
