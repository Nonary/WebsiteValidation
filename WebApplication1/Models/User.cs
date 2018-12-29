using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Models
{
    public class User
    {
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]

        public string LastName { get; set; }

        [Display(Name = "E-Mail Address")]
        [DataType(DataType.EmailAddress)]
        [Remote("ValidateEmailAddress", "Home")]
        [Required]
        public string EmailAddress { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please select a Country.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please select a State or Providence.")]

        public string State { get; set; }
    }

    public class BirthdayValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime) value > DateTime.Now.AddYears(-100) && (DateTime) value < DateTime.Now)
                return ValidationResult.Success;
            return new ValidationResult(null);
        }
    }

    public class Country
    {
        public string Name { get; set; }

        public static List<string> GetCountries { get; set; }
            = new List<string>
            {
                null,
                "Canada",
                "United States"
            };

        public override string ToString()
        {
            return Name;
        }
    }
}