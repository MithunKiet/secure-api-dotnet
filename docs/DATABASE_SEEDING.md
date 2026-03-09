# Database Seeding

This project includes automatic database seeding for development purposes.

## Seeded Test Users

The following test users are automatically created when running the application in **Development** mode:

### 1. Admin User
- **Email**: `admin@secureapi.com`
- **Username**: `admin`
- **Password**: `Admin@123`
- **Status**: Active, Email Verified
- **Purpose**: Administrative testing

### 2. John Doe
- **Email**: `john.doe@example.com`
- **Username**: `johndoe`
- **Password**: `User@123`
- **Status**: Active, Email Verified
- **Purpose**: Regular user testing

### 3. Jane Smith
- **Email**: `jane.smith@example.com`
- **Username**: `janesmith`
- **Password**: `User@123`
- **Status**: Active, Email Verified
- **Purpose**: Regular user testing

### 4. Test User
- **Email**: `test@example.com`
- **Username**: `testuser`
- **Password**: `Test@123`
- **Status**: Active, Email NOT Verified
- **Purpose**: Testing email verification flows

## How Seeding Works

1. **Automatic**: Seeding runs automatically when the application starts in Development mode
2. **Safe**: Only seeds if the Users table is empty (prevents duplicates)
3. **Logged**: All seeding operations are logged via Serilog
4. **Location**: See `src/Infrastructure/Database/DbSeeder.cs`

## Customizing Seed Data

To add or modify seed data:

1. Open `src/Infrastructure/Database/DbSeeder.cs`
2. Modify the `SeedUsersAsync()` method
3. Add new seeding methods for other entities
4. Call them from the `SeedAsync()` method

## Disabling Seeding

To disable automatic seeding, comment out or remove these lines from `Program.cs`:

```csharp
if (app.Environment.IsDevelopment())
{
    await app.SeedDatabaseAsync();
}
```

## Manual Seeding

You can also manually trigger seeding by calling:

```csharp
using var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
await seeder.SeedAsync();
```

## Testing Authentication

Use any of the seeded users to test authentication:

### Example Login Request:
```json
POST /api/auth/login
Content-Type: application/json

{
  "email": "admin@secureapi.com",
  "password": "Admin@123"
}
```

Or using username:
```json
{
  "username": "admin",
  "password": "Admin@123"
}
```

## Production Considerations

⚠️ **Important**: 
- Seeding only runs in Development mode by default
- Never use these test credentials in production
- Remove or secure seed data before deploying to production
- Consider using environment-specific seeding strategies for staging/production
