using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyWebApi.Database.Config
{
    public class CourseConfig:IEntityTypeConfiguration<Course>
    {
            public void Configure(EntityTypeBuilder<Course> builder)
            {
                builder.ToTable("Courses");
                builder.HasKey(x=>x.Id);
                builder.Property(x=>x.Id).UseIdentityColumn();

                    builder.Property(x => x.CourseName)
                        .IsRequired()
                        .HasMaxLength(100);

               
                builder.HasData(new List<Course>(
                    new List<Course>()
                        {
                            new Course
                            {
                                Id=1,
                                CourseName="CourseName1",
                            
                            },
                             new Course
                            {
                                Id=2,
                                CourseName="CourseName2",
                            
                            }
                        }
                ));

                builder.HasMany(s=>s.Students).WithMany(c=>c.Courses).UsingEntity(j=>j.ToTable("StudentCourses"));
               
            }
    }
}