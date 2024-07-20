using CrudWithRepo.Models;

namespace CrudWithRepo.Repository.Interfaces
{
    public interface IEmployee
    {
        List<Employee> GetAllEmployees();
        Employee AddEmployee(Employee employee);
        Employee GetEmployeeById(int id);
        Employee EditEmployee(Employee employee);
        Employee DeleteEmployeeById(int id);
        string UploadImage(Employee employee);
    }
}
