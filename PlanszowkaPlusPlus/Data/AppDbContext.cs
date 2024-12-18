using Microsoft.EntityFrameworkCore;
using PlanszówkaPlusplus.Models;

namespace PlanszowkaPlusPlus.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<GameTable> GameTables { get; set; }
    }
}
