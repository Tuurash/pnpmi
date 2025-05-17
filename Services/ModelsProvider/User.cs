namespace ModelsProvider;

public class User
{

    //impl GUID later
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

