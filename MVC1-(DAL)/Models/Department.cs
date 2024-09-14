using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC1__DAL_.Models
{
    public class Department :ModelBase
    {
       
        [Required(ErrorMessage = "Code Is Required!")]
        public string Code { get; set; } //.Net 5 (Allow Null)
        [Required(ErrorMessage = "Name Is Required!")]
        public string Name { get; set; }
        [Display(Name = "Date Of Creation")]
        public DateTime DataOfCreation { get; set; }
    }
}
