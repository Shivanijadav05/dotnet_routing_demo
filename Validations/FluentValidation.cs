// using FluentValidation;
// using MyWebApi.DTOs;
// // using MyWebApi.Models;


// public class UserValidation:AbstractValidator<StudentDTOFluent>
// {       
//     public UserValidation()
//     {
//         RuleFor(user=>user.StudentName)
//         .Cascade(CascadeMode.Stop)
//         .NotEmpty()
//         .Length(5)
//         .WithMessage("user name cant be empty and length should be less than 5");

//         RuleFor(user=>user.Email)
//         .Cascade(CascadeMode.Stop)
//         .EmailAddress()
//         .WithMessage("Invalid email address");
   
//     }



// }