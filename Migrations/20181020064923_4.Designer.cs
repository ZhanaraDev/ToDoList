﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApplication1.Models;

namespace WebApplication1.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20181020064923_4")]
    partial class _4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("WebApplication1.Models.Task", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("TaskCategoryID");

                    b.Property<string>("TaskDesription");

                    b.Property<string>("TaskName");

                    b.HasKey("Id");

                    b.HasIndex("TaskCategoryID");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("WebApplication1.Models.TaskCategory", b =>
                {
                    b.Property<long>("TaskCategoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName");

                    b.HasKey("TaskCategoryID");

                    b.ToTable("TaskCategory");
                });

            modelBuilder.Entity("WebApplication1.Models.UserProfile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("Role");

                    b.Property<string>("Surname");

                    b.Property<long>("UserRef");

                    b.HasKey("Id");

                    b.HasIndex("UserRef")
                        .IsUnique();

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("WebApplication1.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("WebApplication1.Models.Task", b =>
                {
                    b.HasOne("WebApplication1.Models.TaskCategory", "TaskCategory")
                        .WithMany("Tasks")
                        .HasForeignKey("TaskCategoryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication1.Models.UserProfile", b =>
                {
                    b.HasOne("WebApplication1.User", "user")
                        .WithOne("profile")
                        .HasForeignKey("WebApplication1.Models.UserProfile", "UserRef")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
