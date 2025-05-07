using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;

namespace PlanszowkaPlusPlus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordHasher<Admin> _passwordHasher;
        public AdminsController(AppDbContext appDbContext, IPasswordHasher<Admin> passwordHasher)
        {
            _appDbContext = appDbContext;
            _passwordHasher = passwordHasher;
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(Admin admin)
        {
            admin.PasswordHash = _passwordHasher.HashPassword(admin, admin.PasswordHash);

            _appDbContext.Admins.Add(admin);
            await _appDbContext.SaveChangesAsync();

            return Ok(admin);
        }

        [HttpGet]
        public async Task<IActionResult> ViewAdmins()
        {
            var admins = await _appDbContext.Admins.ToListAsync();
            return Ok(admins);
        }
    }
}
