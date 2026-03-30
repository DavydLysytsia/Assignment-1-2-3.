using Assignment3.Models;

namespace Assignment3.Repositories;

public interface IUserRepository
{
    AppUser? GetByUsername(string username);
    bool UsernameExists(string username);
    AppUser Register(RegisterViewModel model);
    AppUser? Authenticate(string username, string password);
}
