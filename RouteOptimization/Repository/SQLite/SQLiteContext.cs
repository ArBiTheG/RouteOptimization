using Microsoft.EntityFrameworkCore;
using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteContext: DbContext
    {
        public DbSet<IOffice> Offices { get; set; } = null!;
        public DbSet<IRoute> Routes { get; set; } = null!;
        public DbSet<IItem> Items { get; set; } = null!;
        public DbSet<ICar> Cars { get; set; } = null!;
        public SQLiteContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Schedule.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
