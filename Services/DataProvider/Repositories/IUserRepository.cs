using ModelsProvider;

namespace DataProvider.Repositories;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user);
    Task<List<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<bool> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int id);
}