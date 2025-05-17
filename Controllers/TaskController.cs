using DataProvider.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ModelsProvider;

using System.Security.Claims;

namespace pnpmi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;

    public TaskController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    // Managers can create tasks
    [HttpPost]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<ActionResult<UserTask>> Create(UserTask task)
    {
        var createdTask = await _taskRepository.AddTaskAsync(task);
        return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
    }

    // Managers can update any task, Employees can only update their assigned task's status
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Update(int id, UserTask task)
    {
        var userRole = User.FindFirstValue(ClaimTypes.Role);
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        if (userRole == "Manager")
        {
            // Managers can update any task
            var updated = await _taskRepository.UpdateTaskAsync(task);
            if (!updated)
                return NotFound();
            return NoContent();
        }
        else if (userRole == "Employee")
        {
            // Employees can only update status of their assigned tasks
            var existingTask = await _taskRepository.GetTaskByIdAsync(id);
            if (existingTask == null || existingTask.AssignedToUserId != userId)
                return Forbid();

            existingTask.Status = task.Status;
            var updated = await _taskRepository.UpdateTaskAsync(existingTask);
            if (!updated)
                return NotFound();
            return NoContent();
        }
        else
        {
            return Forbid();
        }
    }


    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<UserTask>>> GetAll()
    {
        var userRole = User.FindFirstValue(ClaimTypes.Role);
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        if (userRole == "Manager")
        {
            var tasks = await _taskRepository.GetAllTasksAsync();
            return Ok(tasks);
        }
        else if (userRole == "Employee")
        {
            var tasks = await _taskRepository.SearchTasksAsync(assignedToUserId: userId);
            return Ok(tasks);
        }
        else
        {
            return Forbid();
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<UserTask>> GetById(int id)
    {
        var userRole = User.FindFirstValue(ClaimTypes.Role);
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var task = await _taskRepository.GetTaskByIdAsync(id);

        if (task == null)
            return NotFound();

        if (userRole == "Manager" || (userRole == "Employee" && task.AssignedToUserId == userId))
            return Ok(task);

        return Forbid();
    }
}
