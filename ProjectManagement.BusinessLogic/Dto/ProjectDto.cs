using ProjectManagement.BusinessLogic.Dto;
using ProjectManagement.DataAccess.SQLite.Models;



namespace ProjectManagement.BusinessLogic.Dto
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CustomerCompany { get; set; }
        public string ExecutorCompany { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }
        public int? ManagerId { get; set; }
        public string? Manager { get; set; }
        

        public static ProjectDto ToProjectDto(ProjectEntity projectEntity)
        {
            return  new ProjectDto
            {
                Id = projectEntity.Id,
                Name = projectEntity.Name,
                CustomerCompany = projectEntity.CustomerCompany,
                ExecutorCompany = projectEntity.ExecutorCompany,
                StartDate = projectEntity.StartDate,
                EndDate = projectEntity.EndDate,
                Priority = projectEntity.Priority,
                ManagerId = projectEntity.ManagerId,
                Manager = projectEntity.Manager != null
                                ? $"{projectEntity.Manager.FirstName} {projectEntity.Manager.LastName}"
                                : null,
            };
        }


        public static ProjectEntity ToProjectEntity(ProjectDto projectDto)
        {
            return new ProjectEntity
            {
                Id = projectDto.Id,
                Name = projectDto.Name,
                CustomerCompany = projectDto.CustomerCompany,
                ExecutorCompany = projectDto.ExecutorCompany,
                StartDate = projectDto.StartDate,
                EndDate = projectDto.EndDate,
                Priority = projectDto.Priority,
                ManagerId = projectDto.ManagerId,
            };
        }


        public static IEnumerable<ProjectDto> ToEnumerableProjectsDto(IEnumerable<ProjectEntity> projectEntityList)
        {
            List<ProjectDto> projectDtoList = new List<ProjectDto>();

            foreach(ProjectEntity projectEntity in projectEntityList)
            {
                projectDtoList.Add(ProjectDto.ToProjectDto(projectEntity));
            }


            return projectDtoList;
        }
        public static IEnumerable<ProjectEntity> ToEnumerableProjectsEntity(IEnumerable<ProjectDto> projectDtoList)
        {
            List<ProjectEntity> projectEntityList = new List<ProjectEntity>();

            foreach (ProjectDto projectDto in projectDtoList)
            {
                projectEntityList.Add(ProjectDto.ToProjectEntity(projectDto));
            }


            return projectEntityList;
        }
    }

    public class ProjectsWithTasksAndEmployeesDto : ProjectDto
    {
        public IEnumerable<EmployeeDto> Employees { get; set; }
        public IEnumerable<TaskDto> Tasks { get; set; }


        public static ProjectsWithTasksAndEmployeesDto ToProjectDto (ProjectEntity projectEntity)
        {
            var employees = EmployeeDto.ToEnumerableEmployeeDto(projectEntity.Employees).ToList();
            var tasks = TaskDto.ToEnumerableTasksDto(projectEntity.Tasks).ToList();

            return new ProjectsWithTasksAndEmployeesDto
            {
                Id = projectEntity.Id,
                Name = projectEntity.Name,
                CustomerCompany = projectEntity.CustomerCompany,
                ExecutorCompany = projectEntity.ExecutorCompany,
                StartDate = projectEntity.StartDate,
                EndDate = projectEntity.EndDate,
                Priority = projectEntity.Priority,
                ManagerId = projectEntity.ManagerId,
                Manager = projectEntity.Manager != null
                                ? $"{projectEntity.Manager.FirstName} {projectEntity.Manager.LastName}"
                                : null,
                Employees = employees,
                Tasks = tasks
            };
        }

        public static ProjectEntity ToProjectEntity(ProjectsWithTasksAndEmployeesDto projectDto)
        {
            return new ProjectEntity
            {
                Id = projectDto.Id,
                Name = projectDto.Name,
                CustomerCompany = projectDto.CustomerCompany,
                ExecutorCompany = projectDto.ExecutorCompany,
                StartDate = projectDto.StartDate,
                EndDate = projectDto.EndDate,
                Priority = projectDto.Priority,
                ManagerId = projectDto.ManagerId,
                Employees = EmployeeDto.ToEnumerableEmployeeEntity(projectDto.Employees).ToList(),
                Tasks = TaskDto.ToEnumerableTasksEntity(projectDto.Tasks).ToList()
            };
        }

        public static IEnumerable<ProjectsWithTasksAndEmployeesDto> ToEnumerableProjectsDto(IEnumerable<ProjectEntity> projectEntityList)
        {
            List<ProjectsWithTasksAndEmployeesDto> projectDtoList = new List<ProjectsWithTasksAndEmployeesDto>();

            foreach (ProjectEntity projectEntity in projectEntityList)
            {
                projectDtoList.Add(ProjectsWithTasksAndEmployeesDto.ToProjectDto(projectEntity));
            }


            return projectDtoList;
        }
    }

    public class AddEmployeeToProjectDto
    {
        public int EmployeeId { get; set; }
    }
}
