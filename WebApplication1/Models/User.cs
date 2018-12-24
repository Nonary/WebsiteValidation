using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;

namespace WebApplication1.Models
{

    public class User
    {
        [Display(Name = "First Name")] public string FirstName { get; set; }
        [Display(Name = "Last Name")] public string LastName { get; set; }

        [Display(Name = "E-Mail Address")] public string EmailAddress { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }


        public Country Country { get; set; }

        public State State { get; set; }



    }

    public class Country
    {
        public string Name { get; set; }
        public static List<string> GetCountries { get; set; }
        = new List<string>
            {
                "Canada",
                "United States"
            };

        public override string ToString()
        {
            return Name;
        }
    }

    public class State
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}