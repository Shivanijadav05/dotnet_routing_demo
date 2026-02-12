namespace MyWebApi.Database
{
    public class Course
    {
        public int Id{get;set;}
        public string CourseName {get;set;}

        public ICollection<Student>Students{get;set;}=new List<Student>();
        // public ICollection<StudentCourse> StudentCourses{get;set;}
    }
}