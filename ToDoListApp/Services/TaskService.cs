﻿using Microsoft.Data.SqlClient;
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
        public bool AddTask(CreateTaskModel task, out string responseMessage)
        {
            string token = "";
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


        public List<Tasks> GetAllTasks(out string responseMessage)
        {
            string token = "";
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

                            task.DateTimeCompleted = row["DateTimeCompleted"] == DBNull.Value ? "" : DateTime.Parse(row["DateTimeCompleted"].ToString()).ToString("dddd, dd MMMM yyyy");
                            task.DateTimeDueForCompletion = row["DateTimeDueForCompletion"] == DBNull.Value ? "" : DateTime.Parse(row["DateTimeDueForCompletion"].ToString()).ToString("dddd, dd MMMM yyyy");
                            task.Id = row["Id"] == DBNull.Value ? 0 : Int16.Parse(row["Id"].ToString());

                            task.IsComplete = row["IsComplete"] == DBNull.Value ? false : Boolean.Parse(row["IsComplete"].ToString());
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


        public List<Tasks> GetAllCompletedTasks(out string responseMessage)
        {
            string token = "";
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

                            task.DateTimeCompleted = row["DateTimeCompleted"] == DBNull.Value ? "" : DateTime.Parse(row["DateTimeCompleted"].ToString()).ToString("dddd, dd MMMM yyyy");
                            task.DateTimeDueForCompletion = row["DateTimeDueForCompletion"] == DBNull.Value ? "" : DateTime.Parse(row["DateTimeDueForCompletion"].ToString()).ToString("dddd, dd MMMM yyyy");
                            task.Id = row["Id"] == DBNull.Value ? 0 : Int16.Parse(row["Id"].ToString());

                            task.IsComplete = row["IsComplete"] == DBNull.Value ? false : Boolean.Parse(row["IsComplete"].ToString());
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
