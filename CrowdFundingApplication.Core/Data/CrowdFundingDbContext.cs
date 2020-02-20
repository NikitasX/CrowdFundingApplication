using System;
using System.Collections.Generic;
using System.Text;
using CrowdFundingApplication.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CrowdFundingApplication.Core.Data
{
    public class CrowdFundingDbContext : DbContext
    {
        private readonly string connectionString_;

        public CrowdFundingDbContext()
        {
            connectionString_ = "Server=localhost;Database=crowdfundingplatform;User id=sa;Password=QWE123!@#";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /// <summary>
            /// Create user table, make sure required properties are there
            /// </summary>
            modelBuilder
                .Entity<User>()
                .ToTable("User");

            modelBuilder
                .Entity<User>()
                .Property(u => u.UserEmail)
                .IsRequired();
            
            modelBuilder
                .Entity<User>()
                .HasIndex(u => u.UserEmail)
                .IsUnique();

            modelBuilder
                .Entity<User>()
                .Property(u => u.UserLastName)
                .IsRequired();

            modelBuilder
                .Entity<Project>()
                .ToTable("Project");
            
            modelBuilder
                .Entity<Project>()
                .HasKey(op => new { op.ProjectId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString_);
        }
    }
}
