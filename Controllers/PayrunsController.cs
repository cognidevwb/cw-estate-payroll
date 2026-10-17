using CwEstate.Payroll.Data;
using CwEstate.Payroll.Models;
using CwEstate.Payroll.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CwEstate.Payroll.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PayrunsController(PayrollDbContext db, PayrunService payrun) : ControllerBase
{
    private readonly PayrollDbContext _db = db;
    private readonly PayrunService _payrun = payrun;

    [HttpPost("{year:int}/{month:int}")]
    public async Task<ActionResult<int>> Run(int year, int month, CancellationToken ct) =>
        await _payrun.RunAsync(year, month, ct);

    [HttpGet("{year:int}/{month:int}")]
    public async Task<List<Payslip>> Payslips(int year, int month, CancellationToken ct) =>
        await _db.Payslips.AsNoTracking().Where(p => p.Year == year && p.Month == month).ToListAsync(ct);
}
