public interface IUserService
{
    Task<User> GetUserAsync(string id);
    Task CreateUserAsync(User user);
    Task UpdateUserById(string id, User user);
    Task DeleteUserById(string id);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> Authenticate(string username, string password);
}