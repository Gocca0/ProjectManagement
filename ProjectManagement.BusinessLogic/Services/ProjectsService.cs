using ProjectManagement.BusinessLogic.Abstraction;
using ProjectManagement.BusinessLogic.Dto;
using ProjectManagement.DataAccess.SQLite.Abstraction;
using ProjectManagement.DataAccess.SQLite.Models;


namespace ProjectManagement.BusinessLogic.Services
{
    class ProjectsService(IProjectsRepository projectsRepository) : IProjectsService
    {
        public async Task<List<ProjectDto>> Get()
        {
            return ProjectDto.ToEnumerableProjectsDto(await projectsRepository.Get()).ToList();
        }


        public async Task<ProjectDto?> Get(int? id)
        {
            int Id = id ?? -1;
            return ProjectDto.ToProjectDto(await projectsRepository.Get(Id));
        }


        public async Task<List<ProjectsWithTasksAndEmployeesDto>> GetWithEmployeesAndTasks()
        {
            return ProjectsWithTasksAndEmployeesDto.ToEnumerableProjectsDto(await projectsRepository.GetWithEmployeesAndTasks()).ToList();
        }


        public async Task<ProjectsWithTasksAndEmployeesDto?> GetWithEmployeesAndTasks(int? id)
        {
            int Id = id ?? -1;
            return ProjectsWithTasksAndEmployeesDto.ToProjectDto(await projectsRepository.GetWithEmployeesAndTasks(Id));
        }


        public async Task Create(ProjectDto project)
        {
            await projectsRepository.Add(ProjectDto.ToProjectEntity(project));
        }


        public async Task Update(ProjectDto project)
        {
            await projectsRepository.Update(ProjectDto.ToProjectEntity(project));
        }

        public async Task AddEmployee(int projectId, int employeeId)
        {
            await projectsRepository.AddEmployee(projectId, employeeId);
        }

        public async Task DeleteEmployee(int projectId, int employeeId)
        {
            await projectsRepository.DeleteEmployee(projectId, employeeId);
        }


        public async Task Delete(int id)
        {
            await projectsRepository.Delete(id);
        }
    }
}
