using ProjectManagement.BusinessLogic.Dto;



namespace ProjectManagement.BusinessLogic.Abstraction
{
    public interface IProjectsService
    {
        public Task<List<ProjectDto>> Get();
        public Task<ProjectDto?> Get(int? id);
        public Task<List<ProjectsWithTasksAndEmployeesDto>> GetWithEmployeesAndTasks();
        public Task<ProjectsWithTasksAndEmployeesDto?> GetWithEmployeesAndTasks(int? id);
        public Task Create(ProjectDto project);
        public Task Update(ProjectDto project);
        public Task AddEmployee(int projectId, int employeeId);
        public Task DeleteEmployee(int projectId, int employeeId);
        public Task Delete(int id);

    }
}
