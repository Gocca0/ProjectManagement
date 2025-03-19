using ProjectManagement.DataAccess.SQLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.DataAccess.SQLite.Abstraction
{
    public interface IProjectsRepository
    {
        public Task<List<ProjectEntity>> Get();

        public Task<ProjectEntity> Get(int id);
        public Task<List<ProjectEntity>> GetWithEmployeesAndTasks();
        public Task<ProjectEntity?> GetWithEmployeesAndTasks(int id);

        public Task Add(ProjectEntity project);
        public Task AddEmployee(int projectId, int employeeId);
        public Task DeleteEmployee(int projectId, int employeeId);
        public Task Update(ProjectEntity project);

        public Task Delete(int id);
    }
}
