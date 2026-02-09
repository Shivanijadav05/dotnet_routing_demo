using Microsoft.EntityFrameworkCore;

namespace MyWebApi.Database
{
    public class CollegeDBContext:DbContext
    {

        public CollegeDBContext(DbContextOptions<CollegeDBContext> options)
                : base(options)
            {
            }

        DbSet<Student> Students{get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new List<Student>()
            {
                new Student
                {
                    Id=1,
                    StudentName="Name1",
                    Email="name1@gmail.com",
                    Address="abc"
                },
                  new Student
                {
                    Id=2,
                    StudentName="Name2",
                    Email="name2@gmail.com",
                    Address="def"

                }
            });
        }
    }
}