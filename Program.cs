using CwEstate.Payroll.Data;
using CwEstate.Payroll.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<PayrollDbContext>(o =>
    o.UseSqlite(builder.Configuration.GetConnectionString("Payroll")));
builder.Services.AddScoped<PayrunService>();

var app = builder.Build();
app.MapControllers();
app.Run();
