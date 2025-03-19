﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectManagement.DataAccess.SQLite;

#nullable disable

namespace ProjectManagement.DataAccess.SQLite.Migrations.Migrations
{
    [DbContext(typeof(ProjectManagementDbContext))]
    partial class ProjectManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("EmployeeEntityProjectEntity", b =>
                {
                    b.Property<int>("EmployeesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EmployeesId", "ProjectsId");

                    b.HasIndex("ProjectsId");

                    b.ToTable("EmployeeEntityProjectEntity");
                });

            modelBuilder.Entity("ProjectManagement.DataAccess.SQLite.Models.EmployeeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ProjectManagement.DataAccess.SQLite.Models.ProjectEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomerCompany")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ExecutorCompany")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectManagement.DataAccess.SQLite.Models.TaskEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<int?>("EmployeeEntityId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ExecutorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("EmployeeEntityId");

                    b.HasIndex("ExecutorId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("EmployeeEntityProjectEntity", b =>
                {
                    b.HasOne("ProjectManagement.DataAccess.SQLite.Models.EmployeeEntity", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagement.DataAccess.SQLite.Models.ProjectEntity", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManagement.DataAccess.SQLite.Models.ProjectEntity", b =>
                {
                    b.HasOne("ProjectManagement.DataAccess.SQLite.Models.EmployeeEntity", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("ProjectManagement.DataAccess.SQLite.Models.TaskEntity", b =>
                {
                    b.HasOne("ProjectManagement.DataAccess.SQLite.Models.EmployeeEntity", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagement.DataAccess.SQLite.Models.EmployeeEntity", null)
                        .WithMany("Tasks")
                        .HasForeignKey("EmployeeEntityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ProjectManagement.DataAccess.SQLite.Models.EmployeeEntity", "Executor")
                        .WithMany()
                        .HasForeignKey("ExecutorId");

                    b.HasOne("ProjectManagement.DataAccess.SQLite.Models.ProjectEntity", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Author");

                    b.Navigation("Executor");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectManagement.DataAccess.SQLite.Models.EmployeeEntity", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("ProjectManagement.DataAccess.SQLite.Models.ProjectEntity", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
