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
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager , IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedrole = _mapper.Map< RoleViewModel , IdentityRole>(roleViewModel);
                await _roleManager.CreateAsync(mappedrole);

                    return RedirectToAction("Index");
                
            }
            return View(roleViewModel);
        }
        public async Task<IActionResult> Index(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                var roles = await _roleManager.Roles.Select(u => new RoleViewModel
                {
                    id = u.Id,
                   RoleName = u.Name

                }).ToListAsync();

                return View(roles);
            }
            else
            {
                var role = await _roleManager.FindByNameAsync(Name);
                if (role is not null)
                {
                    var mappedRole = new RoleViewModel
                    {
                        id = role.Id,
                       RoleName = role.Name
                    };
                    return View(new List<RoleViewModel> { mappedRole });
                }
            }
            return View(Enumerable.Empty<RoleViewModel>());
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role is not null)
            {
                var mappedrole = _mapper.Map<IdentityRole, RoleViewModel>(role);
                return View(mappedrole);
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
            var role = await _roleManager?.FindByIdAsync(id);
            if (role is not null)
            {
                var mappedrole = _mapper.Map<IdentityRole, RoleViewModel>(role);
                return View(mappedrole);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel viewModel)
        {
            if (id != viewModel.id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(id);
                viewModel.RoleName = role.Name;
                var result = await _roleManager.UpdateAsync(role);
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
            var role = await _roleManager.FindByIdAsync(id);
            if (role is not null)
            {
                var mappedrole = _mapper.Map<IdentityRole, RoleViewModel>(role);
                return View(mappedrole);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(RoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(viewModel.id);
                await _roleManager.DeleteAsync(role);
                return RedirectToAction(nameof(Index));

            }
            return BadRequest();
        }


    }
}
