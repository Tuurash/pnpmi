namespace ModelsProvider.ViewModels;

public class LoginRequest
{
    public LoginRequest(string email, string password)
    {
        //implement validation
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Email and password cannot be empty.");
        }

        Email = email;
        Password = password;
    }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}