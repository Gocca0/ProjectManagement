using ProjectManagement.BusinessLogic.Dto;
using ProjectManagement.DataAccess.SQLite.Models;

namespace ProjectManagement.API.RequestModels.Task
{
    public class TaskUpdateRequest : TaskAddRequest
    {
        public int Id { get; set; }
        public static TaskDto ToDto(TaskUpdateRequest request)
        {
            return new TaskDto
            {
                Id = request.Id,
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
