using System;
using System.ComponentModel.DataAnnotations;

namespace MVC1.viewModels
{
    public class RoleViewModel
    {
        public string id { get; set; }
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
        public RoleViewModel()
        {
            id = Guid.NewGuid().ToString();
        }
    }
}
