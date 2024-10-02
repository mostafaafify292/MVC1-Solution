using MVC1__DAL_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC__BLL_.Interfacies
{
    public interface IEmployeeRepository :IGenericRepository<Employee>
    {
     public IQueryable<Employee> GetEmployeeByName(string name);
    }
}
