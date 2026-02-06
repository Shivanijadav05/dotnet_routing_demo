using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;
using Microsoft.Extensions.Logging;

using MyWebApi.DTOs;


namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {

        private readonly IStudentRepository _repository;
        private readonly ILogger<StudentController> _myLogger;

        public StudentController(IStudentRepository repository,ILogger<StudentController> Logger)
        {
            _repository=repository;
            _myLogger=Logger;
        }


        [HttpGet("test")]
      
        public IActionResult ThrowError()
        {
            throw new Exception("exception thrown");
        }



        [HttpGet]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
                   _myLogger.LogInformation("GETTINGT STUDENTS");
                var students=_repository.GetStudents().Select(s=>new StudentDTO()
                {
                        Id=s.Id,
                        StudentName=s.StudentName,
                        Address=s.Address,
                        Email=s.Email
                });

            return Ok(students);
        }


         [HttpGet("{id:int}",Name="GetStudentById")]
        public ActionResult<Student> GetStudentById(int id)
        {
            if(id<=0)
            {
                return BadRequest();  //400 client error
            }
            var student=_repository.GetStudentById(id);
            if(student==null)
            {
                return NotFound($"The student with {id} doesnt exist"); // 404 client error
            }
            var studentDTO=new StudentDTO()
                {
                        Id=student.Id,
                        StudentName=student.StudentName,
                        Address=student.Address,
                        Email=student.Email
                };
            
            return Ok(studentDTO);
            
        }
                // creating student using fluent validations 
        [HttpPost]
        [Route("create-fluent")]
        public ActionResult<Student> CreateStudentFluent([FromBody]StudentDTOFluent dto)
        {
                 
             Student student=new Student
            {
                
                StudentName=dto.StudentName,
                
                Email=dto.Email,
                
            };
            CollegeRepository.Students.Add(student);
          
     
            return Ok(student);
        }





        [HttpGet("by-name/{name:alpha}")]
        // [Route("{name:alpha}")]
        public ActionResult<Student> GetStudentByName(string name)
        {
             if(string.IsNullOrEmpty(name))
            {
                return BadRequest();  //400 client error
            }
            var student=CollegeRepository.Students.Where(n=>n.StudentName==name).FirstOrDefault();
            if(student==null)
            {
                return NotFound($"The student with {name} doesnt exist"); // 404 client error
            }
            return Ok(student);
            // return CollegeRepository.Students.Where(n=>n.StudentName==name).FirstOrDefault();
        }

          [HttpDelete("{id:int}")]
        public ActionResult<bool> DeleteStudentById(int id)
        {
            
              if(id<=0)
            {
                return BadRequest();  //400 client error
            }
            var student=CollegeRepository.Students.Where(n=>n.Id==id).FirstOrDefault();
            if(student==null)
            {
                return NotFound($"The student with {id} doesnt exist"); // 404 client error
            }
           
            CollegeRepository.Students.Remove(student);
            return Ok(true);
        }

       
        [HttpPost]
        [Route("Create") ]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody]StudentDTO model)
        {
           
            if(model==null)
            {
                return BadRequest();
            }
            int newId=CollegeRepository.Students.LastOrDefault().Id+1;
            Student student=new Student
            {
                Id=newId,
                StudentName=model.StudentName,
                Address=model.Address,
                Email=model.Email,
                // AdmissionDate=model.AdmissionDate
            };
            CollegeRepository.Students.Add(student);
            model.Id=newId;
            return CreatedAtRoute("GetStudentById",new {id=model.Id},model);
        } 


            [HttpPut]
        public ActionResult UpdateStudent([FromBody]StudentDTO model)
        {
            if(model==null)
            {
                return BadRequest();
            }

            var existingStudent=CollegeRepository.Students.Where(s=>s.Id==model.Id).FirstOrDefault();
            if(existingStudent==null)
            {
                return NotFound();
            }
            existingStudent.StudentName=model.StudentName;
            existingStudent.Address=model.Address;
            existingStudent.Email=model.Email;
            // existingStudent.AdmissionDate=model.AdmissionDate;

            return NoContent();




        }


    }
}
