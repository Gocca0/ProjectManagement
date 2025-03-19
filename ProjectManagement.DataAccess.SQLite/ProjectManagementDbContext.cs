using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.SQLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.DataAccess.SQLite
{
    public class ProjectManagementDbContext : DbContext
    {

        public ProjectManagementDbContext() { }



        public ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options) 
            : base(options)
        {
        
        }

        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "DB", "ProjectManagement.db");
            var connectionString = $"Data Source={dbPath}";
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(connectionString);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelBuilderConfigurations.ApplyConfigurations(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }


    }
}
