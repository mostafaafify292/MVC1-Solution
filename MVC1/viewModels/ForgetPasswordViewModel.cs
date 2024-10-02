using System.ComponentModel.DataAnnotations;

namespace MVC1.viewModels
{
    public class ForgetPasswordViewModel
    {
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
	}
}
