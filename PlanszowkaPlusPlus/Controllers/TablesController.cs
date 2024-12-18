using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanszowkaPlusPlus.Models;
using PlanszowkaPlusPlus.Data;

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
        public async Task<IActionResult> AddTable (GameTable table)
        {
            _appDbContext.GameTables.Add(table);
            await _appDbContext.SaveChangesAsync();

            return Ok(table);
        }
    }
}
