using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.SQLite.Abstraction;
using ProjectManagement.DataAccess.SQLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.DataAccess.SQLite.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly ProjectManagementDbContext _dbContext;

        public EmployeesRepository(ProjectManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EmployeeEntity>> Get()
        {
            return await _dbContext.Employees
                .AsNoTracking()
                .OrderBy(e => e.Id)
                .ToListAsync();   
        }

        public async Task<EmployeeEntity?> Get(int id)
        {
            return await _dbContext.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<EmployeeEntity>> GetWithPrjectsAndTasks()
        {
            return await _dbContext.Employees
               .Include(e => e.Projects)
               .Include(e => e.Tasks)
               .OrderBy(e => e.Id)
               .ToListAsync();
        }

        public async Task<EmployeeEntity?> GetWithPrjectsAndTasks(int id)
        {
            return await _dbContext.Employees
                .Include(e => e.Projects)
                .Include(e => e.Tasks)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Add(EmployeeEntity employee)
        {
            var employeeEntity = new EmployeeEntity
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Projects = employee.Projects,
                Tasks = employee.Tasks
            };
            await _dbContext.AddAsync(employeeEntity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Update(EmployeeEntity employee)
        {
            await _dbContext.Employees
                .Where(e => e.Id == employee.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(e => e.FirstName, employee.FirstName)
                    .SetProperty(e => e.LastName, employee.LastName)
                    .SetProperty(e => e.Email, employee.Email));
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _dbContext.Employees
                .Where(e => e.Id == id)
                .ExecuteDeleteAsync();
        }

    }
}
