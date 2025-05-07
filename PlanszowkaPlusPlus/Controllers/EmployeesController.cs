using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;

namespace PlanszowkaPlusPlus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordHasher<Employee> _passwordHasher;

        public EmployeesController(AppDbContext appDbContext, IPasswordHasher<Employee> passwordHasher)
        {
            _appDbContext = appDbContext;
            _passwordHasher = passwordHasher;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            //hash the employee's password
            employee.PasswordHash = _passwordHasher.HashPassword(employee, employee.PasswordHash);

            _appDbContext.Employees.Add(employee);
            await _appDbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpGet]
        public async Task<IActionResult> ViewEmployees()
        {
            var employees = await _appDbContext.Employees.ToListAsync();
            return Ok(employees);
        }
    }
}
