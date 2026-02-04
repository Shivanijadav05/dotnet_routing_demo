using  MyWebApi.Models;


 public static class CollegeRepository
{
    public static List<Student> Students{get; set;}= new List<Student>{
                new Student{
                    Id=1,
                    StudentName="Student1", 
                    Email="studentemail@gmail.com" ,
                    Address="Hyd, India" 
                },
                 new Student {
                    Id=2,
                    StudentName="Student2", 
                    Email="studentemail2@gmail.com" ,
                    Address="Guj, India"
                 }
            };
}


