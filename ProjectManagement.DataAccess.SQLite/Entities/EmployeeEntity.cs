

namespace ProjectManagement.DataAccess.SQLite.Models
{
    public class EmployeeEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();
        public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }
}
