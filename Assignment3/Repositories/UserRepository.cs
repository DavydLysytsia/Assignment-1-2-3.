using Assignment3.Models;

namespace Assignment3.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<AppUser> _users = new()
    {
        new AppUser { Id = 1, Username = "admin", Email = "admin@lms.com", PasswordHash = "admin123" }
    };

    private static int _nextId = 2;

    public AppUser? GetByUsername(string username) =>
        _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

    public bool UsernameExists(string username) =>
        _users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

    public AppUser Register(RegisterViewModel model)
    {
        var user = new AppUser
        {
            Id           = _nextId++,
            Username     = model.Username,
            Email        = model.Email,
            PasswordHash = model.Password
        };
        _users.Add(user);
        return user;
    }

    public AppUser? Authenticate(string username, string password)
    {
        var user = GetByUsername(username);
        if (user == null) return null;
        return user.PasswordHash == password ? user : null;
    }
}
