using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.SQLite.Abstraction;
using ProjectManagement.DataAccess.SQLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectManagement.DataAccess.SQLite.Repositories
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly ProjectManagementDbContext _dbContext;

        public ProjectsRepository(ProjectManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProjectEntity>> Get()
        {
            return await _dbContext.Projects
                .AsNoTracking()
                .Include(p => p.Manager)
                .OrderBy(p => p.Id)
                .ToListAsync();
        }
        public async Task<ProjectEntity?> Get(int id)
        {
            return await _dbContext.Projects
                  .AsNoTracking()
                  .Include(p => p.Manager)
                  .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<ProjectEntity>> GetWithEmployeesAndTasks()
        {
            return await _dbContext.Projects
                .AsNoTracking()
                .Include(p => p.Employees)
                .Include(p => p.Tasks)
                    .ThenInclude(t => t.Author)
                .Include(p => p.Tasks)
                    .ThenInclude(t => t.Executor)
                .Include(p => p.Manager)
                .OrderBy(p => p.Id)
                .ToListAsync();
        }
        public async Task<ProjectEntity?> GetWithEmployeesAndTasks(int id)
        {
            return await _dbContext.Projects
                  .AsNoTracking()
                  .Include(p => p.Employees)
                  .Include(p => p.Tasks)
                    .ThenInclude(t => t.Author)
                  .Include(p => p.Tasks)
                    .ThenInclude(t => t.Executor)
                  .Include(p => p.Manager)
                  .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Add(ProjectEntity project)
        {
            ProjectEntity projectEntity = new ProjectEntity()
            {
                Name = project.Name,
                CustomerCompany = project.CustomerCompany,
                ExecutorCompany = project.ExecutorCompany,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority,
                ManagerId = project.ManagerId == -1
                                ? null
                                : project.ManagerId,
            };

            await _dbContext.AddAsync(projectEntity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Update(ProjectEntity project)
        {
            var employee = await _dbContext.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == project.ManagerId);

            project.Manager = employee;

            await _dbContext.Projects
                .Where(p => p.Id == project.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.Name, project.Name)
                    .SetProperty(p => p.CustomerCompany, project.CustomerCompany)
                    .SetProperty(p => p.ExecutorCompany, project.ExecutorCompany)
                    .SetProperty(p => p.StartDate, project.StartDate)
                    .SetProperty(p => p.EndDate, project.EndDate)
                    .SetProperty(p => p.Priority, project.Priority)
                    .SetProperty(p => p.ManagerId, project.ManagerId)
                    );
        }

        public async Task AddEmployee(int projectId, int employeeId)
        {
            var project = await _dbContext.Projects
                  .FirstOrDefaultAsync(p => p.Id == projectId);


            var employee = await _dbContext.Employees
                  .AsNoTracking()
                  .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (project is not null && employee is not null)
            {
                project.Employees.Add(employee);
                await _dbContext.SaveChangesAsync();
            } 
        }
        public async Task DeleteEmployee(int projectId, int employeeId)
        {
            var project = await _dbContext.Projects
                  .Include(p => p.Employees)
                  .FirstOrDefaultAsync(p => p.Id == projectId);


            var employee = await _dbContext.Employees
                  .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (project is not null && employee is not null)
            {
                project.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            await _dbContext.Projects
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
