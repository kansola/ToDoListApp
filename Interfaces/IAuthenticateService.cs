using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Models;

namespace ToDoListApp.Interfaces
{
    public interface IAuthenticateService
    {
        public bool AuthenticateUser(Login login, out string responseMessage);

        public void RegisterToken(string userEmail, string token);

        public bool LogOut(string token, out string responseMessage);

        public bool VerifyToken(string token);
    }
}
