
using MongoDB.Driver;

public class UserService : IUserService
{
    private readonly IMongoCollection<User> _users;

    public UserService(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("CompanyDB");
        _users = database.GetCollection<User>("Users");
    }

    public async Task<User> GetUserAsync(string id)
    {
        return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateUserAsync(User user)
    {
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);  
        await _users.InsertOneAsync(user);
    }

    public async Task UpdateUserById(string id, User user)
    {
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash); 
        await _users.ReplaceOneAsync(u => u.Id == id, user);
    }

    public async Task DeleteUserById(string id)
    {
        await _users.DeleteOneAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _users.Find(u => true).ToListAsync();
    }

    public async Task<User> Authenticate(string username, string password)
    {
        var user = await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return null;
        }
        return user;
    }
}
