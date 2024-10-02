using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC__BLL_.Interfacies;
using MVC__BLL_.Repositories;
using MVC1.Helper;
using MVC1.viewModels;
using MVC1__DAL_.Models;
using System;
using System.Collections.Generic;


namespace MVC1.Controllers
{
   
    public class EmployeeController : Controller
    {
       // private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork , IWebHostEnvironment env ,IMapper mapper )
        {
            //_EmployeeRepository = repository;
            _unitOfWork = unitOfWork;
            _env = env;
            _mapper = mapper;
        }

        public IActionResult Index(string searchInp)
        {
            if (string.IsNullOrEmpty(searchInp))
            {

                var Employees = _unitOfWork.EmployeeRepository.GetAll();
                var mappedEmp = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>> (Employees);
                return View(mappedEmp);
            }
            else 
            {
                var Employees = _unitOfWork.EmployeeRepository.GetEmployeeByName(searchInp);
                var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
                return View(mappedEmp);
            }  
        }
        public IActionResult Create()
        {
            ViewData["departments"] = _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVm)
        {
            if (ModelState.IsValid)
            {
                employeeVm.ImageName =DocumentSettings.UploadFile(employeeVm.Image, "Images");
                var mappedEmp = _mapper.Map<EmployeeViewModel,Employee>(employeeVm);
                 _unitOfWork.EmployeeRepository.Add(mappedEmp);
                var count = _unitOfWork.Complete();
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
            return View(employeeVm);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }
            var Employee = _unitOfWork.EmployeeRepository.GetById(id.Value);
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(Employee);



            if (Employee == null)
                return NotFound();
            else
                return View(mappedEmp);

        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }
            ViewData["departments"] = _unitOfWork.DepartmentRepository.GetAll();
            var Employee = _unitOfWork.EmployeeRepository.GetById(id.Value);
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(Employee);
            //ViewData["departments"] = _departmentRepository.GetAll();
            if (Employee == null)
                return NotFound();
            else
                return View(mappedEmp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVm)
        {
            if (id != employeeVm.id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(employeeVm);
            }
            try
            {
                employeeVm.ImageName = DocumentSettings.UploadFile(employeeVm.Image, "Images");
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                _unitOfWork.EmployeeRepository.Update(mappedEmp);
                 _unitOfWork.Complete();
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
            return View(employeeVm);

        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }
            var Employee = _unitOfWork.EmployeeRepository.GetById(id.Value);
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(Employee);
            if (Employee == null)
                return NotFound();
            else
                return View(mappedEmp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(EmployeeViewModel employeeVm)
        {
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                _unitOfWork.Complete();
                DocumentSettings.DeleteFile(employeeVm.ImageName, "Images");
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
            return View(employeeVm);
        }
    }
}
