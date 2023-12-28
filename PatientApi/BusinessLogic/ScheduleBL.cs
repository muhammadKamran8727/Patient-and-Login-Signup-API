using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration.UserSecrets;
using PatientApi.IBusinessLogic;
using PatientApi.Models;
using System.Collections.Generic;

namespace PatientApi.BusinessLogic
{
    public class ScheduleBL : IScheduleBL
    {
        private readonly PBase _dbContext;

        public ScheduleBL(PBase dbContext)
        {
            _dbContext = dbContext;

        }


        //------------HTTP Get----------------------

        public Response ADDAppoinmnet(appoinment_schedule appoinmetnschedule)
        {
            try
            {
                var newCreate = new Schedule
                {
                    User_ID = appoinmetnschedule.User_Id,
                    Patient_Name = appoinmetnschedule.Patient_Name,
                    Doctor_Name = appoinmetnschedule.Doctor_Name,
                    Patient_Contact = appoinmetnschedule.Patient_Contact,
                    Appointment_Status = appoinmetnschedule.Appointment_Status,
                    Appoinment_Date_Time = appoinmetnschedule.Appoinment_Date_Time
                };
                _dbContext.Schedule.Add(newCreate);
                _dbContext.SaveChanges();

                return new Response { IsError = false, Message = "Appoinment Successfully Given", Result = newCreate };
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }

        }
        // Get User
        public Response GetUser()
        {
            try
            {
                var login = _dbContext.Schedule.Select(p => new Schedule
                {
                    Serial_No = p.Serial_No,
                    User_ID = p.User_ID,
                    Patient_Name = p.Patient_Name,
                    Doctor_Name = p.Doctor_Name,
                    Patient_Contact = p.Patient_Contact,
                    Appointment_Status = p.Appointment_Status,
                    Appoinment_Date_Time = p.Appoinment_Date_Time,


                }).ToList();
                return new Response { IsError = false, Message = "User Registered successfully!", Result = login };
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }

        }

        // -------------------------Get a specific User By ID---------------------------------//
        
        public Response GetUserById(long user_ID)
        {
            try
            {

                var Getuser = _dbContext.Schedule.Where(p => p.User_ID == user_ID).DefaultIfEmpty().ToList<dynamic>();
                var Patient = _dbContext.Patient_Login.Where(pa => pa.User_ID == user_ID).DefaultIfEmpty().ToList<dynamic>();

                var Reports = new Dictionary<string, List<dynamic>>
                {
                     {"Schedule",Getuser},
                     {"Patient",Patient}
                };
                return new Response { IsError = false, Message = "Your Required Patient Successfully Get!", Result = Reports };
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }

        }


        // Update User

        public Response UpdateUser(appoinment_schedule appoinmetnschedule, long serial_No)
        {
            try
            {
                var da = _dbContext.Schedule.Where(p => p.Serial_No == serial_No).FirstOrDefault();
                if (da != null)
                {

                    da.Patient_Name = appoinmetnschedule.Patient_Name;
                    da.Doctor_Name = appoinmetnschedule.Doctor_Name;
                    da.Patient_Contact = appoinmetnschedule.Patient_Contact;
                    da.Appointment_Status = appoinmetnschedule.Appointment_Status;
                    da.Appoinment_Date_Time = appoinmetnschedule.Appoinment_Date_Time;

                    _dbContext.Schedule.Update(da);
                    _dbContext.SaveChanges();
                    return new Response { IsError = false, Message = "User Updated successfully!", Result = da };
                }
                return new Response { IsError = false, Message = "User Not Found", Result = null };
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
        }


        //Delete a USer

        public Response DeleteUser(long serial_No)
        {
            try
            {
                var del = _dbContext.Schedule.Where(p => p.Serial_No == serial_No).FirstOrDefault();
                if (del != null)
                {
                    _dbContext.Schedule.Remove(del);
                    _dbContext.SaveChanges();
                    return new Response { IsError = false, Message = "Data Deleted successfully!", Result = del };
                }
                return new Response { IsError = false, Message = "Invalid ID", Result = del };


            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
        }

    }
}
