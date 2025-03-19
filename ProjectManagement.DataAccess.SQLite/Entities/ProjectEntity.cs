
namespace ProjectManagement.DataAccess.SQLite.Models
{
    public class ProjectEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CustomerCompany { get; set; }
        public string ExecutorCompany { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }
        public int? ManagerId { get; set; }
        public EmployeeEntity? Manager { get; set; }
        public ICollection<EmployeeEntity> Employees { get; set; } = new List<EmployeeEntity>();
        public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>(); 
    }
}
