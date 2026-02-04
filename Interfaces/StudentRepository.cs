using MyWebApi.Models;

public class StudentRepository:IStudentRepository{
    public IEnumerable<Student> GetStudents()
    {
        return CollegeRepository.Students;
    }

    public Student GetStudentById(int id)
    {
        return CollegeRepository.Students.FirstOrDefault(s => s.Id == id);
    }
}