using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    
    public class HomeController : Controller
    {
        private IRepository<Employee> _employeeRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(IRepository<Employee> employeeRepository,IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
                
        [Route("~/")]
        public ViewResult Index() {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();

            employeeViewModel.employee = _employeeRepository.GetEmployees();
            employeeViewModel.PageTitle = "Employee Details";


            return View(employeeViewModel);
        }

        public ViewResult Details(int? id)
        {
            //throw new Exception("Error in Details view");
            List<Employee> employee= _employeeRepository.GetEmployees(id.Value);

            if (!employee.Any())
            {
                Response.StatusCode = 404;
                return View("NotFound","Employee with ID = "+ id.Value+" not found");
            }

            EmployeeViewModel employeeViewModel = new EmployeeViewModel();

            employeeViewModel.employee = employee;
            employeeViewModel.PageTitle = "Employee Details";
            

            return View(employeeViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();

        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                id = employee.Id,
                EmployeeName = employee.EmployeeName,
                Email = employee.Email,
                Address = employee.Address,
                existingPhotoPath = employee.PhotoPath

            };
            return View(employeeEditViewModel);

        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel employee)
        {
            
            if (ModelState.IsValid)
            {

                Employee emp = _employeeRepository.GetEmployee(employee.id);
                emp.EmployeeName = employee.EmployeeName;
                emp.Email = employee.Email;
                emp.Address = employee.Address;
                if (employee.Photo != null) {
                    if (employee.existingPhotoPath!=null) {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "Img", employee.existingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    

                    emp.PhotoPath = UploadPhoto(employee);
                }               

                Employee updatedEmployee = _employeeRepository.Update(emp);
                return RedirectToAction("details", new { id = updatedEmployee.Id });
            }

            return View();

        }

       
        public IActionResult Delete(int id)
        {

            

            

                Employee emp = _employeeRepository.GetEmployee(id);
                
                if (emp != null)
                {
                    if (emp.PhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "Img", emp.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }


                    _employeeRepository.Delete(id);
                }
                
                return RedirectToAction("index");
           

            

        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel employee)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadPhoto(employee);

                Employee emp = new Employee
                {
                    EmployeeName = employee.EmployeeName,
                    Email = employee.Email,
                    Address = employee.Address,
                    PhotoPath = uniqueFileName
                };

                Employee newEmployee = _employeeRepository.Add(emp);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();

        }

        private string UploadPhoto(EmployeeCreateViewModel employee)
        {
            string uniqueFileName = null;
            if (employee.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + employee.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create)) {
                    employee.Photo.CopyTo(fileStream);
                }
                    
            }

            return uniqueFileName;
        }
    }
}
