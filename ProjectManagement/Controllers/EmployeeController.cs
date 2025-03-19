using Microsoft.AspNetCore.Mvc;
using ProjectManagement.BusinessLogic.Abstraction;
using ProjectManagement.BusinessLogic.Dto;

namespace ProjectManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController(IEmployeesService employeesService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await employeesService.Get();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await employeesService.Get(id);
            return Ok(employee);
        }

        [HttpGet("withProjectAndTasks")]
        public async Task<IActionResult> GetWithProjectsAndTasks()
        {
            var employees = await employeesService.GetWithProjectsAndTasks();
            return Ok(employees);
        }

        [HttpGet("withProjectAndTasks/{id}")]
        public async Task<IActionResult> GetWithProjectsAndTasks(int id)
        {
            var employee = await employeesService.GetWithProjectsAndTasks(id);
            return Ok(employee);
        }

        // Нужно добавить маппер
        [HttpPost]
        public async Task<IActionResult> Create(int id,EmployeeDto employee)
        {       
            await employeesService.Create(employee);
            return NoContent();
        }

        // Нужно добавить маппер
        [HttpPut]
        public async Task<IActionResult> Update(EmployeeDto employee)
        {
            await employeesService.Update(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await employeesService.Delete(id);
            return NoContent();
        }
    }
}
