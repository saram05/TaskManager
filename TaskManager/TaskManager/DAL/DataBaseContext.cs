using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using TaskApp.DAL.Entities;

namespace TaskManager.DAL
{
    /// <summary>
    /// Esta clase se utiliza para mapear las entidades (tablas) en la base de datos 
    /// </summary>
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DbSet<DAL.Entities.Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DAL.Entities.Group>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(c => c.UserName).IsUnique();
        }
    }
}
