using Microsoft.EntityFrameworkCore;
using test.Models.Entities;

namespace test.Models.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        #region DbSet

        public DbSet<Teacher> Teachers  { get; set; }
        public DbSet<Subjects> Subjects  { get; set; }
        public DbSet<TeacherSubjects> TeacherSubjects  { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
