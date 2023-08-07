using WebServerEmployee.BL.Implementations;
using WebServerEmployee.BL.Interfaces;
using WebServerEmployee.DAL.Interfaces;
using WebServerEmployee.DAL.Repositories;
using WebServerEmployee.Domain.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IEmployeeBL, EmployeeBL>();
builder.Services.AddTransient<ICompanyRepository<Company>, CompanyRepository>();
builder.Services.AddTransient<ICompanyBL, CompanyBL>();
builder.Services.AddTransient<IDepartmentRepository<Department>, DepartmentRepository>();
builder.Services.AddTransient<IDepartmentBL, DepartmentBL>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
