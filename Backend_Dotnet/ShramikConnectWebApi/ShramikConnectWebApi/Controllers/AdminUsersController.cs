using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShramikConnectWebApi.Data;
using ShramikConnectWebApi.Models.Admin;
using ShramikConnectWebApi.Shared.Enums;

namespace ShramikConnectWebApi.Controllers;

[ApiController]
[Route("api/admin/users")]
// [Authorize(Roles = "Admin")] // enable after JWT
public class AdminUsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminUsersController(AppDbContext context)
    {
        _context = context;
    }

    // ======================
    // GET ALL USERS WITH PROFILES
    // ======================
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _context.Users
            .Include(u => u.Role)
            .Include(u => u.Worker)
            .Include(u => u.Client)
            .Include(u => u.Organization)
            .AsNoTracking()
            .Select(u => new UserProfileResponseDto
            {
                UserId = u.UserId,
                FullName = u.FullName,
                Email = u.Email,
                Phone = u.Phone,
                Role = u.Role.RoleName,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt,

                WorkerProfile = u.Worker == null ? null : new WorkerProfileViewDto
                {
                    SkillSet = u.Worker.SkillSet,
                    ExperienceYears = u.Worker.ExperienceYears,
                    Location = u.Worker.Location,
                    Rating = u.Worker.Rating
                },

                ClientProfile = u.Client == null ? null : new ClientProfileViewDto
                {
                    Address = u.Client.Address
                },

                OrganizationProfile = u.Organization == null ? null : new OrganizationProfileViewDto
                {
                    OrgName = u.Organization.OrgName,
                    GSTNumber = u.Organization.GSTNumber,
                    Address = u.Organization.Address
                }
            })
            .ToListAsync();

        return Ok(users);
    }
}
