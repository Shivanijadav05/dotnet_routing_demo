using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MyWebApi.Database.Config
{
    public class DepartmentConfig:IEntityTypeConfiguration<Department>
    {
            public void Configure(EntityTypeBuilder<Department> builder)
            {
                builder.ToTable("Departments");
                builder.HasKey(x=>x.Id);
                builder.Property(x=>x.Id).UseIdentityColumn();

                builder.Property(n=>n.DepartmentName).IsRequired().HasMaxLength(250);;
                
               
                builder.Property(n=>n.Description).IsRequired(false).HasMaxLength(500);

                builder.HasData(new List<Department>(
                    new List<Department>()
                        {
                            new Department
                            {
                                Id=1,
                               DepartmentName="CE",
                               Description="Computer engineering"  
                            },
                            new Department
                            {
                                Id=2,
                               DepartmentName="IT",
                               Description="Information Technology"
                            }
                        }
                ));

            }
    }
    

    
}