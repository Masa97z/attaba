using attaba.Models;

public interface IEmployeeService
{
    Task<Employee> GetEmployeeAsync(string id);
    Task CreateEmployeeAsync(Employee employee);
    Task UpdateEmployeeById(string id, Employee employee);
    Task DeleteEmployeeById(string id);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
}

public interface IUserService
{
    Task<User> GetUserAsync(string id);
    Task CreateUserAsync(User user);
    Task UpdateUserById(string id, User user);
    Task DeleteUserById(string id);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> Authenticate(string username, string password);
}

public interface ISectionService
{
    Task<Section> GetSectionAsync(string id);
    Task CreateSectionAsync(Section section);
    Task UpdateSectionById(string id, Section section);
    Task DeleteSectionById(string id);
    Task<IEnumerable<Section>> GetAllSectionsAsync();
}

public interface IDepartmentService
{
    Task<Department> GetDepartmentAsync(string id);
    Task CreateDepartmentAsync(Department department);
    Task UpdateDepartmentById(string id, Department department);
    Task DeleteDepartmentById(string id);
    Task<IEnumerable<Department>> GetAllDepartmentsAsync();
}
