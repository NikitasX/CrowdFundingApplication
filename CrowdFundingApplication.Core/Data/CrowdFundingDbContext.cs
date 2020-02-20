using Microsoft.EntityFrameworkCore;
using CrowdFundingApplication.Core.Model;

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
            /// Create User table, make sure required properties are there
            /// </summary>
            modelBuilder
                .Entity<User>()
                .ToTable("User");

            modelBuilder
                .Entity<User>()
                .Property(u => u.UserEmail)
                .IsRequired()
                .HasMaxLength(255);
            
            modelBuilder
                .Entity<User>()
                .HasIndex(u => u.UserEmail)
                .IsUnique();

            modelBuilder
                .Entity<User>()
                .Property(u => u.UserLastName)
                .IsRequired()
                .HasMaxLength(255);

            /// <summary>
            /// Create Project table, make sure required properties are there
            /// </summary>
            modelBuilder
                .Entity<Project>()
                .ToTable("Project");

            modelBuilder
                .Entity<Project>()
                .Property(p => p.ProjectTitle)
                .IsRequired()
                .HasMaxLength(255);            
            
            modelBuilder
                .Entity<Project>()
                .Property(p => p.ProjectFinancialGoal)
                .IsRequired()
                .HasMaxLength(20);

            /// <summary>
            /// Create Media table
            /// </summary>
            modelBuilder
                .Entity<Media>()
                .ToTable("Media");            
            
            modelBuilder
                .Entity<Media>()
                .Property(m => m.MediaURL)
                .IsRequired();

            /// <summary>
            /// Create Incentive table, make sure required properties are there
            /// </summary>
            modelBuilder
                .Entity<Incentive>()
                .ToTable("Incentive");

            modelBuilder
                .Entity<Incentive>()
                .Property(p => p.IncentiveTitle)
                .IsRequired()
                .HasMaxLength(255);            
            
            modelBuilder
                .Entity<Incentive>()
                .Property(i => i.IncentivePrice)
                .IsRequired()
                .HasMaxLength(20);

            /// <summary>
            /// Create BackedIncentives table where the connection between
            /// user id and incentive id is made
            /// </summary>
            modelBuilder
                .Entity<BackedIncentives>()
                .ToTable("BackedIncentives");

            modelBuilder
                .Entity<BackedIncentives>()
                .HasKey(key => new { key.UserId, key.IncentiveId });

            /// <summary>
            /// Create Post table, make sure required properties are there
            /// </summary>
            modelBuilder
                .Entity<Post>()
                .ToTable("Post");

            modelBuilder
                .Entity<Post>()
                .Property(p => p.PostTitle)
                .IsRequired()
                .HasMaxLength(255);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString_);
        }
    }
}
