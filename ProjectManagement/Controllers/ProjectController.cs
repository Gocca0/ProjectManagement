using Microsoft.AspNetCore.Mvc;
using ProjectManagement.BusinessLogic.Abstraction;
using ProjectManagement.BusinessLogic.Dto;


namespace ProjectManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController(IProjectsService projectsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var project = await projectsService.Get();
            return Ok(project);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var project = await projectsService.Get(id);
            return Ok(project);
        }

        [HttpGet("withEmployeesAndTasks")]
        public async Task<IActionResult> GetWithEmployeesAndTasks()
        {
            var project = await projectsService.GetWithEmployeesAndTasks();
            return Ok(project);
        }

        [HttpGet("withEmployeesAndTasks/{id}")]
        public async Task<IActionResult> GetWithEmployeesAndTaskst(int id)
        {
            var project = await projectsService.GetWithEmployeesAndTasks(id);
            return Ok(project);
        }

        // Нужно добавить маппер
        [HttpPost]
        public async Task<IActionResult> Create(ProjectDto project)
        {
            await projectsService.Create(project);
            return NoContent();
        }

        // Нужно добавить маппер
        [HttpPut]
        public async Task<IActionResult> Update(ProjectDto project)
        {
            await projectsService.Update(project);
            return NoContent();
        }

        [HttpPut("{id}/addemployee")]
        public async Task<IActionResult> AddEmployee(int id, AddEmployeeToProjectDto employee)
        {
            await projectsService.AddEmployee(id, employee.EmployeeId);
            return NoContent();
        }

        [HttpDelete("{projid}/deleteemployee/{empid}")]
        public async Task<IActionResult> DeleteEmployee(int projid, int empid)
        {
            await projectsService.DeleteEmployee(projid, empid);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await projectsService.Delete(id);
            return NoContent();
        }
    }
}
