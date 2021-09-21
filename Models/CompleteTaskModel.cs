using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.Models
{
    public class CompleteTaskModel
    {
        [Required]
        public int TaskId { get; set; }
        
        [Required]
        public DateTime DateTimeCompleted { get; set; }
        //[Required]
        //public string AuthorizationToken { get; set; }

    }
}
