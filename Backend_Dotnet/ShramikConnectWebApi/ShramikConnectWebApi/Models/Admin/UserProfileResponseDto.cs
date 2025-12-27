namespace ShramikConnectWebApi.Models.Admin;

public class UserProfileResponseDto
{
    public int UserId { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Role { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    public WorkerProfileViewDto? WorkerProfile { get; set; }
    public ClientProfileViewDto? ClientProfile { get; set; }
    public OrganizationProfileViewDto? OrganizationProfile { get; set; }
}