using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ControlOctoberTechnologyUniversitySystem.Models
{
    public class ControlDbContext: IdentityDbContext<ApplicationUser>
    {
        public ControlDbContext(DbContextOptions<ControlDbContext> options) :base(options)
        { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuring the composite primary key for StudentSubject
            modelBuilder.Entity<StudentSubject>()
                .HasKey(ss => new { ss.StudentId, ss.SubjectId });

            // Configuring the relationship between Subject and StudentSubject
            

            modelBuilder.Entity<Student>()
            .HasOne<Department>()  // Use HasOne without navigation property
            .WithMany(s => s.Students)           // Use WithMany without navigation property
            .HasForeignKey(s => s.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull); // Set delete behavior to SetNull


            modelBuilder.Entity<StudentImage>()
            .HasOne<Student>()
            .WithMany(s => s.StudentImages)
            .HasForeignKey(si => si.StudentId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false); // Ensure the foreign key is not required
        }
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedDate = now;
                }
                ((BaseEntity)entity.Entity).UpdatedDate = now;
            }
        }
    }
}
