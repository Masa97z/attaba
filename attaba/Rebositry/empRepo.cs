using MongoDB.Driver;
using attaba.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EmployeeService : IEmployeeService
{
    private readonly IMongoCollection<Employee> _employees;

    public EmployeeService(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("CompanyDB");
        _employees = database.GetCollection<Employee>("Employees");
    }

    public async Task<Employee> GetEmployeeAsync(string id)
    {
        return await _employees.Find(e => e.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateEmployeeAsync(Employee employee)
    {
        await _employees.InsertOneAsync(employee);
    }

    public async Task UpdateEmployeeById(string id, Employee employee)
    {
        await _employees.ReplaceOneAsync(e => e.Id == id, employee);
    }

    public async Task DeleteEmployeeById(string id)
    {
        await _employees.DeleteOneAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _employees.Find(e => true).ToListAsync();
    }
}

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
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);  // Hash the password
        await _users.InsertOneAsync(user);
    }

    public async Task UpdateUserById(string id, User user)
    {
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);  // Hash the password
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

public class SectionService : ISectionService
{
    private readonly IMongoCollection<Section> _sections;

    public SectionService(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("CompanyDB");
        _sections = database.GetCollection<Section>("Sections");
    }

    public async Task<Section> GetSectionAsync(string id)
    {
        return await _sections.Find(s => s.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateSectionAsync(Section section)
    {
        await _sections.InsertOneAsync(section);
    }

    public async Task UpdateSectionById(string id, Section section)
    {
        await _sections.ReplaceOneAsync(s => s.Id == id, section);
    }

    public async Task DeleteSectionById(string id)
    {
        await _sections.DeleteOneAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Section>> GetAllSectionsAsync()
    {
        return await _sections.Find(s => true).ToListAsync();
    }
}

public class DepartmentService : IDepartmentService
{
    private readonly IMongoCollection<Department> _departments;

    public DepartmentService(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("CompanyDB");
        _departments = database.GetCollection<Department>("Departments");
    }

    public async Task<Department> GetDepartmentAsync(string id)
    {
        return await _departments.Find(d => d.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateDepartmentAsync(Department department)
    {
        await _departments.InsertOneAsync(department);
    }

    public async Task UpdateDepartmentById(string id, Department department)
    {
        await _departments.ReplaceOneAsync(d => d.Id == id, department);
    }

    public async Task DeleteDepartmentById(string id)
    {
        await _departments.DeleteOneAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
    {
        return await _departments.Find(d => true).ToListAsync();
    }
}
