using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyWebApi.Database.Config
{
    public class StudentConfig:IEntityTypeConfiguration<Student>
    {
            public void Configure(EntityTypeBuilder<Student> builder)
            {
                builder.ToTable("Students");
                builder.HasKey(x=>x.Id);
                builder.Property(x=>x.Id).UseIdentityColumn();

                builder.Property(n=>n.StudentName).IsRequired();
                builder.Property(nameof=>nameof.StudentName).HasMaxLength(250);
                builder.Property(n=>n.Address).IsRequired(false);
                builder.Property(n=>n.Email).IsRequired().HasMaxLength(250);

                builder.HasData(new List<Student>(
                    new List<Student>()
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
                        }
                ));

            }
    }
}