using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC__BLL_.Interfacies;
using MVC__BLL_.Repositories;
using MVC1__DAL_.Models;
using System;


namespace MVC1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository repository , IWebHostEnvironment env)
        {
            _EmployeeRepository = repository;
            _env = env;
        }

        public IActionResult Index()
        {
            var Employees = _EmployeeRepository.GetAll();
            return View(Employees);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var count = _EmployeeRepository.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "Employee Created Succesfully";
                    
                }
                else 
                {
                    TempData["Message"] = "An Error Occured";
                }
                return RedirectToAction("Index");

            }
            return View(employee);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }
            var Employee = _EmployeeRepository.GetById(id.Value);
            if (Employee == null)
                return NotFound();
            else
                return View(Employee);

        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }
            var Employee = _EmployeeRepository.GetById(id.Value);
            if (Employee == null)
                return NotFound();
            else
                return View(Employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee Employee)
        {
            if (id != Employee.id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(Employee);
            }
            try
            {
                _EmployeeRepository.Update(Employee);
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
                    ModelState.AddModelError(string.Empty, "An Error Occured During Update Employee");
                }
            }
            return View(Employee);

        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }
            var Employee = _EmployeeRepository.GetById(id.Value);
            if (Employee == null)
                return NotFound();
            else
                return View(Employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee Employee)
        {
            try
            {
                _EmployeeRepository.Delete(Employee);
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
                    ModelState.AddModelError(string.Empty, "An Error Occured During Deleting Employee");
                }

            }
            return View(Employee);
        }
    }
}
