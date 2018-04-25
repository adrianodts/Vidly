using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class DateValidate: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var movie = (Movie) validationContext.ObjectInstance;

            if (movie.ReleaseDate != null && movie.ReleaseDate.Value.Year > 0)
            {
                var isInvalidDate = DateTime.Compare(DateTime.Today, movie.ReleaseDate.Value);

                return (isInvalidDate >= 0
                    ? ValidationResult.Success
                    : new ValidationResult("Release Date should not be a future date"));
            }
            else
                return ValidationResult.Success;
        }
    }
}