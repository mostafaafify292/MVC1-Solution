using MVC1__DAL_.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;

namespace MVC1.viewModels
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
    public class EmployeeViewModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50, ErrorMessage = "Max Lenght For Name Is 50")]
        [MinLength(4, ErrorMessage = "Min Lenght For Name Is 4")]
        public string Name { get; set; }
        [Range(21, 60)]
        public int? Age { get; set; }
        //[RegularExpression(@"^\[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]\)$"
        //        , ErrorMessage = "Address Must Be Like 123-street-city-country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        public bool IsDeleted { get; set; } //soft Delete
        public Gender Gender { get; set; }
        //[InverseProperty(nameof(Models.Department.Employees))]
        //[Display(Name = "Department")]
        public Department department { get; set; }
        public int? DepartmentId { get; set; }
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }

    }
}
