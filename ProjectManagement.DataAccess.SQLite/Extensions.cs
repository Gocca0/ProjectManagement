using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.DataAccess.SQLite.Abstraction;
using ProjectManagement.DataAccess.SQLite.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.DataAccess.SQLite
{
    public static class Extensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection)
        {
            var dbPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "DB", "ProjectManagement.db");
            var connectionString = $"Data Source={dbPath}";
            serviceCollection.AddScoped<IEmployeesRepository, EmployeesRepository>();
            serviceCollection.AddScoped<IProjectsRepository, ProjectsRepository>();
            serviceCollection.AddScoped<ITasksRepository, TasksRepository>();
            serviceCollection.AddDbContext<ProjectManagementDbContext>(
                options =>
                {
                    options.UseSqlite(
                        connectionString,
                        m => m.MigrationsAssembly("ProjectManagement.DataAccess.SQLite.Migrations"));
                });

            return serviceCollection;
        }
    }
}
