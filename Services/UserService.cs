//using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Interfaces;
using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public class UserService: IUserService
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;

        public UserService(IConfiguration configuration)

        {
            _config = configuration;
            connectionString = configuration.GetConnectionString("ToDolistDb");

           // connectionString = _config.GetValue<string>("ConnectionStrings:ToDolistDb");

        }

        public bool RegisterUser(CreateUserModel user, out string responseMessage )
        {
            bool isRegister = false;
            try
            {
                int response = 0;
                responseMessage = "";
                
                if (user != null)
                {
                    try
                    {
                        SqlConnection sqlCon = new SqlConnection(connectionString);
                        SqlCommand sqlCommand = new SqlCommand("RegisterUser", sqlCon);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@email", user.Email.Trim().ToLower());
                        sqlCommand.Parameters.AddWithValue("@password", user.Password.Trim());
                        sqlCommand.Parameters.AddWithValue("@fullName", user.FullName.Trim());

                        sqlCommand.Parameters.Add("@response", SqlDbType.Int).Direction = ParameterDirection.Output;
                        try
                        {
                            sqlCon.Open();
                            sqlCommand.ExecuteNonQuery();
                             response = Convert.ToInt32(sqlCommand.Parameters["@response"].Value);

                            if (response == 1)
                            {
                                responseMessage = "User Email Address Already Exist";
                                
                            }
                            else if (response == 2)
                            {
                                
                                responseMessage = "User Registration Successful";
                                isRegister = true;
                            }
                            else
                            {
                                responseMessage = "Failed to register user";
                            }
                        }
                        catch (Exception e)
                        {
                            throw;
                            //string a = e.Message + e.StackTrace;

                            //throw new Exception("Error Encountered During Registration, Please Try Again");
                        }
                    }
                    catch (Exception e)
                    {
                        throw;

                        //string a = e.Message + e.StackTrace;
                        //throw new Exception("Error Encountered During Registration, Please Try Again");
                    }

                }
            }
            catch (Exception )
            {
                throw;

                //throw new Exception("Error Encountered During Registration, Please Try Again");
            }
            return isRegister;
        }
    }
}
