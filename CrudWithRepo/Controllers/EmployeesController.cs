using CrudWithRepo.Models;
using CrudWithRepo.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrudWithRepo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployee _context;

        public EmployeesController(IEmployee context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.GetAllEmployees();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            _context.AddEmployee(employee);
            TempData["Msg"] = "Record Saved";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            var data = _context.GetEmployeeById(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.EditEmployee(employee);
                TempData["Msg"] = "Record Updated";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Data");
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            _context.DeleteEmployeeById(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
