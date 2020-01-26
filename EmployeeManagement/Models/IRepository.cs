using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public interface IRepository<T> where T : class
    {
        
        T GetEmployee(int id);
        List<T> GetEmployees(int id);
        List<T> GetEmployees();
        T Add(T entity);
        T Update(T entity);
        T Delete(int id);
    }
}
