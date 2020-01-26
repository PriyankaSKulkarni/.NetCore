using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly EmployeeDbContext context;

        private readonly EmployeeDbContext _context = null;
        private readonly DbSet<T> table = null;


        public GenericRepository(EmployeeDbContext context)
        {
            this.context = context;
            table = context.Set<T>();

        }
        public T Add(T entity)
        {
            table.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public T Delete(int id)
        {
            var entity = table.Find(id);
            if (entity != null)
            {
                context.Remove(entity);
                context.SaveChanges();
            }
            return entity;
        }

        public T GetEmployee(int Id)
        {
            return table.Find(Id);
        }

        public List<T> GetEmployees(int id)
        {
            return table.ToList();
        }

        public List<T> GetEmployees()
        {
            return table.ToList();
        }


        public T Update(T entity)
        {
            var entityModified = table.Attach(entity);
            entityModified.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return entity;
        }
    }
}
