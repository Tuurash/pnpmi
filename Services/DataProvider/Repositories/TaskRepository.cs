using Microsoft.EntityFrameworkCore;

using ModelsProvider;

namespace DataProvider.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly DataContext _context;

    public TaskRepository(DataContext context)
    {
        _context = context;
    }

    // Create
    public async Task<UserTask> AddTaskAsync(UserTask task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    // Read All
    public async Task<List<UserTask>> GetAllTasksAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

    // Read by Id
    public async Task<UserTask?> GetTaskByIdAsync(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    // Update
    public async Task<bool> UpdateTaskAsync(UserTask task)
    {
        var existingTask = await _context.Tasks.FindAsync(task.Id);
        if (existingTask == null)
            return false;

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.Status = task.Status;
        existingTask.AssignedToUserId = task.AssignedToUserId;
        existingTask.CreatedByUserId = task.CreatedByUserId;
        existingTask.TeamId = task.TeamId;
        existingTask.DueDate = task.DueDate;

        await _context.SaveChangesAsync();
        return true;
    }

    // Delete
    public async Task<bool> DeleteTaskAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
            return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }

    // Search and filter tasks by Status, AssignedTo, Team, and DueDate
    public async Task<List<UserTask>> SearchTasksAsync(
        ModelsProvider.TaskStatus? status = null,
        int? assignedToUserId = null,
        int? teamId = null,
        DateTime? dueDateFrom = null,
        DateTime? dueDateTo = null)
    {
        var query = _context.Tasks.AsQueryable();

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        if (assignedToUserId.HasValue)
            query = query.Where(t => t.AssignedToUserId == assignedToUserId.Value);

        if (teamId.HasValue)
            query = query.Where(t => t.TeamId == teamId.Value);

        if (dueDateFrom.HasValue)
            query = query.Where(t => t.DueDate >= dueDateFrom.Value);

        if (dueDateTo.HasValue)
            query = query.Where(t => t.DueDate <= dueDateTo.Value);

        return await query.ToListAsync();
    }
}
