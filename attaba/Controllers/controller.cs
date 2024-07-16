using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using attaba.Models;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]  

public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployeeById(string id)
    {
        var employee = await _employeeService.GetEmployeeAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }



    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
    {
        if (employee == null)
        {
            return BadRequest("Employee is null");
        }

        await _employeeService.CreateEmployeeAsync(employee);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateEmployee(string id, Employee employee)
    {
        if (employee == null || id != employee.Id)
        {
            return BadRequest("Employee ID mismatch");
        }

        var existingEmployee = await _employeeService.GetEmployeeAsync(id);
        if (existingEmployee == null)
        {
            return NotFound();
        }

        await _employeeService.UpdateEmployeeById(id, employee);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEmployee(string id)
    {
        var employee = await _employeeService.GetEmployeeAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        await _employeeService.DeleteEmployeeById(id);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();
        return Ok(employees);
    }


}


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(string id)
    {
        var user = await _userService.GetUserAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        if (user == null)
        {
            return BadRequest("User is null");
        }

        await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(string id, User user)
    {
        if (user == null || id != user.Id)
        {
            return BadRequest("User ID mismatch");
        }

        var existingUser = await _userService.GetUserAsync(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        await _userService.UpdateUserById(id, user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(string id)
    {
        var user = await _userService.GetUserAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        await _userService.DeleteUserById(id);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }
}
