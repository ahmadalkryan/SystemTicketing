﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Dtos.LogIn
{
    public class LoginDto
    {

         
      //  [Required(ErrorMessage="Required")]
       public string Name {  get; set; } // change to Name 
      //  [Required(ErrorMessage = "Required")]
        public  string Password { get; set; } // Password

    }
}
