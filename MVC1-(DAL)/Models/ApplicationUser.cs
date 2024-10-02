using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC1__DAL_.Models
{
	public class ApplicationUser :IdentityUser
	{
        public bool IsAgree { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
