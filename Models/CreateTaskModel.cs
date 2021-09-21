﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.Models
{
    public class CreateTaskModel
    {
        [Required]
        public string TaskName { get; set; }
        [Required]
        public DateTime DateTimeDueForCompletion { get; set; }
        //[Required]
        //public string AuthorizationToken { get; set; }
    }
}
