using MVC1__DAL_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC1.viewModels
{
    public class DepartmentViewModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Code Is Required!")]
        public string Code { get; set; } //.Net 5 (Allow Null)
        [Required(ErrorMessage = "Name Is Required!")]

        public string Name { get; set; }
        [Display(Name = "Date Of Creation")]
        public DateTime DataOfCreation { get; set; }
        //[InverseProperty(nameof(MVC1__DAL_.Models.Employee.department))]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
