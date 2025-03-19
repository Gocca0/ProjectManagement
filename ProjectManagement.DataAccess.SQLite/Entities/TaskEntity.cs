
namespace ProjectManagement.DataAccess.SQLite.Models
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public EmployeeEntity Author { get; set; }
        public int? ExecutorId { get; set; }
        public EmployeeEntity? Executor { get; set; }
        public int? ProjectId { get; set; }
        public ProjectEntity? Project { get; set; }
        public StatusTask Status { get; set; }
        public string? Comment { get; set; }
        public int Priority { get; set; }
    }
    public enum StatusTask { ToDo, InProgress, Done }
}
