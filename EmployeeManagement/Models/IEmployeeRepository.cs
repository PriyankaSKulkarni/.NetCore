using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public interface IEmployeeRepository
    {
        Employee Add(Employee employee);
        Employee Update(Employee employee);
        Employee Delete(int id);
        Employee GetEmployee(int Id);
        List<Employee> GetEmployees(int Id);
        List<Employee> GetEmployees();
    }
}
