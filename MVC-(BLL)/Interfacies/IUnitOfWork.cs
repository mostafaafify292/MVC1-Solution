using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC__BLL_.Interfacies
{
    public interface IUnitOfWork : IDisposable
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        public int Complete();
        
    }
}
