using Microsoft.EntityFrameworkCore;
using PlanszowkaPlusPlus.Models;

namespace PlanszowkaPlusPlus.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<GameTable> GameTables { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Rent> Rentals { get; set; } 
        public DbSet<Admin> Admins { get; set; }

    }
}
