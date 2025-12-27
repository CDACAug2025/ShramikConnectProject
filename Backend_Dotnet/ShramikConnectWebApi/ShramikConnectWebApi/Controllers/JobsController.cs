using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShramikConnectWebApi.Data;
using ShramikConnectWebApi.Models;
using ShramikConnectWebApi.Models.Jobs;
using ShramikConnectWebApi.Shared.Enums;

namespace ShramikConnectWebApi.Controllers;

[ApiController]
[Route("api/jobs")]
public class JobsController : ControllerBase
{
    private readonly AppDbContext _context;

    public JobsController(AppDbContext context)
    {
        _context = context;
    }

    // ======================
    // ADD JOB (Client / Organization)
    // ======================
    [HttpPost]
    public async Task<IActionResult> CreateJob(CreateJobDto dto)
    {
        int userId = GetUserId();

        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return Unauthorized();

        if (user.RoleId != (int)RoleType.Client &&
            user.RoleId != (int)RoleType.Organization)
        {
            return Forbid("Only clients or organizations can post jobs.");
        }

        var job = new Job
        {
            Title = dto.Title,
            Description = dto.Description,
            Category = dto.Category,
            Location = dto.Location,
            Budget = dto.Budget,
            Status = JobStatus.Open,
            CreatedAt = DateTime.UtcNow,
            PostedByUserId = userId
        };

        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();

        return Ok(new { job.JobId });
    }

    // ======================
    // GET ALL OPEN JOBS (PUBLIC)
    // ======================
    [HttpGet]
    public async Task<IActionResult> GetJobs()
    {
        var jobs = await _context.Jobs
            .Include(j => j.PostedByUser)
            .Where(j => j.Status == JobStatus.Open)
            .AsNoTracking()
            .Select(j => new JobResponseDto
            {
                JobId = j.JobId,
                Title = j.Title,
                Description = j.Description,
                Category = j.Category,
                Location = j.Location,
                Budget = j.Budget,
                Status = j.Status,
                CreatedAt = j.CreatedAt,
                PostedByUserId = j.PostedByUserId,
                PostedByName = j.PostedByUser.FullName
            })
            .ToListAsync();

        return Ok(jobs);
    }

    // ======================
    // GET SINGLE JOB BY ID
    // ======================
    [HttpGet("{jobId:int}")]
    public async Task<IActionResult> GetJobById(int jobId)
    {
        var job = await _context.Jobs
            .Include(j => j.PostedByUser)
            .AsNoTracking()
            .Where(j => j.JobId == jobId)
            .Select(j => new JobResponseDto
            {
                JobId = j.JobId,
                Title = j.Title,
                Description = j.Description,
                Category = j.Category,
                Location = j.Location,
                Budget = j.Budget,
                Status = j.Status,
                CreatedAt = j.CreatedAt,
                PostedByUserId = j.PostedByUserId,
                PostedByName = j.PostedByUser.FullName
            })
            .FirstOrDefaultAsync();

        if (job == null)
            return NotFound();

        return Ok(job);
    }

    // ======================
    // TEMP USER ID (REPLACE WITH JWT)
    // ======================
    private int GetUserId()
    {
        if (!Request.Headers.TryGetValue("X-User-Id", out var userId))
            throw new UnauthorizedAccessException("User not authenticated.");

        return int.Parse(userId!);
    }
}
