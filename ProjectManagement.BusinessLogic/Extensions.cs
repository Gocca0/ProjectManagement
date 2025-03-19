using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.BusinessLogic.Abstraction;
using ProjectManagement.BusinessLogic.Services;
using ProjectManagement.DataAccess.SQLite;

namespace ProjectManagement.BusinessLogic
{
    static public class Extensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IEmployeesService, EmployeesService>();
            serviceCollection.AddScoped<IProjectsService, ProjectsService>();
            serviceCollection.AddScoped<ITasksService, TasksService>();
            serviceCollection.AddDataAccess();

            return serviceCollection;
        }

    }
}
