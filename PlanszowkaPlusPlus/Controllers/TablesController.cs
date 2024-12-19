using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanszowkaPlusPlus.Models;
using PlanszowkaPlusPlus.Data;
using Microsoft.EntityFrameworkCore;

namespace PlanszowkaPlusPlus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public TablesController(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddTable(GameTable table)
        {
            _appDbContext.GameTables.Add(table);
            await _appDbContext.SaveChangesAsync();

            return Ok(table);
        }
        [HttpGet]
        public async Task<IActionResult> ViewTables()
        {
            var tables = await _appDbContext.GameTables.ToListAsync();
            return Ok(tables);
        }
    }
}
