using ProjectManagement.BusinessLogic.Abstraction;
using ProjectManagement.BusinessLogic.Dto;
using ProjectManagement.DataAccess.SQLite.Abstraction;
using ProjectManagement.DataAccess.SQLite.Models;
using ProjectManagement.DataAccess.SQLite.Repositories;


namespace ProjectManagement.BusinessLogic.Services
{
    public class TasksService(ITasksRepository tasksRepository) : ITasksService
    {
        public async Task<List<TaskDto>> Get()
        {
            return TaskDto.ToEnumerableTasksDto(await tasksRepository.Get()).ToList();
        }

        public async Task<TaskDto?> Get(int? id)
        {
            int Id = id ?? -1;
            return TaskDto.ToTaskDto(await tasksRepository.Get(Id));
        }

        public async Task Create(TaskDto task)
        {
            await tasksRepository.Add(TaskDto.ToTaskEntity(task));
        }

        public async Task Update(TaskDto task)
        {
            await tasksRepository.Update(TaskDto.ToTaskEntity(task));

        }

        public async Task Delete(int id)
        {
            await tasksRepository.Delete(id);
        }
    }
}
