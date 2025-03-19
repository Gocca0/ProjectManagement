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
    public class TasksRepository : ITasksRepository
    {
        private readonly ProjectManagementDbContext _dbContext;

        public TasksRepository(ProjectManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TaskEntity>> Get()
        {
            return await _dbContext.Tasks
                .AsNoTracking()
                .Include(t => t.Author)
                .Include(t => t.Executor)
                .Include(t => t.Project)
                .OrderBy(t => t.Id)
                .ToListAsync();
        }
        public async Task<TaskEntity?> Get(int id)
        {
            return await _dbContext.Tasks
                  .AsNoTracking()
                  .Include(t => t.Author)
                  .Include(t => t.Executor)
                  .Include(t => t.Project)
                  .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task Add(TaskEntity task)
        {

            var employee = await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.Id == task.ExecutorId);

            if (employee is not null)
            {
                employee.Tasks.Add(task);
            }

            await _dbContext.AddAsync(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(TaskEntity task)
        {
            await _dbContext.Tasks
                .Where(t => t.Id == task.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(t => t.Name, task.Name)
                    .SetProperty(t => t.AuthorId, task.AuthorId)
                    .SetProperty(t => t.ExecutorId, task.ExecutorId)
                    .SetProperty(t => t.ProjectId, task.ProjectId)
                    .SetProperty(t => t.Status, task.Status)
                    .SetProperty(t => t.Comment, task.Comment)
                    .SetProperty(t => t.Priority, task.Priority)
                    );
        }

        public async Task Delete(int id)
        {
            await _dbContext.Tasks
                .Where(e => e.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
