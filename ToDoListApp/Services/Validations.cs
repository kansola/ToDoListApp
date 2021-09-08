using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ToDoListApp.Services
{
    public class Validations
    {
        public bool ValidateEmail(string emailaddress, out string ErrorMessage)
        {
            ErrorMessage = "";
            bool response = false;
            if (String.IsNullOrWhiteSpace(emailaddress))
            {
                //throw new Exception("Email Address should not be empty");
                ErrorMessage = "Email Address should not be empty";
            }
            else
            {
                try
                {
                    MailAddress m = new MailAddress(emailaddress);
                    ErrorMessage = string.Empty;
                    response = true;
                }
                catch (FormatException)
                {
                    ErrorMessage = "Email Address not in the correct format";
                    response = false;
                }
            }

            return response;

          
        }


        public bool ValidatePassword(string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage += "Password should contain At least one lower case letter, ";
                //return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage += "Password should contain At least one upper case letter, ";
                //return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage += "Password should not be less than 8 characters, ";
                //return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage += "Password should contain At least one numeric value, ";
                //return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage += "Password should contain At least one special case characters";
                //return false;
            }
            else
            {
                return true;
            }

            return false;
        }

        public bool ValidateName(string name, out string ErrorMessage)
        {
            var input = name;
            ErrorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Name should not be empty");
            }
            var hasFirstCharAlpha = new Regex(@"(?<firstchar>(?=[A-Za-z]))");
            var hasCharAlpha = new Regex(@"(?<alphachars>[A-Za-z])");
            var hasSpecialChars = new Regex(@"^[a-zA-Z’\-'()/.,\s]+$");

            if (!hasFirstCharAlpha.IsMatch(input))
            {
                ErrorMessage += "Name must start with Alphabet, ";
                //return false;
            }
            else if (!hasCharAlpha.IsMatch(input))
            {
                ErrorMessage += "Name must contain Alphabets";
                //return false;
            }
            //else if (!hasSpecialChars.IsMatch(input))
            //{
            //    ErrorMessage = "Name must have special characters before or after an Alphabet";
            //    return false;
            //}

            else
            {
                return true;
            }

            return false;


        }

    }
}
