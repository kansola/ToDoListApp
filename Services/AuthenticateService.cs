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
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;

        public AuthenticateService(IConfiguration configuration)

        {
            _config = configuration;
            connectionString = _config.GetValue<string>("ConnectionStrings:ToDolistDb");

        }
        public bool AuthenticateUser(Login login, out string responseMessage)
        {
            bool isRegister = false;
            try
            {
                int response = 0;
                responseMessage = "";

                if (login != null)
                {
                    try
                    {
                        SqlConnection sqlCon = new SqlConnection(connectionString);
                        SqlCommand sqlCommand = new SqlCommand("AuthenticateUser", sqlCon);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@email", login.Email.Trim().ToLower());
                        sqlCommand.Parameters.AddWithValue("@password", login.Password.Trim());

                        sqlCommand.Parameters.Add("@response", SqlDbType.Int).Direction = ParameterDirection.Output;
                        try
                        {
                            sqlCon.Open();
                            sqlCommand.ExecuteNonQuery();
                            response = Convert.ToInt32(sqlCommand.Parameters["@response"].Value);

                            if (response > 0)
                            {
                                responseMessage = "Authentication Successful";
                                isRegister = true;
                            }
                           
                            else
                            {
                                responseMessage = "Authentication Failed";
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                            //throw new Exception("Error Encountered During Registration, Please Try Again");
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                        //throw new Exception("Error Encountered During Authentication, Please Try Again");
                    }

                }
            }
            catch (Exception)
            {
                throw;
                //throw new Exception("Error Encountered During Authentication, Please Try Again");
            }
            return isRegister;
        }


        public void RegisterToken(string userEmail, string token)
        {
           

                    try
                    {
                        SqlConnection sqlCon = new SqlConnection(connectionString);
                        SqlCommand sqlCommand = new SqlCommand("RegisterToken", sqlCon);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@UserEmail", userEmail.Trim());
                        sqlCommand.Parameters.AddWithValue("@Token", token.Trim());

                       
                        try
                        {
                            sqlCon.Open();
                            sqlCommand.ExecuteNonQuery();
                            
                        }
                        catch (Exception)
                        {
                            throw;
                            //throw new Exception("Error Encountered During Registration, Please Try Again");
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                        //throw new Exception("Error Encountered During Authentication, Please Try Again");
                    }

                
            
        }

        public bool LogOut(string token, out string responseMessage)
        {
            bool isLoggedOut = false;
            try
            {
                int response = 0;
                responseMessage = "";

                
                    try
                    {
                        SqlConnection sqlCon = new SqlConnection(connectionString);
                        SqlCommand sqlCommand = new SqlCommand("LogOutSession", sqlCon);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@token", token.Trim());

                        sqlCommand.Parameters.Add("@response", SqlDbType.Int).Direction = ParameterDirection.Output;
                        try
                        {
                            sqlCon.Open();
                            sqlCommand.ExecuteNonQuery();
                            response = Convert.ToInt32(sqlCommand.Parameters["@response"].Value);

                            if (response > 0)
                            {
                                responseMessage = "Logged out";
                                isLoggedOut = true;
                            }

                            else if (response < 0)
                            {
                                responseMessage = "Invalid Token";
                            }
                            else
                            {
                                responseMessage = "An error has occured!";
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                            //throw new Exception("Error Encountered During Registration, Please Try Again");
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                        //throw new Exception("Error Encountered During Authentication, Please Try Again");
                    }

                
            }
            catch (Exception)
            {
                throw;
                //throw new Exception("Error Encountered During Authentication, Please Try Again");
            }
            return isLoggedOut;
        }
    }
}
