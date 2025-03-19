using ProjectManagement.BusinessLogic.Dto;
using ProjectManagement.DataAccess.SQLite.Models;

namespace ProjectManagement.API.RequestModels.Task
{
    public class TaskAddRequest
    {
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public int? ExecutorId { get; set; }
        public int? ProjectId { get; set; }
        public StatusTask Status { get; set; }
        public string? Comment { get; set; }
        public int Priority { get; set; }

        public static TaskDto ToDto(TaskAddRequest request)
        {
            return new TaskDto
            {
                Name = request.Name,
                AuthorId = request.AuthorId,
                ExecutorId = request.ExecutorId,
                ProjectId = request.ProjectId,
                Status = request.Status,
                Comment = request.Comment,
                Priority = request.Priority
            };
        }
    }
}
