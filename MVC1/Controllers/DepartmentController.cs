using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC__BLL_.Interfacies;
using MVC1__DAL_.Models;
using System;

namespace MVC1.Controllers 
{

    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentRepository repository, IWebHostEnvironment env)
        {
            _departmentRepository = repository;    
            _env = env;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                var count = _departmentRepository.Add(department);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }
            var department = _departmentRepository.GetById(id.Value);
            if (department == null)
                return NotFound();
            else
                return View(department);

        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }
            var department = _departmentRepository.GetById(id.Value);
            if (department == null)
                return NotFound();
            else
                return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (id != department.id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(department);
            }
            try
            {
                _departmentRepository.Update(department);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //log Exception
                //Freindly massage
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error Occured During Update Department");
                }
            }
            return View(department);

        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }
            var department = _departmentRepository.GetById(id.Value);
            if (department == null)
                return NotFound();
            else
                return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department department)
        {
            try
            {
                _departmentRepository.Delete(department);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error Occured During Deleting Department");
                }

            }
            return View(department);
        }
    }

}
