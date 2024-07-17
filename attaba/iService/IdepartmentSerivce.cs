public interface IDepartmentService
{
    Task<Department> GetDepartmentAsync(string id);
    Task CreateDepartmentAsync(Department department);
    Task UpdateDepartmentById(string id, Department department);
    Task DeleteDepartmentById(string id);
    Task<IEnumerable<Department>> GetAllDepartmentsAsync();
}
