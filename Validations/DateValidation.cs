namespace MyWebApi.Validations;
using System.ComponentModel.DataAnnotations;



public class DateValidation:ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var date=(DateTime?)value;
        if(date<DateTime.Now)
        {
            return new ValidationResult("DATE MUST BE GREATER THAN TODAYS");
        }
        return ValidationResult.Success;
    }
}