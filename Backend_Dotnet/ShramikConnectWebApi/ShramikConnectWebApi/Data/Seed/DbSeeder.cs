using Microsoft.EntityFrameworkCore;
using ShramikConnectWebApi.Models;
using ShramikConnectWebApi.Shared.Enums;

namespace ShramikConnectWebApi.Data.Seed;

public static class DbSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        SeedRoles(modelBuilder);
        // SeedAdminUser(modelBuilder);
    }

    // ======================
    // ROLES
    // ======================
    private static void SeedRoles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = 1, RoleName = "Admin" },
            new Role { RoleId = 2, RoleName = "Worker" },
            new Role { RoleId = 3, RoleName = "Client" },
            new Role { RoleId = 4, RoleName = "Organization" },
            new Role { RoleId = 5, RoleName = "Supervisor" }
        );
    }

    // ======================
    // ADMIN USER
    // ======================
    // private static void SeedAdminUser(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<User>().HasData(
    //         new User
    //         {
    //             UserId = 1,
    //             FullName = "System Administrator",
    //             Email = "admin@shramikconnect.com",
    //             Phone = "9999999999",
    //             PasswordHash = "AQAAAAIAAYagAAAAEL+3Zz5vHjM6vJ3T1Z9h3u2jQbq8Jk0K1N0Y1e3WfM7ZKJ1rA==",
    //             RoleId = 1, // Admin
    //             IsActive = true,
    //             CreatedAt = new DateTime(2025, 1, 1)
    //         }
    //     );
    // }
}