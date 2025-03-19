using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.DataAccess.SQLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.DataAccess.SQLite.Configurations
{
    public class EmployeeConfiguretions : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder
                .HasMany(e => e.Projects)
                .WithMany(p => p.Employees);

            builder
                .HasMany(e => e.Tasks)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);


        }
        
    }
}
