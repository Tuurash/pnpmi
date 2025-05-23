﻿using DataProvider.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ModelsProvider;

namespace pnpmi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly ITeamRepository _teamRepository;

    public TeamController(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }


    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Team>> Create(Team team)
    {
        var createdTeam = await _teamRepository.AddTeamAsync(team);
        return CreatedAtAction(nameof(GetById), new { id = createdTeam.Id }, createdTeam);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, Team team)
    {
        if (id != team.Id)
            return BadRequest();

        //Get the existing team to check if it exists
        var existingTeam = await _teamRepository.GetTeamByIdAsync(id);
        if (existingTeam == null)
            return NotFound();



        var updated = await _teamRepository.UpdateTeamAsync(team);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    //add User to team
    [HttpPut("{teamId}/addUser/{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddUserToTeam(int teamId, int userId)
    {
        var team = await _teamRepository.GetTeamByIdAsync(teamId);
        if (team == null)
            return NotFound();
        var userAdded = await _teamRepository.AddUserToTeamAsync(teamId, userId);
        if (!userAdded)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _teamRepository.DeleteTeamAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }


    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Team>>> GetAll()
    {
        var teams = await _teamRepository.GetAllTeamsAsync();
        return Ok(teams);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Team>> GetById(int id)
    {
        var team = await _teamRepository.GetTeamByIdAsync(id);
        if (team == null)
            return NotFound();
        return Ok(team);
    }
}
