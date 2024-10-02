using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MVC1__DAL_.Models
{
    public enum Gender
    {
        [EnumMember(Value = "Male")]
        Male = 0,
        [EnumMember(Value = "Female")]
        Female = 1
    }
    enum EmployeeType
    {
        FullTime = 1,
        PartTime = 2
    }
    public class Employee :ModelBase
    {
       
     public string Name { get; set; }
     
        public int? Age { get; set; }
        
        public string Address { get; set; }
        
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
       
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public DateTime HireDate { get; set; }
        public bool IsDeleted { get; set; } //soft Delete
        public Gender Gender { get; set; }
        //[InverseProperty(nameof(Models.Department.Employees))]
        //[Display(Name ="Department")]
        public Department department { get; set; }
        public int? DepartmentId { get; set; }
        public string ImageName { get; set; }
    }
}
