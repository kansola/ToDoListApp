using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Models;

namespace ToDoListApp.Interfaces
{
    public interface ITaskService
    {
        public bool AddTask(CreateTaskModel task, string token, out string responseMessage);
        public List<Tasks> GetAllTasks(string token, out string responseMessage);

        public List<Tasks> GetAllCompletedTasks(string token, out string responseMessage);

        public bool CompleteTask(CompleteTaskModel task, string token, out string responseMessage);

        public bool UpdateTask(UpdateTaskModel task, string token, out string responseMessage);


    }
}
