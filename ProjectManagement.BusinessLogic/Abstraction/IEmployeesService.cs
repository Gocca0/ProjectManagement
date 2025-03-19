using ProjectManagement.BusinessLogic.Dto;
using ProjectManagement.DataAccess.SQLite.Models;



namespace ProjectManagement.BusinessLogic.Abstraction
{
    public interface IEmployeesService
    {
        public Task<List<EmployeeDto>> Get();
        public Task<EmployeeDto?> Get(int? id);
        public Task<List<EmployeeWithProjectsAndTasksDto>> GetWithProjectsAndTasks();
        public Task<EmployeeWithProjectsAndTasksDto?> GetWithProjectsAndTasks(int id);
        public Task Create(EmployeeDto employee);
        public Task Update(EmployeeDto employee);
        public Task Delete(int id);

    }
}
