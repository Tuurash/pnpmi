using ModelsProvider;

namespace DataProvider.Repositories;

public interface ITaskRepository
{
    Task<UserTask> AddTaskAsync(UserTask task);
    Task<List<UserTask>> GetAllTasksAsync();
    Task<UserTask?> GetTaskByIdAsync(int id);
    Task<bool> UpdateTaskAsync(UserTask task);
    Task<bool> DeleteTaskAsync(int id);
    Task<List<UserTask>> SearchTasksAsync(ModelsProvider.TaskStatus? status = null, int? assignedToUserId = null, int? teamId = null, DateTime? dueDateFrom = null, DateTime? dueDateTo = null);

}