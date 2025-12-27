using ShramikConnectWebApi.Shared.Enums;

namespace ShramikConnectWebApi.Models.Auth;

public class RegisterRequestDto
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Password { get; set; } = null!;
    public RoleType Role { get; set; }
}