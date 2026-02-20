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
        private readonly IAuthService _authService;
        public LoginController(IConfiguration configuration,IAuthService authService)
        {
            _configuration=configuration;
            _authService=authService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var result=await _authService.Login(model);
               if(result==null)
               {
                     return Unauthorized("Invalid username and password");
               }
                
                return Ok(result);
        }
    }
}