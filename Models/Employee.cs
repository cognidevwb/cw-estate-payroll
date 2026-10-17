namespace CwEstate.Payroll.Models;

public class Employee
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public decimal MonthlySalary { get; set; }
    public decimal TaxRate { get; set; } = 0.2m;
    public bool Active { get; set; } = true;
}
