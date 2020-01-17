using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository() {
            _employeeList = new List<Employee> {

                new Employee(){Id=1,EmployeeName="ABC",Email="abc@gmail.com",Address=Address.Mumbai },
                new Employee(){Id=2,EmployeeName="XYZ",Email="xyz@gmail.com",Address=Address.Delhi },
                new Employee(){Id=3,EmployeeName="PQR",Email="pqr@gmail.com",Address=Address.Santacruz }
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(i => i.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _employeeList.FirstOrDefault(i => i.Id == id);
            if (employee!=null) {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(i=>i.Id==Id);
        }

        public List<Employee> GetEmployees()
        {
            return _employeeList;
        }

        public List<Employee> GetEmployees(int Id)
        {
            return _employeeList.Where(i=>i.Id==Id).ToList();
        }

        public Employee Update(Employee employee)
        {
            Employee _employee = _employeeList.FirstOrDefault(i => i.Id == employee.Id);
            if (employee != null)
            {
                _employee.EmployeeName = employee.EmployeeName;
                _employee.Email = employee.Email;
                _employee.Address = employee.Address;                
            }
            return employee;
        }
    }
}
