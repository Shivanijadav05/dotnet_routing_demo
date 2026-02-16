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


namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  

    public class LoginController:ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration=configuration;
        }
        [HttpPost]
        public ActionResult Login(LoginDTO model)
        {
                LoginResponseDTO response=new()
                {
                    Username=model.studentName
                }; 
                if(!ModelState.IsValid)
                {
                    return BadRequest("invalid username and password");
                }
                if(model.studentName=="Shivani" && model.password=="Shivani@935")
                {
                    var key=Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWTSecret"));
                    var tokenHandler=new JwtSecurityTokenHandler();
                    var tokenDescriptor=new SecurityTokenDescriptor()
                    {
                        Subject=new System.Security.Claims.ClaimsIdentity(new Claim[]
                        {
                             new Claim(ClaimTypes.Name, model.studentName)
                        }),
                        Expires=DateTime.Now.AddHours(1),
                          Issuer = "MyWebApi",       
                        Audience = "MyAngularApp", 
                        SigningCredentials = new ( new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                    };
                    var token=tokenHandler.CreateToken(tokenDescriptor);
                  response.token=tokenHandler.WriteToken(token);
                }
                else
                { 
                    return Unauthorized("Invalid username and password");
                }
                return Ok(response);
        }
    }
}