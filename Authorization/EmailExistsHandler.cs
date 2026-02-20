using Microsoft.AspNetCore.Authorization;
using MyWebApi.Database;
using System;
using MyWebApi.Controllers;
using System.Security.Claims;
 

public class EmailExistsHandler : AuthorizationHandler<EmailExistsRequirement>
{


    private readonly CollegeDBContext _context;
   

    public EmailExistsHandler(CollegeDBContext  context,ILogger<AuthService> myLogger)
    {
        _context = context;
       
    }


    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        EmailExistsRequirement requirement)
    {
        // 1️⃣ Get email from token claims
        var email = context.User.FindFirst(ClaimTypes.Email)?.Value;


            if (!string.IsNullOrEmpty(email))
            {
                var userExists = _context.Students
                    .Any(x => x.Email == email);

                if (userExists)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
            }
}
