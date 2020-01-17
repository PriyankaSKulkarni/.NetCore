using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext context;
        public EmployeeRepository(EmployeeDbContext context)
        {
            this.context = context;

        }
        public Employee Add(Employee employee)
        {
            context.Employee.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            var employee = context.Employee.Find(id);
            if (employee != null) {
                context.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        public Employee GetEmployee(int Id)
        {
            return context.Employee.Find(Id);
        }

        public List<Employee> GetEmployees(int id)
        {
            return context.Employee.Where(i=>i.Id==id).ToList();
        }

        public List<Employee> GetEmployees()
        {
            return context.Employee.ToList();
        }      

        
        public Employee Update(Employee employee)
        {
            var _employee = context.Employee.Attach(employee);
            _employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return employee;
        }
    }
}
