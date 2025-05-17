namespace ModelsProvider
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public UserRole Role { get; set; } = UserRole.Employee;
    }


    public enum UserRole
    {
        Admin,
        Manager,
        Employee
    }

}
