using Microsoft.EntityFrameworkCore;

namespace UniversityRegistrar.Models
{
    public class UniversityRegistrarContext : DbContext
    {
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public DbSet<CourseStudent> CourseStudent { get; set; }
        public virtual DbSet<Department> Departments { get; set; }

        public UniversityRegistrarContext(DbContextOptions options) : base(options) {}
    }
}
