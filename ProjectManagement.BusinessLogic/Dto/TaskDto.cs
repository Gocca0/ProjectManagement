using ProjectManagement.BusinessLogic.Dto;
using ProjectManagement.DataAccess.SQLite.Models;
using System.Threading.Tasks;


namespace ProjectManagement.BusinessLogic.Dto
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public string Author { get; set; }
        public int? ExecutorId { get; set; }
        public string? Executor { get; set; }
        public int? ProjectId { get; set; }
        public string? Project { get; set; }
        public StatusTask Status { get; set; }
        public string? Comment { get; set; }
        public int Priority { get; set; }


        public static TaskDto ToTaskDto(TaskEntity taskEntity)
        {
            return new TaskDto
            {
                Id = taskEntity.Id,
                Name = taskEntity.Name,
                AuthorId = taskEntity.AuthorId,
                Author = taskEntity.Author != null
                                ? $"{taskEntity.Author.FirstName} {taskEntity.Author.LastName}"
                                : null,
                ExecutorId = taskEntity.ExecutorId,
                Executor = taskEntity.Executor != null
                                ? $"{taskEntity.Executor.FirstName} {taskEntity.Executor.LastName}"
                                : null,
                ProjectId = taskEntity.ProjectId,
                Project = taskEntity.Project != null
                                ? $"{taskEntity.Project.Name}"
                                : null,
                Status = taskEntity.Status,
                Comment = taskEntity.Comment,
                Priority = taskEntity.Priority
            };
        }


        public static TaskEntity ToTaskEntity(TaskDto taskDto)
        {
            return new TaskEntity
            {
                Id = taskDto.Id,
                Name = taskDto.Name,
                AuthorId = taskDto.AuthorId,
                ExecutorId = taskDto.ExecutorId,
                ProjectId = taskDto.ProjectId,
                Status = taskDto.Status,
                Comment = taskDto.Comment,
                Priority = taskDto.Priority
            };
        }


        public static IEnumerable<TaskDto> ToEnumerableTasksDto(IEnumerable<TaskEntity> taskEntityList)
        {
            List<TaskDto> taskDtoList = new List<TaskDto>();

            foreach(TaskEntity taskEntity in taskEntityList)
            {
                taskDtoList.Add(TaskDto.ToTaskDto(taskEntity));
            }


            return taskDtoList;
        }

        public static IEnumerable<TaskEntity> ToEnumerableTasksEntity(IEnumerable<TaskDto> taskDtoList)
        {
            List<TaskEntity> taskEntityList = new List<TaskEntity>();

            foreach (TaskDto taskDto in taskDtoList)
            {
                taskEntityList.Add(TaskDto.ToTaskEntity(taskDto));
            }


            return taskEntityList;
        }
    }
}
