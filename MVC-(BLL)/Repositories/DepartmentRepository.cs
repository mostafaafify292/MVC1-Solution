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
    public class DepartmentRepository : GenericRepository<Department> ,IDepartmentRepository
    {
        private readonly AppDbContext _DbContext;
        public DepartmentRepository(AppDbContext DbContext):base(DbContext)
        {
            _DbContext = DbContext;
        }


    }
}
