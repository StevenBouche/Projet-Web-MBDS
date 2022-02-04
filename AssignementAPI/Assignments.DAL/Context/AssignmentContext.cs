using Assignments.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignments.DAL.Context
{
    public class AssignmentContext : DbContext
    {
        public AssignmentContext(DbContextOptions<AssignmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserEntity> Users => Set<UserEntity>();
        public virtual DbSet<CourseEntity> Courses => Set<CourseEntity>();
        public virtual DbSet<CourseImageEntity> CourseImages => Set<CourseImageEntity>();
        public virtual DbSet<UserProfilImageEntity> UserProfilImages => Set<UserProfilImageEntity>();
        public virtual DbSet<AssignmentEntity> Assignments => Set<AssignmentEntity>();
        public virtual DbSet<RefreshTokenEntity> RefreshTokens => Set<RefreshTokenEntity>();
        public virtual DbSet<WorkSubmitEntity> WorkSubmits => Set<WorkSubmitEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder
                .Entity<UserEntity>()
                .Property(e => e.Role)
                .HasConversion(v => v.ToString(), v => (UserRoles)Enum.Parse(typeof(UserRoles), v));*/

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Password).HasMaxLength(255);

                entity.HasIndex(e => e.Name).IsUnique();

                entity.HasOne(e => e.Image)
                    .WithOne(p => p.User)
                    .HasForeignKey<UserProfilImageEntity>(b => b.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.RefreshToken)
                    .WithOne(p => p.User)
                    .HasForeignKey<RefreshTokenEntity>(b => b.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RefreshTokenEntity>(entity =>
            {
                entity.HasIndex(e => e.Token).IsUnique();
                entity.HasIndex(e => e.UserId).IsUnique();
            });

            modelBuilder.Entity<CourseEntity>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(e => e.Image)
                    .WithOne(p => p.Course)
                    .HasForeignKey<CourseImageEntity>(b => b.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.User)
                    .WithMany(a => a.Courses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AssignmentEntity>(entity =>
            {
                entity.HasOne(d => d.Course)
                    .WithMany(a => a.Assignments)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<WorkSubmitEntity>(entity =>
            {
                entity.HasOne(d => d.Assignment)
                    .WithMany(a => a.WorkSubmits)
                    .HasForeignKey(d => d.AssignmentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                entity.HasOne(d => d.User)
                    .WithMany(a => a.WorkSubmits)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => new { e.UserId, e.AssignmentId }).IsUnique();
            });

            modelBuilder.Entity<CourseImageEntity>(entity =>
            {
                entity.HasOne(e => e.Course)
                    .WithOne(p => p.Image)
                    .HasForeignKey<CourseEntity>(b => b.ImageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);
            });

            modelBuilder.Entity<UserProfilImageEntity>(entity =>
            {
                /*entity.HasOne(e => e.User)
                    .WithOne(p => p.Image)
                    .HasForeignKey<UserEntity>(b => b.ImageId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);*/
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditOnEntries();
            return base.SaveChangesAsync(cancellationToken);
        }

        internal void SetAuditOnEntries()
        {
            // AutoDetectChanges is disabled so you need to update the tracker
            ChangeTracker.DetectChanges();
            var entries = ChangeTracker.Entries()
                .Where(t => t.Entity is BaseModel && (t.State == EntityState.Added || t.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var element = (BaseModel)entry.Entity;

                element.UpdatedDate = DateTimeOffset.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    element.CreatedDate = DateTimeOffset.UtcNow;
                };
            }
        }
    }
}