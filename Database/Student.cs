using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApi.Database
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         public int Id{get;set; }
        public string StudentName {get;set;}
        public string Email {get;set;}
        public string Address {get;set;}
         public int? DepartmentId{get;set;}
        public virtual Department? Department{get;set;}

        public ICollection<Course> Courses{get;set;}=new List<Course>(); 
        // public ICollection<StudentCourse> StudentCourses{get;set;}
    }

}


