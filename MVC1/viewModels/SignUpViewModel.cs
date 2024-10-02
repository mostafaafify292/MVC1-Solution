using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MVC1.viewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="First Name Is Required")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last Name Is Required")]
		public string LastName { get; set; }
		[Required(ErrorMessage ="Email Is Required")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage ="Confirm Password Is Required")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password),ErrorMessage ="Confirm Password Dosn't Match Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="Reqired To Agree")]
        public bool IsAgree { get; set; }
    }
}
