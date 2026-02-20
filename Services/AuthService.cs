using Microsoft.AspNetCore.Mvc;
// using MyWebApi.Models;
using Microsoft.Extensions.Logging;
using System.Text;
using MyWebApi.DTOs;
using MyWebApi.Database;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Configutations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

public class AuthService:IAuthService
{       private readonly CollegeDBContext _dbContext;
    private readonly IPasswordService _passwordService;
    private readonly IConfiguration _configuration;

    public AuthService( CollegeDBContext dbContext,IPasswordService passwordService,IConfiguration configuration)
        {
            _dbContext = dbContext;
            _passwordService = passwordService;
            _configuration = configuration;
        }

    public async Task<LoginResponseDTO?> Login(LoginDTO model)
        {
               
                var student =await _dbContext.Students.FirstOrDefaultAsync(x=>x.StudentName==model.studentName);
                if(student==null)
                {
                    return null;
                }
                       var isValid = _passwordService.VerifyPassword( student.Password,model.password);
                 if(!isValid)
                 {
                    return null;
                 }
                 var token=generatetoken(model.studentName,model.email);

                 return new LoginResponseDTO
                 {
                    Username=student.StudentName,
                    token=token
                 };

               
        }




        public string generatetoken(string username,string email)
            {
                var key=Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWTSecret"));
                                var tokenHandler=new JwtSecurityTokenHandler();
                                var tokenDescriptor=new SecurityTokenDescriptor()
                                {
                                    Subject=new System.Security.Claims.ClaimsIdentity(new Claim[]
                                    {
                                        new Claim(ClaimTypes.Name, username),
                                        new Claim(ClaimTypes.Email, email)
                                    }),
                                    Expires=DateTime.Now.AddHours(1),
                                    Issuer = "MyWebApi",       
                                    Audience = "MyAngularApp", 
                                    SigningCredentials = new ( new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                                };
                                var token=tokenHandler.CreateToken(tokenDescriptor);
                            
                            return tokenHandler.WriteToken(token);;
            }
}


