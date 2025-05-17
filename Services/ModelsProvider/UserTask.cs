namespace ModelsProvider;


public enum TaskStatus
{
    Todo,
    InProgress,
    Done
}
public class UserTask
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public TaskStatus Status { get; set; } = TaskStatus.Todo;
    public int AssignedToUserId { get; set; }
    public int CreatedByUserId { get; set; }
    public int TeamId { get; set; }
    public DateTime DueDate { get; set; } = DateTime.Now.AddDays(7);
}