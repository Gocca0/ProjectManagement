using ProjectManagement.BusinessLogic.Abstraction;
using ProjectManagement.BusinessLogic.Dto;
using ProjectManagement.DataAccess.SQLite.Abstraction;
using ProjectManagement.DataAccess.SQLite.Models;



namespace ProjectManagement.BusinessLogic.Services
{
    class EmployeesService(IEmployeesRepository employeesRepository) : IEmployeesService 
    {
        public async Task<List<EmployeeDto>> Get()
        {
            return EmployeeDto.ToEnumerableEmployeeDto(await employeesRepository.Get()).ToList();
        }


        public async Task<EmployeeDto?> Get(int? id)
        {
            int Id = id ?? -1;
            return EmployeeDto.ToEmployeeDto(await employeesRepository.Get(Id));
        }


        public async Task<List<EmployeeWithProjectsAndTasksDto>> GetWithProjectsAndTasks()
        {
            return EmployeeWithProjectsAndTasksDto.ToEnumerableEmployeeDto(await employeesRepository.GetWithPrjectsAndTasks()).ToList();
        }

        public async Task<EmployeeWithProjectsAndTasksDto?> GetWithProjectsAndTasks(int id)
        {
            return EmployeeWithProjectsAndTasksDto.ToEmployeeDto(await employeesRepository.GetWithPrjectsAndTasks(id));
        }


        public async Task Create(EmployeeDto employee)
        {
            await employeesRepository.Add(EmployeeDto.ToEmployeeEntity(employee));
        }


        public async Task Update(EmployeeDto employee)
        {
            await employeesRepository.Update(EmployeeDto.ToEmployeeEntity(employee));

        }


        public async Task Delete(int id)
        {
            await employeesRepository.Delete(id);
        }

    }
}
