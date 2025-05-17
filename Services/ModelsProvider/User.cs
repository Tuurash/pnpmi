namespace ModelsProvider;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public UserRole Role { get; set; } = UserRole.Employee;
}


public enum UserRole
{
    Admin,
    Manager,
    Employee
}

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<User> Users { get; set; } = new List<User>();
}

//Task: Id, Title, Description, Status (Todo / InProgress / Done), AssignedToUserId,CreatedByUserId, TeamId, DueDate

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