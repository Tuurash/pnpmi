using ModelsProvider;

namespace DataProvider.Repositories;

public interface ITeamRepository
{
    Task<Team> AddTeamAsync(Team team);
    Task<List<Team>> GetAllTeamsAsync();
    Task<Team?> GetTeamByIdAsync(int id);
    Task<bool> UpdateTeamAsync(Team team);
    Task<bool> DeleteTeamAsync(int id);
    Task<bool> AddUserToTeamAsync(int teamId, int userId);
}