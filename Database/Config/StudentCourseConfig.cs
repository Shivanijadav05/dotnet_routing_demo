// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace MyWebApi.Database.Config
// {
//     public class StudentCourseConfig:IEntityTypeConfiguration<StudentCourse>
//     {
//             public void Configure(EntityTypeBuilder<StudentCourse> builder)
//             {
//                 builder.ToTable("StudentCourses");
//                 builder.HasKey(s=> new{s.StudentId,s.CourseId});
              
//                 builder.HasOne(n=>n.Student)
//                 .WithMany(n=>n.StudentCourses)
//                 .HasForeignKey(n=>n.StudentId);

//                  builder.HasOne(n=>n.Course)
//                 .WithMany(n=>n.StudentCourses)
//                 .HasForeignKey(n=>n.CourseId);

//             }
//     }
// }