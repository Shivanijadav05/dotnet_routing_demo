using Microsoft.AspNetCore.Mvc;
// using MyWebApi.Models;
using Microsoft.Extensions.Logging;

using MyWebApi.DTOs;
using MyWebApi.Database;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Configutations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;


namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
     [Authorize]
    public class StudentController : ControllerBase
    {

        // private readonly IStudentRepository _repository;
        private readonly ILogger<StudentController> _myLogger;
        private readonly CollegeDBContext _dbContext;

        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public StudentController(ILogger<StudentController> Logger,
                        CollegeDBContext dbContext,
                        IPasswordService passwordService
                        )
        {
            // _repository=repository;
            _myLogger=Logger;
            _dbContext=dbContext;
            _passwordService=passwordService;
           

        }


        // [HttpGet("test")]
      
        // public IActionResult ThrowError()
        // {
        //     throw new Exception("exception thrown");
        // }


        [AllowAnonymous]
        [HttpGet]
        public async  Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
                   _myLogger.LogInformation("GETTINGT STUDENTS");
                   var students=await _dbContext.Students.AsNoTracking().Select(s=>new StudentDTO()
                            {
                                    Id=s.Id,
                                    StudentName=s.StudentName,
                                    Address=s.Address,
                                    Email=s.Email,
                                    Password=s.Password
                            }).ToListAsync();
               

            return Ok(students);
        }


        [HttpGet("with-department")]
        public async Task<ActionResult> GetStudentsWithDepartment()
        {
            var students = await _dbContext.Students
                .AsNoTracking()
                .Select(s => new
                {
                    s.Id,
                    s.StudentName,
                    s.Email,
                    DepartmentName = s.Department.DepartmentName
                })
                .ToListAsync();

            return Ok(students);
        }

        // [HttpGet("student-course-dept")]
        //     public async Task<IActionResult> GetStudentCourseDepartment()
        //     {
        //         var result = await _dbContext.StudentCourses
        //             .AsNoTracking()
        //             .Select(sc => new
        //             {
        //                 StudentId = sc.StudentId,
        //                 CourseId = sc.CourseId,
        //                 DepartmentId = sc.Student.DepartmentId
        //             })
        //             .ToListAsync();

        //         return Ok(result);
        //     }

        [HttpGet("StudentDepartmentCourse")]
            public async Task<ActionResult<IEnumerable<StudentCourseDepartmentDTO>>> GetStudentDepartmentCourse()
            {
                var result = await _dbContext.Students
                    .Include(s => s.Courses)
                    .SelectMany(s => s.Courses.Select(c => new StudentCourseDepartmentDTO
                    {
                        StudentId = s.Id,
                        DepartmentId = s.DepartmentId,
                        CourseId = c.Id
                    }))
                    .ToListAsync();

                return Ok(result);
            }


      [Authorize(Policy = "EmailExistsPolicy")]
         [HttpGet("{id:int}",Name="GetStudentById")]
        public async Task<ActionResult<StudentDTO>> GetStudentById(int id)
        {
            if(id<=0)
            {
                return BadRequest();  //400 client error
            }
            var student=await _dbContext.Students.AsNoTracking().Where(n=>n.Id==id)
            .Select(s=>new StudentDTO{
                Id=s.Id,
                StudentName=s.StudentName,
                Address = s.Address,
                 Email = s.Email
            }).FirstOrDefaultAsync();
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
        public async Task<ActionResult<StudentDTO>> GetStudentByName(string name)
        {
             if(string.IsNullOrEmpty(name))
            {
                return BadRequest();  //400 client error
            }
            var student=await _dbContext.Students.Where(n=>n.StudentName==name).FirstOrDefaultAsync();
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
        public async Task<ActionResult<bool>> DeleteStudentById(int id)
        {
            
              if(id<=0)
            {
                return BadRequest();  //400 client error
            }
            var student= await _dbContext.Students.Where(n=>n.Id==id).FirstOrDefaultAsync();
            if(student==null)
            {
                return NotFound($"The student with {id} doesnt exist"); // 404 client error
            }
           
            _dbContext.Students.Remove(student);
              await _dbContext.SaveChangesAsync();
            return Ok(true);
        }
            [AllowAnonymous]
        [HttpPost]
        [Route("Create") ]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> CreateStudent([FromBody]StudentDTO model)
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
                Password=_passwordService.HashPassword(model.Password)
                // AdmissionDate=model.AdmissionDate
            };
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            // model.Id=newId;
            return CreatedAtRoute("GetStudentById",new {id=model.Id},model);
        } 


            [HttpPut]
        public async Task<ActionResult> UpdateStudent([FromBody]StudentDTO model)
        {
            if(model==null)
            {
                return BadRequest();
            }

            var existingStudent=await _dbContext.Students.AsNoTracking().Where(s=>s.Id==model.Id).FirstOrDefaultAsync();
            if(existingStudent==null)
            {
                return NotFound();
            }
            var newRecord=new Student()
            {
                Id=existingStudent.Id,
                StudentName=model.StudentName,
                Address=model.Address,
                Email=model.Email

            } ;
           
              _dbContext.Students.Update(newRecord);
              await _dbContext.SaveChangesAsync();
            // existingStudent.AdmissionDate=model.AdmissionDate;

            return NoContent();




        }


    }
}
