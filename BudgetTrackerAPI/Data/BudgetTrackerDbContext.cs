using BudgetTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetTrackerAPI.Data
{
    public class BudgetTrackerDbContext : DbContext
    {
        public BudgetTrackerDbContext(DbContextOptions<BudgetTrackerDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
    }
}
