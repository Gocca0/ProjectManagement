using ProjectManagement.BusinessLogic.Dto;
using ProjectManagement.DataAccess.SQLite.Models;



namespace ProjectManagement.BusinessLogic.Abstraction
{
    public interface ITasksService
    {
        public Task<List<TaskDto>> Get();
        public Task<TaskDto?> Get(int? id);
        public Task Create(TaskDto project);
        public Task Update(TaskDto project);
        public Task Delete(int id);
    }
}
