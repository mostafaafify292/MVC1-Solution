using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC1.Helper;
using MVC1.viewModels;
using MVC1__DAL_.Models;
using System.Threading.Tasks;

namespace MVC1.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager )
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}
            
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel ViewModel)
        {
            if (ModelState.IsValid) //ServerSideValidation
            {
                //manual Mapping
                var mappedUser = new ApplicationUser()
                {
                    UserName = ViewModel.Email.Split("@")[0],
                    Email = ViewModel.Email,
                    IsAgree = ViewModel.IsAgree,
                    FirstName = ViewModel.FirstName,
                    LastName = ViewModel.LastName
                };
                var Result =await _userManager.CreateAsync(mappedUser,ViewModel.Password);
                if (Result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }


            }
            return View(ViewModel);
            

        }
		
        public IActionResult SignIn()
        {
              return View(); 
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> SignIn(SignInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(viewModel.Email);
                if (user is not null)
                {
                    bool flag = await _userManager.CheckPasswordAsync(user, viewModel.Password);
                    if (flag)
                    {
                       var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe , true);
                        if (result.Succeeded)
                        { 
                         return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "InValid Login");

			}
            return View(viewModel);
        }
        
		    
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user =await _userManager.FindByEmailAsync(viewModel.Email);
                if (user is not null)
                {
                    var token =await _userManager.GeneratePasswordResetTokenAsync(user);
                    var ResetPasswordUrl = Url.Action("ResetPassword", "Account", new { email = viewModel.Email ,token = token});
                    var email = new Email()
                    {
                        Subject = "Reset Your Password",
                        Reciepints = viewModel.Email,
                        Body = $"To reset your password, please click the link below: " +
                        $"https://localhost:44332{ResetPasswordUrl}"
					};
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Invalid Email");

            }
            return View(viewModel);

        }
        public IActionResult CheckYourInbox()
        {
            return View(); 
        }
        public IActionResult ResetPassword(string email , string token)
        {
            TempData["Email"]=email;
            TempData["Token"]=token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string email = TempData["Email"] as string;
                string token = TempData["Token"] as string;
                var user = await _userManager.FindByEmailAsync(email);
                var result =await _userManager.ResetPasswordAsync(user, token , viewModel.Password);
                if (result.Succeeded)
                { 
                 return RedirectToAction(nameof(SignIn));
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }

            }
            return View(viewModel);
        }

    }
}
