using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Models;

namespace ToDoListApp.Interfaces
{
    public interface ITaskService
    {
        public bool AddTask(CreateTaskModel task, out string responseMessage);

        public List<Tasks> GetAllTasks(out string responseMessage);

        public List<Tasks> GetAllCompletedTasks(out string responseMessage);

    }
}
