using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.SQLite.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.DataAccess.SQLite
{
    static class ModelBuilderConfigurations
    {
        public static void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguretions());
            modelBuilder.ApplyConfiguration(new ProjectConfiguretions());
            modelBuilder.ApplyConfiguration(new TaskConfiguretions());
        }
    }
}
