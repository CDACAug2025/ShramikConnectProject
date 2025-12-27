using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShramikConnectWebApi.Data;
using ShramikConnectWebApi.Models;
using ShramikConnectWebApi.Models.Profile;
using ShramikConnectWebApi.Shared.Enums;
using System.Security.Claims;


namespace ShramikConnectWebApi.Controllers;

[ApiController]
[Route("api/profile")]
public class ProfileController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProfileController(AppDbContext context)
    {
        _context = context;
    }

    // ======================
    // WORKER PROFILE
    // ======================
    [HttpPut("worker")]
    public async Task<IActionResult> UpdateWorkerProfile(WorkerProfileDto dto)
    {
        int userId = GetUserId();

        var worker = await _context.Workers
            .FirstOrDefaultAsync(w => w.UserId == userId);

        if (worker == null)
            return NotFound("Worker profile not found.");

        worker.SkillSet = dto.SkillSet;
        worker.ExperienceYears = dto.ExperienceYears;
        worker.Location = dto.Location;

        await _context.SaveChangesAsync();
        return Ok("Worker profile updated.");
    }


    // ======================
    // CLIENT PROFILE
    // ======================
    [HttpPut("client")]
    public async Task<IActionResult> UpdateClientProfile(ClientProfileDto dto)
    {
        int userId = GetUserId();

        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (client == null)
            return NotFound("Client profile not found.");

        client.Address = dto.Address;

        await _context.SaveChangesAsync();
        return Ok("Client profile updated.");
    }


    // ======================
    // ORGANIZATION PROFILE
    // ======================
    [HttpPut("organization")]
    public async Task<IActionResult> UpdateOrganizationProfile(OrganizationProfileDto dto)
    {
        int userId = GetUserId();

        var org = await _context.Organizations
            .FirstOrDefaultAsync(o => o.UserId == userId);

        if (org == null)
            return NotFound("Organization profile not found.");

        org.OrgName = dto.OrgName;
        org.GSTNumber = dto.GSTNumber;
        org.Address = dto.Address;

        await _context.SaveChangesAsync();
        return Ok("Organization profile updated.");
    }


    // ======================
    // TEMP USER ID (REPLACE WITH JWT)
    // ======================
    private int GetUserId()
    {
        return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

}
