using Microsoft.AspNetCore.Mvc;
using ProjectManagement.API.RequestModels.Task;
using ProjectManagement.BusinessLogic.Abstraction;
using ProjectManagement.BusinessLogic.Dto;
using ProjectManagement.DataAccess.SQLite.Models;



namespace ProjectManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController(ITasksService tasksService, IEmployeesService employeesService, IProjectsService projectsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var task = await tasksService.Get();
            return Ok(task);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await tasksService.Get(id);
            return Ok(task);
        }

        // Нужно добавить маппер
        [HttpPost]
        public async Task<IActionResult> Create(TaskAddRequest task)
        {
            var dto = TaskAddRequest.ToDto(task);
            await tasksService.Create(dto);
            return NoContent();
        }

        // Нужно добавить маппер
        [HttpPut]
        public async Task<IActionResult> Update(TaskUpdateRequest task)
        {
            var dto = TaskUpdateRequest.ToDto(task);
            await tasksService.Update(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await tasksService.Delete(id);
            return NoContent();
        }
    }
}
