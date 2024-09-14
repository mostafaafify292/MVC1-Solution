using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}
