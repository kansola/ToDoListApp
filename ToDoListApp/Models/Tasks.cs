using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string DateTimeDueForCompletion { get; set; }
        public bool IsComplete { get; set; }
        public string DateTimeCompleted { get; set; }

    }
}
