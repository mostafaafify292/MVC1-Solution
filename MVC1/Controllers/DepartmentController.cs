using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC__BLL_.Interfacies;
using MVC1.viewModels;
using MVC1__DAL_.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MVC1.Controllers 
{
    
    public class DepartmentController : Controller
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IWebHostEnvironment env ,IMapper mapper)
        {
           
            _unitOfWork = unitOfWork;
            _env = env;
            _mapper = mapper;
        }
        public IActionResult Index()
        {

            var departments = _unitOfWork.DepartmentRepository.GetAll();
            var mappeddept = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(mappeddept);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVm)
        {
            if (ModelState.IsValid)
            {
                var mappeddept = _mapper.Map<DepartmentViewModel,Department>(departmentVm);
                _unitOfWork.DepartmentRepository.Add(mappeddept);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(departmentVm);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }
            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);
            var mappeddept = _mapper.Map<Department, DepartmentViewModel>(department);
            if (department == null)
                return NotFound();
            else
                return View(mappeddept);

        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }
            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);
            var mappeddept = _mapper.Map<Department, DepartmentViewModel>(department);
            if (department == null)
                return NotFound();
            else
                return View(mappeddept);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVm)
        {
            if (id != departmentVm.id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {

                return View(departmentVm);
            }
            try
            {
                var mappeddept = _mapper.Map<DepartmentViewModel, Department>(departmentVm);
                _unitOfWork.DepartmentRepository.Update(mappeddept);
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
                    ModelState.AddModelError(string.Empty, "An Error Occured During Update Department");
                }
            }
            return View(departmentVm);

        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }
            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);
            var mappeddept = _mapper.Map<Department, DepartmentViewModel>(department);
            if (department == null)
                return NotFound();
            else
                return View(mappeddept);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DepartmentViewModel departmentVm)
        {
            try
            {
                var mappeddept = _mapper.Map<DepartmentViewModel, Department>(departmentVm);
                _unitOfWork.DepartmentRepository.Delete(mappeddept);
                 _unitOfWork.Complete();
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
            return View(departmentVm);
        }
    }

}
