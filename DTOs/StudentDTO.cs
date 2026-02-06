using System.ComponentModel.DataAnnotations;
using MyWebApi.Validations;

namespace MyWebApi.DTOs
{
public class StudentDTO
{
        public int Id{get;set; }
        [Required]
        public string StudentName {get;set;}

        [EmailAddress]
        public string Email {get;set;}
        

        public string Address {get;set;}

        //  [DateValidation]
        // public DateTime AdmissionDate {get;set;}

}






public class StudentDTOFluent
{
        public string StudentName {get;set;}

        public string Email{get;set;}
}
}