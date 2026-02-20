
using System.ComponentModel.DataAnnotations;

namespace MyWebApi.DTOs
{

    public class LoginDTO
    {
        [Required]
        public string studentName{get;set;}

          [Required]
        public string password{get;set;}
        public string email{get;set;}
    }
   
}