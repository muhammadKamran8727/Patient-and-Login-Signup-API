using Azure.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PatientApi.IBusinessLogic;
using PatientApi.Models;
using System;
using System.Data;

namespace PatientApi.BusinessLogic
{
    public class PatientBL : IPatientBL
    {
        private readonly PBase _dbContext;
        public PatientBL(PBase dbContext)
        {
            _dbContext = dbContext;

        }

        // -------------------------Get a specific User By ID---------------------------------//

        public Response GetUserById(long user_ID)
        {
            try
            {
                var Getuser = _dbContext.Patient_Login.Where(p => p.User_ID == user_ID).FirstOrDefault();

                if (Getuser != null)
                {
                    return new Response { IsError = false, Message = "Your Required Patient Successfully Get!", Result = Getuser };
                }
                return new Response { IsError = false, Message = "No Patient Available With This ID" };


            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }

        }

        // ---------------Get  a User  by filling any params---------


        public Response GetUserByAnyParams(long? user_ID = null, string? FirstName = null, string? LastName = null, string? UserEmail = null, string? UserContact = null, string? UserPassword = null)
        {
            try
            {
                var response = _dbContext.Patient_Login.ToList();
                if (response != null)
                {
                    if (!string.IsNullOrEmpty(user_ID.ToString()))
                    {
                        response = response.Where(r => r.User_ID == user_ID).ToList();
                    }
                    if (!string.IsNullOrEmpty(FirstName))
                    {
                        response = response.Where(r => r.First_Name == FirstName).ToList();
                    }
                    if (!string.IsNullOrEmpty(LastName))
                    {
                        response = response.Where(r => r.Last_Name == LastName).ToList();
                    }
                    if (!string.IsNullOrEmpty(UserEmail))
                    {
                        response = response.Where(r => r.User_Email == UserEmail).ToList();
                    }
                    if (!string.IsNullOrEmpty(UserContact))
                    {
                        response = response.Where(r => r.User_Contact == UserContact).ToList();
                    }
                    if (!string.IsNullOrEmpty(UserPassword))
                    {
                        response = response.Where(r => r.User_Password == UserPassword).ToList();
                    }
                    return new Response { IsError = false, Message = "Your Required Patient Successfully Get!", Result = response };
                }
                return new Response { IsError = false, Message = "No Patient Available With This ID" };


            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }

        }


        // Get Methd by Stored Procedure

        public Response GetPatientByAllParams(long? user_ID = null, string? FirstName = null, string? LastName = null, string? UserEmail = null, string? UserContact = null, string? UserPassword = null)
        {
            try
            {
                var response = _dbContext.Database.GetDbConnection().CreateCommand();
                response.CommandText = "[dbo].[GetPatientByAllParams]";
                response.CommandType = CommandType.StoredProcedure;
                response.Parameters.Add(new SqlParameter("@User_Id", user_ID));
                response.Parameters.Add(new SqlParameter("@First_name", FirstName));
                response.Parameters.Add(new SqlParameter("@Last_name", LastName));
                response.Parameters.Add(new SqlParameter("@User_Email", UserEmail));
                response.Parameters.Add(new SqlParameter("@User_contact", UserContact));
                response.Parameters.Add(new SqlParameter("@User_password", UserPassword));
                SqlDataAdapter da = new SqlDataAdapter(); //adapter
                DataSet ds = new DataSet(); //dataset
                da = new SqlDataAdapter((SqlCommand)response);
                da.Fill(ds);  //fill dataset with multiple select
                              // Check if the DataSet has data
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // Convert the DataTable to a list of objects or any other format you want
                    var data = ds.Tables[0].AsEnumerable().Select(row => new
                    {
                        User_Id = row["User_Id"],
                        First_name = row["First_name"],
                        Last_name = row["Last_name"],
                        User_Email = row["User_Email"],
                        User_contact = row["User_contact"],
                        User_password = row["User_password"],
                        
                    }).ToList();

                    return new Response { IsError = false, Message = "Your Required Patient Successfully Get!", Result = data };
                }
                else
                {
                   
                    return new Response { IsError = false, Message = "No Patient Available With This ID" };
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return new Response { IsError = true, Message = ex.Message };
            }
        }


    }
}
