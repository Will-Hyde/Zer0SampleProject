using Microsoft.EntityFrameworkCore;

namespace Zer0SampleProject
{

    /// <summary>
    /// Object Relationship Mapping using EntityFramework Core. A bit overkill for the purposes of this project, but it should make it very easy to build upon.
    /// </summary>
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(p => p.ProjectId);

                entity.HasMany(u => u.UserProject)
                    .WithOne(p => p.Project)
                    .HasForeignKey(u => u.ProjectId);
                entity.Property(p => p.Format)
                    .HasConversion<int>();
                entity.Property(p => p.Status)
                    .HasConversion<int>();
                entity.Property(p => p.Visibility)
                    .HasConversion<int>();
            });
            modelBuilder.Entity<UserProject>(entity =>
            {
                modelBuilder.Entity<UserProject>().HasKey(u => new { u.ProjectId, u.UserId });

                entity.HasOne(u => u.User)
                    .WithMany(d => d.UserProject)
                    .HasForeignKey(u => u.UserId);

                entity.HasOne(p => p.Project)
                    .WithMany(u => u.UserProject)
                    .HasForeignKey(p => p.ProjectId);
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(p => p.UserId);
                entity.HasMany(u => u.UserProject)
                    .WithOne(d => d.User)
                    .HasForeignKey(u => u.UserId);
            });
        }
    }
}
