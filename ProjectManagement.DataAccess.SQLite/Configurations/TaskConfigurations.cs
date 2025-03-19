using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.SQLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.DataAccess.SQLite.Configurations
{
    public class TaskConfiguretions : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder
                .HasOne(t => t.Author)
                .WithMany()
                .HasForeignKey(t => t.AuthorId);

            builder
                .HasOne(t => t.Executor)
                .WithMany()
                .HasForeignKey(t => t.ExecutorId);

            builder
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
