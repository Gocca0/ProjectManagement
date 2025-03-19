using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.SQLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.DataAccess.SQLite.Abstraction
{
    public interface IEmployeesRepository
    {
        public Task<List<EmployeeEntity>> Get();
        public Task<List<EmployeeEntity>> GetWithPrjectsAndTasks();
        public Task<EmployeeEntity?> GetWithPrjectsAndTasks(int id);

        public Task<EmployeeEntity?> Get(int id);

        public Task Add(EmployeeEntity employee);

        public Task Update(EmployeeEntity employee);

        public Task Delete(int id);
        
    }
}
