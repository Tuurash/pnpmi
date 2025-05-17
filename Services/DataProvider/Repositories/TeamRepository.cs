using Microsoft.EntityFrameworkCore;

using ModelsProvider;

namespace DataProvider.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly DataContext _context;

    public TeamRepository(DataContext context)
    {
        _context = context;
    }

    // Create
    public async Task<Team> AddTeamAsync(Team team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();
        return team;
    }

    // Read All
    public async Task<List<Team>> GetAllTeamsAsync()
    {
        return await _context.Teams
            .Include(t => t.Users)
            .ToListAsync();
    }

    // Read by Id
    public async Task<Team?> GetTeamByIdAsync(int id)
    {
        return await _context.Teams
            .Include(t => t.Users)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    // Update
    public async Task<bool> UpdateTeamAsync(Team team)
    {
        var existingTeam = await _context.Teams
            .Include(t => t.Users)
            .FirstOrDefaultAsync(t => t.Id == team.Id);
        if (existingTeam == null)
            return false;

        existingTeam.Name = team.Name;
        existingTeam.Description = team.Description;
        existingTeam.Users = team.Users;

        await _context.SaveChangesAsync();
        return true;
    }

    // Delete
    public async Task<bool> DeleteTeamAsync(int id)
    {
        var team = await _context.Teams.FindAsync(id);
        if (team == null)
            return false;

        _context.Teams.Remove(team);
        await _context.SaveChangesAsync();
        return true;
    }
}
