
public interface IEmployeeService
{

    Task<Employee> GetEmployeeAsync(string id);
    Task CreateEmployeeAsync(Employee employee);
    Task UpdateEmployeeById(string id, Employee employee);
    Task DeleteEmployeeById(string id);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
}





