using CrudWithRepo.Data;
using CrudWithRepo.Models;
using CrudWithRepo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrudWithRepo.Repository.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly ApplicationContext context;
        private readonly IWebHostEnvironment env;

        public EmployeeService(ApplicationContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }
        public List<Employee> GetAllEmployees()
        {
            var data = context.Employees.ToList();
            return data;
        }
        public Employee AddEmployee(Employee employee)
        {
            string uniqueFileName = UploadImage(employee);
            Employee data = new Employee()
            {
                Name = employee.Name,
                Gender = employee.Gender,
                Age = employee.Age,
                IsActive = employee.IsActive,
                Photo = uniqueFileName
            };
            context.Employees.Add(data);
            context.SaveChanges();
            return data;
        }
        public Employee GetEmployeeById(int id)
        {
            var data = context.Employees.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }
        public Employee DeleteEmployeeById(int id)
        {
            var data = context.Employees.Where(x => x.Id == id).FirstOrDefault();
            context.Employees.Remove(data);
            context.SaveChanges();
            return data;
        }
        public Employee EditEmployee(Employee employee)
        {
            var data = context.Employees.Where(x=>x.Id == employee.Id).FirstOrDefault();
            if (data != null)
            {
                string uniqueFileName = UploadImage(employee);
                data.Name = employee.Name;
                data.Gender = employee.Gender;
                data.Age = employee.Age;
                data.IsActive = employee.IsActive;
                data.Photo = uniqueFileName;
                context.Update(data);
                context.SaveChanges();
            }
            return data;
        }

        public string UploadImage(Employee employee)
        {
            string uniqueFileName = string.Empty;
            string extension = Path.GetExtension(employee.ImgPath.FileName).ToLower();
            string[] isExtension = { ".jpeg", ".png", ".jpg" };

            if (employee.ImgPath != null)
            {
                if (isExtension.Contains(extension)) 
                { 
                    if(employee.ImgPath.Length <= 1000000) 
                    { 
                        string fileFolder = Path.Combine(env.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + employee.ImgPath.FileName;
                        string filePath = Path.Combine(fileFolder, uniqueFileName);
                        using (var fileStrem = new FileStream(filePath,FileMode.Create))
                        {
                            employee.ImgPath.CopyTo(fileStrem);
                        }
                    }
                }
            }
            return uniqueFileName;
        }
    }
}
