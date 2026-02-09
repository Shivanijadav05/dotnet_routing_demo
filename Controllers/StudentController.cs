using Microsoft.AspNetCore.Mvc;
// using MyWebApi.Models;
using Microsoft.Extensions.Logging;

using MyWebApi.DTOs;
using MyWebApi.Database;
using Microsoft.EntityFrameworkCore;


namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {

        // private readonly IStudentRepository _repository;
        private readonly ILogger<StudentController> _myLogger;
        private readonly CollegeDBContext _dbContext;

        public StudentController(ILogger<StudentController> Logger,CollegeDBContext dbContext)
        {
            // _repository=repository;
            _myLogger=Logger;
            _dbContext=dbContext;

        }


        // [HttpGet("test")]
      
        // public IActionResult ThrowError()
        // {
        //     throw new Exception("exception thrown");
        // }



        [HttpGet]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
                   _myLogger.LogInformation("GETTINGT STUDENTS");
                var students=_dbContext.Students.Select(s=>new StudentDTO()
                {
                        Id=s.Id,
                        StudentName=s.StudentName,
                        Address=s.Address,
                        Email=s.Email
                });

            return Ok(students);
        }


         [HttpGet("{id:int}",Name="GetStudentById")]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if(id<=0)
            {
                return BadRequest();  //400 client error
            }
            var student=_dbContext.Students.Where(n=>n.Id==id).FirstOrDefault();
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
        // [HttpPost]
        // [Route("create-fluent")]
        // public ActionResult<StudentDTO> CreateStudentFluent([FromBody]StudentDTOFluent dto)
        // {
                 
        //      Student student=new Student
        //     {
                
        //         StudentName=dto.StudentName,
                
        //         Email=dto.Email,
                
        //     };
        //     CollegeRepository.Students.Add(student);
          
     
        //     return Ok(student);
        // }





        [HttpGet("by-name/{name:alpha}")]
        // [Route("{name:alpha}")]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
             if(string.IsNullOrEmpty(name))
            {
                return BadRequest();  //400 client error
            }
            var student=_dbContext.Students.Where(n=>n.StudentName==name).FirstOrDefault();
            if(student==null)
            {
                return NotFound($"The student with {name} doesnt exist"); // 404 client error
            }
            var studentDTO=new StudentDTO
            {
                         Id=student.Id,
                        StudentName=student.StudentName,
                        Address=student.Address,
                        Email=student.Email
            };
            return Ok(studentDTO);
            // return CollegeRepository.Students.Where(n=>n.StudentName==name).FirstOrDefault();
        }

          [HttpDelete("{id:int}")]
        public ActionResult<bool> DeleteStudentById(int id)
        {
            
              if(id<=0)
            {
                return BadRequest();  //400 client error
            }
            var student=_dbContext.Students.Where(n=>n.Id==id).FirstOrDefault();
            if(student==null)
            {
                return NotFound($"The student with {id} doesnt exist"); // 404 client error
            }
           
            _dbContext.Students.Remove(student);
              _dbContext.SaveChanges();
            return Ok(true);
        }

       
        [HttpPost]
        [Route("Create") ]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody]StudentDTO model)
        {
           
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // int newId=_dbContext.Students.LastOrDefault().Id+1;
            Student student=new Student
            {
                // Id=newId,
                StudentName=model.StudentName,
                Address=model.Address,
                Email=model.Email,
                // AdmissionDate=model.AdmissionDate
            };
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();
            // model.Id=newId;
            return CreatedAtRoute("GetStudentById",new {id=model.Id},model);
        } 


            [HttpPut]
        public ActionResult UpdateStudent([FromBody]StudentDTO model)
        {
            if(model==null)
            {
                return BadRequest();
            }

            var existingStudent=_dbContext.Students.Where(s=>s.Id==model.Id).FirstOrDefault();
            if(existingStudent==null)
            {
                return NotFound();
            }
            existingStudent.StudentName=model.StudentName;
            existingStudent.Address=model.Address;
            existingStudent.Email=model.Email;
              _dbContext.SaveChanges();
            // existingStudent.AdmissionDate=model.AdmissionDate;

            return NoContent();




        }


    }
}
