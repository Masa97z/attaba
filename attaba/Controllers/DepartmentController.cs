using Microsoft.AspNetCore.Mvc;
using attaba.Models;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetDepartmentById(string id)
    {
        var department = await _departmentService.GetDepartmentAsync(id);
        if (department == null)
        {
            return NotFound();
        }
        return Ok(department);
    }

    [HttpPost]
    public async Task<ActionResult<Department>> CreateDepartment(Department department)
    {
        if (department == null)
        {
            return BadRequest("Department is null");
        }

        await _departmentService.CreateDepartmentAsync(department);
        return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Id }, department);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateDepartment(string id, Department department)
    {
        if (department == null || id != department.Id)
        {
            return BadRequest("Department ID mismatch");
        }

        var existingDepartment = await _departmentService.GetDepartmentAsync(id);
        if (existingDepartment == null)
        {
            return NotFound();
        }

        await _departmentService.UpdateDepartmentById(id, department);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDepartment(string id)
    {
        var department = await _departmentService.GetDepartmentAsync(id);
        if (department == null)
        {
            return NotFound();
        }

        await _departmentService.DeleteDepartmentById(id);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments()
    {
        var departments = await _departmentService.GetAllDepartmentsAsync();
        return Ok(departments);
    }
}
