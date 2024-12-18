using Microsoft.EntityFrameworkCore;
using PlanszowkaPlusPlus.Models;

namespace PlanszowkaPlusPlus.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<GameTable> GameTables { get; set; }
    }
}
