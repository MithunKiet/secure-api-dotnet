using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecureApiFoundation.Domain.Entities;

namespace SecureApiFoundation.Infrastructure.Database;

public class DbSeeder
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ILogger<DbSeeder> _logger;

    public DbSeeder(AppDbContext context, IPasswordHasher<User> passwordHasher, ILogger<DbSeeder> logger)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        try
        {
            // Ensure database is created
            await _context.Database.MigrateAsync();

            // Seed users if none exist
            if (!await _context.Users.AnyAsync())
            {
                _logger.LogInformation("Seeding initial users...");
                await SeedUsersAsync();
            }

            _logger.LogInformation("Database seeding completed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task SeedUsersAsync()
    {
        var dummyUser = new User();
        var users = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                Email = "admin@secureapi.com",
                Username = "admin",
                PasswordHash = _passwordHasher.HashPassword(dummyUser, "Admin@123"),
                FirstName = "Admin",
                LastName = "User",
                IsActive = true,
                IsEmailVerified = true,
                FailedLoginAttempts = 0,
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = Guid.NewGuid(),
                Email = "john.doe@example.com",
                Username = "johndoe",
                PasswordHash = _passwordHasher.HashPassword(dummyUser, "User@123"),
                FirstName = "John",
                LastName = "Doe",
                IsActive = true,
                IsEmailVerified = true,
                FailedLoginAttempts = 0,
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = Guid.NewGuid(),
                Email = "jane.smith@example.com",
                Username = "janesmith",
                PasswordHash = _passwordHasher.HashPassword(dummyUser, "User@123"),
                FirstName = "Jane",
                LastName = "Smith",
                IsActive = true,
                IsEmailVerified = true,
                FailedLoginAttempts = 0,
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                Username = "testuser",
                PasswordHash = _passwordHasher.HashPassword(dummyUser, "Test@123"),
                FirstName = "Test",
                LastName = "User",
                IsActive = true,
                IsEmailVerified = false,
                FailedLoginAttempts = 0,
                CreatedAt = DateTime.UtcNow
            }
        };

        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Seeded {Count} users", users.Count);
    }
}
