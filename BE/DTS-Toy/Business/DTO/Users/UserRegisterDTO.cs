﻿using Business.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO.Users
{
    public class UserRegisterDTO
    {
        //public string? FullName { get; set; }

        public string? Email { get; set; }

        //public string? PhoneNumber { get; set; }

        public string? Password { get; set; }
       
    }
}
