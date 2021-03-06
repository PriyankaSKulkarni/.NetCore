﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Address Address { get; set; }
                
        public string PhotoPath { get; set; }

    }
}
