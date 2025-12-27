using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShramikConnectWebApi.Data;
using ShramikConnectWebApi.Models;
using ShramikConnectWebApi.Models.Auth;
using ShramikConnectWebApi.Shared.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ShramikConnectWebApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _configuration;

    
    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
        _passwordHasher = new PasswordHasher<User>();
    }
    
    
    // for register
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            return BadRequest("Email already registered.");

        if (request.Role == RoleType.Admin || request.Role == RoleType.Supervisor)
            return BadRequest("Cannot self-register as Admin or Supervisor.");

        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            Phone = request.Phone,
            RoleId = (int)request.Role,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // CREATE DEFAULT PROFILE (ONCE)
        await CreateDefaultProfileAsync(user);

        return Ok(new
        {
            user.UserId,
            user.Email,
            Role = request.Role.ToString(),
            ProfileCompleted = false
        });
    }

    
    
    private async Task CreateDefaultProfileAsync(User user)
    {
        switch ((RoleType)user.RoleId)
        {
            case RoleType.Worker:
                _context.Workers.Add(new Worker
                {
                    UserId = user.UserId,
                    SkillSet = SkillSet.Other,
                    ExperienceYears = 0,
                    Location = "Not set",
                    Rating = 0
                });
                break;

            case RoleType.Client:
                _context.Clients.Add(new Client
                {
                    UserId = user.UserId,
                    Address = "Not set"
                });
                break;

            case RoleType.Organization:
                _context.Organizations.Add(new Organization
                {
                    UserId = user.UserId,
                    OrgName = "Not set",
                    GSTNumber = "Not set",
                    Address = "Not set"
                });
                break;
        }

        await _context.SaveChangesAsync();
    }
    
    // for login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || !user.IsActive)
            return Unauthorized("Invalid credentials.");

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password
        );

        if (result == PasswordVerificationResult.Failed)
            return Unauthorized("Invalid credentials.");

        // Generate JWT
        var token = GenerateJwtToken(user);

        return Ok(new LoginResponseDto
        {
            Token = token.Token,
            ExpiresAt = token.ExpiresAt,
            UserId = user.UserId,
            Email = user.Email,
            Role = user.Role.RoleName
        });
    }

    
    private (string Token, DateTime ExpiresAt) GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.Role.RoleName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddMinutes(
            int.Parse(jwtSettings["ExpiryMinutes"]!)
        );

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return (
            new JwtSecurityTokenHandler().WriteToken(token),
            expires
        );
    }


}