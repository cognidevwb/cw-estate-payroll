using CwEstate.Payroll.Models;
using Microsoft.EntityFrameworkCore;

namespace CwEstate.Payroll.Data;

public class PayrollDbContext(DbContextOptions<PayrollDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Payslip> Payslips => Set<Payslip>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Payslip>().HasIndex(p => new { p.EmployeeId, p.Year, p.Month }).IsUnique();
    }
}
