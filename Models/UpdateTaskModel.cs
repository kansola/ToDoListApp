using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.Models
{
    public class UpdateTaskModel
    {
        [Required]
        public int TaskId { get; set; }
        [Required]
        public string TaskName { get; set; }
        [Required]
        public DateTime TimeDueForCompletion { get; set; }
        //[Required]
        //public string AuthorizationToken { get; set; }

    }
}
