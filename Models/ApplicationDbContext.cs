using Microsoft.EntityFrameworkCore;

namespace StudentPortal.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<Result> Results { get; set; }

        public DbSet<Fee> Fees { get; set; }

        public DbSet<Notice> Notices { get; set; }

        public DbSet<Admin> Admins { get; set; }
    }
}