using CwEstate.Payroll.Data;
using CwEstate.Payroll.Models;
using Microsoft.EntityFrameworkCore;

namespace CwEstate.Payroll.Services;

public class PayrunService(PayrollDbContext db)
{
    private readonly PayrollDbContext _db = db;

    /// Writes one payslip per active employee for the month, skipping any that exist.
    public async Task<int> RunAsync(int year, int month, CancellationToken ct)
    {
        var employees = await _db.Employees.Where(e => e.Active).ToListAsync(ct);
        var done = await _db.Payslips.Where(p => p.Year == year && p.Month == month).Select(p => p.EmployeeId).ToListAsync(ct);
        var created = 0;
        foreach (var e in employees.Where(e => !done.Contains(e.Id)))
        {
            _db.Payslips.Add(new Payslip { Id = Guid.NewGuid(), EmployeeId = e.Id, Year = year, Month = month, Gross = e.MonthlySalary, Tax = Math.Round(e.MonthlySalary * e.TaxRate, 2) });
            created++;
        }
        await _db.SaveChangesAsync(ct);
        return created;
    }
}
