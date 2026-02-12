using Microsoft.EntityFrameworkCore;
using MyWebApi.Database.Config;

namespace MyWebApi.Database
{
    public class CollegeDBContext:DbContext
    {

        public CollegeDBContext(DbContextOptions<CollegeDBContext> options)
                : base(options)
            {
            }

       public DbSet<Student> Students{get;set;}
       public DbSet<Department> Departments {get;set;}
        public DbSet<Course> Courses { get; set; }
        // public DbSet<StudentCourse> StudentCourses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
          modelBuilder.ApplyConfiguration(new StudentConfig());
          modelBuilder.ApplyConfiguration(new DepartmentConfig());
         modelBuilder.ApplyConfiguration(new CourseConfig());
        //  modelBuilder.ApplyConfiguration(new StudentCourseConfig());
        


        }
    }
}