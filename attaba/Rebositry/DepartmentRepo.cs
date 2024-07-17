
using MongoDB.Driver;

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
