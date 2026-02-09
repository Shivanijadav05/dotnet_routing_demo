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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
          modelBuilder.ApplyConfiguration(new StudentConfig());


        }
    }
}