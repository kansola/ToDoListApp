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
    public class TaskService : ITaskService
    {
        private readonly IConfiguration _config;
        private readonly string connectionString;

        public TaskService(IConfiguration configuration)

        {
            _config = configuration;
            connectionString = _config.GetValue<string>("ConnectionStrings:ToDolistDb");

        }
        public bool AddTask(CreateTaskModel task, string token , out string responseMessage)
        {
            
            bool isRegister = false;
            try
            {
                int response = 0;
                responseMessage = "";

                if (task != null)
                {
                    try
                    {
                        SqlConnection sqlCon = new SqlConnection(connectionString);
                        SqlCommand sqlCommand = new SqlCommand("AddTask", sqlCon);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@token", token);
                        sqlCommand.Parameters.AddWithValue("@TaskName", task.TaskName.Trim());
                        sqlCommand.Parameters.AddWithValue("@TimeDueForCompletion", task.DateTimeDueForCompletion);

                        sqlCommand.Parameters.Add("@response", SqlDbType.Int).Direction = ParameterDirection.Output;
                        try
                        {
                            sqlCon.Open();
                            sqlCommand.ExecuteNonQuery();
                            response = Convert.ToInt32(sqlCommand.Parameters["@response"].Value);

                            if (response > 0)
                            {
                                responseMessage = "Task added Successfully";
                                isRegister = true;
                            }

                            else
                            {
                                responseMessage = "Failed to add task";
                            }
                        }
                        catch (Exception)
                        {

                            throw new Exception("Error Encountered while adding task, Please Try Again");
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("Error Encountered while adding task, Please Try Again");
                    }

                }
            }
            catch (Exception)
            {
                throw new Exception("Error Encountered while adding task, Please Try Again");
            }
            return isRegister;
        }

        public bool CompleteTask(CompleteTaskModel task, string token, out string responseMessage)
        {

            bool isComplete = false;
            try
            {
                int response = 0;
                responseMessage = "";

                if (task != null)
                {
                    try
                    {
                        SqlConnection sqlCon = new SqlConnection(connectionString);
                        SqlCommand sqlCommand = new SqlCommand("CompleteTask", sqlCon);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@token", token);
                        sqlCommand.Parameters.AddWithValue("@TaskId", task.TaskId);
                        sqlCommand.Parameters.AddWithValue("@TaskCompletedTime", task.DateTimeCompleted);

                        sqlCommand.Parameters.Add("@response", SqlDbType.Int).Direction = ParameterDirection.Output;
                        try
                        {
                            sqlCon.Open();
                            sqlCommand.ExecuteNonQuery();
                            response = Convert.ToInt32(sqlCommand.Parameters["@response"].Value);

                            if (response > 0)
                            {
                                responseMessage = "Task Completed Successfully";
                                isComplete = true;
                            }

                            else
                            {
                                responseMessage = "Failed to complete task";
                            }
                        }
                        catch (Exception)
                        {

                            throw new Exception("Error Encountered while adding task, Please Try Again");
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("Error Encountered while adding task, Please Try Again");
                    }

                }
            }
            catch (Exception)
            {
                throw new Exception("Error Encountered while adding task, Please Try Again");
            }
            return isComplete;
        }

        public bool UpdateTask(UpdateTaskModel task, string token, out string responseMessage)
        {

            bool isComplete = false;
            try
            {
                int response = 0;
                responseMessage = "";

                if (task != null)
                {
                    try
                    {
                        SqlConnection sqlCon = new SqlConnection(connectionString);
                        SqlCommand sqlCommand = new SqlCommand("UpdateTask", sqlCon);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@token", token);
                        sqlCommand.Parameters.AddWithValue("@TaskName", task.TaskName);
                        sqlCommand.Parameters.AddWithValue("@TaskId", task.TaskId);

                        sqlCommand.Parameters.AddWithValue("@TimeDueForCompletion", task.TimeDueForCompletion);

                        sqlCommand.Parameters.Add("@response", SqlDbType.Int).Direction = ParameterDirection.Output;
                        try
                        {
                            sqlCon.Open();
                            sqlCommand.ExecuteNonQuery();
                            response = Convert.ToInt32(sqlCommand.Parameters["@response"].Value);

                            if (response > 0)
                            {
                                responseMessage = "Task updated Successfully";
                                isComplete = true;
                            }

                            else
                            {
                                responseMessage = "Failed to update task";
                            }
                        }
                        catch (Exception)
                        {

                            throw new Exception("Error Encountered while adding task, Please Try Again");
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("Error Encountered while adding task, Please Try Again");
                    }

                }
            }
            catch (Exception)
            {
                throw new Exception("Error Encountered while adding task, Please Try Again");
            }
            return isComplete;
        }



        public List<Tasks> GetAllTasks(string token,out string responseMessage)
        {
            int response = 0;
            List<Tasks> allTasks = new List<Tasks>();
            responseMessage = "";
            try
            {
                SqlConnection sqlCon = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand("GetAllTasks", sqlCon);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@token", token);
                sqlCommand.Parameters.Add("@response", SqlDbType.Int).Direction = ParameterDirection.Output;

                sqlCon.Open();
                sqlCommand.ExecuteNonQuery();
                response = Convert.ToInt32(sqlCommand.Parameters["@response"].Value);

                if (response > 0)
                {
                    responseMessage = "token exist";

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    da.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        try
                        {
                            Tasks task = new Tasks();

                            task.DateTimeCompleted = row["TimeCompleted"] == DBNull.Value ? "" : DateTime.Parse(row["TimeCompleted"].ToString()).ToString("dddd, dd MMMM yyyy hh:mm:ss tt");
                            task.DateTimeDueForCompletion = row["TimeDueForCompletion"] == DBNull.Value ? "" : DateTime.Parse(row["TimeDueForCompletion"].ToString()).ToString("dddd, dd MMMM yyyy hh:mm:ss tt");
                            task.Id = row["Id"] == DBNull.Value ? 0 : Int16.Parse(row["Id"].ToString());

                            task.IsComplete = row["IsCompleted"] == DBNull.Value ? false : Boolean.Parse(row["IsCompleted"].ToString());
                            task.TaskName = row["TaskName"] == DBNull.Value ? "" : row["TaskName"].ToString();

                            allTasks.Add(task);
                        }
                        catch (Exception e)
                        {

                        }

                    }
                }
                else
                {
                    responseMessage = "token doesnt exist";
                }
                   
            }
            catch (Exception e)
            {
                throw new Exception("Error Encountered while retreiving all tasks, Please Try Again");
            }
            return allTasks;
        }


        public List<Tasks> GetAllCompletedTasks(string token, out string responseMessage)
        {
            
            int response = 0;
            List<Tasks> allTasks = new List<Tasks>();
            responseMessage = "";
            try
            {
                SqlConnection sqlCon = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand("GetAllCompletedTasks", sqlCon);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@token", token);
                sqlCommand.Parameters.Add("@response", SqlDbType.Int).Direction = ParameterDirection.Output;

                sqlCon.Open();
                sqlCommand.ExecuteNonQuery();
                response = Convert.ToInt32(sqlCommand.Parameters["@response"].Value);

                if (response > 0)
                {
                    responseMessage = "token exist";

                    SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                    DataTable dataTable = new DataTable();
                    da.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        try
                        {
                            Tasks task = new Tasks();

                            task.DateTimeCompleted = row["TimeCompleted"] == DBNull.Value ? "" : DateTime.Parse(row["TimeCompleted"].ToString()).ToString("dddd, dd MMMM yyyy hh:mm:ss tt");
                            task.DateTimeDueForCompletion = row["TimeDueForCompletion"] == DBNull.Value ? "" : DateTime.Parse(row["TimeDueForCompletion"].ToString()).ToString("dddd, dd MMMM yyyy hh:mm:ss tt");
                            task.Id = row["Id"] == DBNull.Value ? 0 : Int16.Parse(row["Id"].ToString());

                            task.IsComplete = row["IsCompleted"] == DBNull.Value ? false : Boolean.Parse(row["IsCompleted"].ToString());
                            task.TaskName = row["TaskName"] == DBNull.Value ? "" : row["TaskName"].ToString();

                            allTasks.Add(task);
                        }
                        catch (Exception e)
                        {

                        }

                    }
                }
                else
                {
                    responseMessage = "token doesnt exist";
                }

            }
            catch (Exception e)
            {
                throw new Exception("Error Encountered while retreiving all tasks, Please Try Again");
            }
            return allTasks;
        }

    }
}
