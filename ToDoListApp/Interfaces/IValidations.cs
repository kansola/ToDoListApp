using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.Interfaces
{
    public interface IValidations
    {
        public void ValidateEmail(string emailaddress, out string ErrorMessage);

        public void ValidatePassword(string password, out string ErrorMessage);

        public void ValidateName(string name, out string ErrorMessage);

    }
}
