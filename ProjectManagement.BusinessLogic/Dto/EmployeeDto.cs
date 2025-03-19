using ProjectManagement.BusinessLogic.Dto;
using ProjectManagement.DataAccess.SQLite.Models;

namespace ProjectManagement.BusinessLogic.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }


        public static EmployeeDto ToEmployeeDto(EmployeeEntity employeeEntity)
        {
            return new EmployeeDto
            {
                Id = employeeEntity.Id,
                FirstName = employeeEntity.FirstName,
                LastName = employeeEntity.LastName,
                Email = employeeEntity.Email
            };
        }


        public static EmployeeEntity ToEmployeeEntity(EmployeeDto employeeDto)
        {
            return new EmployeeEntity
            {
                Id = employeeDto.Id,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email
            };
        }


        public static IEnumerable<EmployeeDto> ToEnumerableEmployeeDto(IEnumerable<EmployeeEntity>  employeeEntityList)
        {
            List<EmployeeDto> employeesList = new List<EmployeeDto>();

            foreach(EmployeeEntity employeeEntity in employeeEntityList)
            {
                employeesList.Add(EmployeeDto.ToEmployeeDto(employeeEntity));
            }

            return employeesList;
        }
        public static IEnumerable<EmployeeEntity> ToEnumerableEmployeeEntity(IEnumerable<EmployeeDto> employeeDtoList)
        {
            List<EmployeeEntity> employeesList = new List<EmployeeEntity>();

            foreach (EmployeeDto employeeDto in employeeDtoList)
            {
                employeesList.Add(EmployeeDto.ToEmployeeEntity(employeeDto));
            }

            return employeesList;
        }
    }


    public class EmployeeWithProjectsAndTasksDto : EmployeeDto
    {
        public IEnumerable<ProjectDto> Projects { get; set; }
        public IEnumerable<TaskDto> Tasks { get; set; }


        public static EmployeeWithProjectsAndTasksDto ToEmployeeDto(EmployeeEntity employeeEntity)
        {
            var projects = ProjectDto.ToEnumerableProjectsDto(employeeEntity.Projects);
            var tasks = TaskDto.ToEnumerableTasksDto(employeeEntity.Tasks);


            return new EmployeeWithProjectsAndTasksDto
            {
                Id = employeeEntity.Id,
                FirstName = employeeEntity.FirstName,
                LastName = employeeEntity.LastName,
                Email = employeeEntity.Email,
                Projects = projects,
                Tasks = tasks
            };
        }


        public static EmployeeEntity ToEmployeeEntity(EmployeeWithProjectsAndTasksDto employeeDto)
        {
            return new EmployeeEntity
            {
                Id = employeeDto.Id,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                Projects = ProjectDto.ToEnumerableProjectsEntity(employeeDto.Projects).ToList(),
                Tasks = TaskDto.ToEnumerableTasksEntity(employeeDto.Tasks).ToList()
            };
        }

        public static IEnumerable<EmployeeWithProjectsAndTasksDto> ToEnumerableEmployeeDto(IEnumerable<EmployeeEntity> employeeEntityList)
        {
            List<EmployeeWithProjectsAndTasksDto> employeesList = new List<EmployeeWithProjectsAndTasksDto>();

            foreach (EmployeeEntity employeeEntity in employeeEntityList)
            {
                employeesList.Add(EmployeeWithProjectsAndTasksDto.ToEmployeeDto(employeeEntity));
            }

            return employeesList;
        }
    }
}
