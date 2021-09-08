using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Models;

namespace ToDoListApp.Interfaces
{
    public interface IUserService
    {
        public bool RegisterUser(CreateUserModel user, out string responseMessage);
    }
}
