namespace CwEstate.Payroll.Models;

public class Payslip
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal Gross { get; set; }
    public decimal Tax { get; set; }
    public decimal Net => Gross - Tax;
}
