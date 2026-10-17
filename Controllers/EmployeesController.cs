using CwEstate.Payroll.Data;
using CwEstate.Payroll.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CwEstate.Payroll.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(PayrollDbContext db) : ControllerBase
{
    private readonly PayrollDbContext _db = db;

    [HttpGet]
    public async Task<List<Employee>> List(CancellationToken ct) =>
        await _db.Employees.AsNoTracking().OrderBy(e => e.Name).ToListAsync(ct);

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Employee>> Get(Guid id, CancellationToken ct)
    {
        var e = await _db.Employees.FindAsync(new object[] { id }, ct);
        return e is null ? NotFound() : e;
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> Create(Employee employee, CancellationToken ct)
    {
        employee.Id = Guid.NewGuid();
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
    }
}
