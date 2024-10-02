using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC1.viewModels;
using MVC1__DAL_.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC1.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<ApplicationUser> userManager ,RoleManager<IdentityRole> roleManager ,IMapper mapper  )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                var users =await _userManager.Users.Select( u => new userViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    FName = u.FirstName,
                    LName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    Roles =_userManager.GetRolesAsync(u).Result

                }).ToListAsync();

                return View(users);
            }
            else
            {
                var users =await _userManager.FindByEmailAsync(email);
                if (users is not null)
                {
                    var mappeduser = new userViewModel
                    {
                        Id = users.Id,
                        Email = users.Email,
                        FName = users.FirstName,
                        LName = users.LastName,
                        PhoneNumber = users.PhoneNumber,
                        Roles = _userManager.GetRolesAsync(users).Result
                    };
                    return View(new List<userViewModel> {mappeduser } );
                }
            }
            return View(Enumerable.Empty<userViewModel>());
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var user =await _userManager.FindByIdAsync(id);
            if (user is not null)
            {
                var mappedUser = _mapper.Map<ApplicationUser , userViewModel>(user);
                return View(mappedUser);
            }
            else
            {
                return NotFound();
            }

        }
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var user =await _userManager?.FindByIdAsync(id);
            if (user is not null)
            {
                var mappedUser = _mapper.Map<ApplicationUser, userViewModel>(user);
                return View(mappedUser);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id,userViewModel viewModel)
        {
            if (id !=viewModel.Id)
            {
                return BadRequest();  
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                user.PhoneNumber = viewModel.PhoneNumber;
                user.FirstName = viewModel.FName;
                user.LastName = viewModel.LName;
                var result =  await _userManager.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(viewModel);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user is not null)
            {
                var mappedUser = _mapper.Map<ApplicationUser, userViewModel>(user);
                return View(mappedUser);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(userViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(viewModel.Id);
                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));

            }
            return BadRequest();
        }
    }
}
