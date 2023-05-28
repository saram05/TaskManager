using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using TaskApp.DAL.Entities;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL
{
    /// <summary>
    /// Maps the entities in the data base
    /// </summary>
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DbSet<DAL.Entities.Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ListTask> ListTasks { get; set; }
        public DbSet<DAL.Entities.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DAL.Entities.Group>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(c => c.UserName).IsUnique();
            modelBuilder.Entity<Project>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<DAL.Entities.Task>().HasIndex(c => c.Description).IsUnique();
            //composite index (the name of a task is not repeated in the same project)
            modelBuilder.Entity<ListTask>().HasIndex("Name", "IdProyecto").IsUnique();

        }
    }
}
