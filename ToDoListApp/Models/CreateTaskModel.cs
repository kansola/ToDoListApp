using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.Models
{
    public class CreateTaskModel
    {
        public string TaskName { get; set; }
        public DateTime DateTimeDueForCompletion { get; set; }
    }
}
