using System.ComponentModel.DataAnnotations;

namespace MVC1.viewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [Compare(nameof(Password), ErrorMessage = "Confirm Password Dosn't Match Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
    }
}
