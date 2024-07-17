using MongoDB.Driver;

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

