using Microsoft.EntityFrameworkCore;

namespace StudentdetailsCrud.Models
{
    public class StudentDbContext:DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> opt) : base(opt)
        {

        }
        public DbSet<Subject>? Subjects { get; set; }
        public DbSet<Student>? Students { get; set; }
        public DbSet<Addmission>? Addmissions { get; set; }
    }
}
