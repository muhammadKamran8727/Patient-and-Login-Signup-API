using AutoWrapper.Wrappers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using PatientApi.IBusinessLogic;
using PatientApi.Models;
using System.Diagnostics.Eventing.Reader;
using System.Net.Mail;

namespace PatientApi.BusinessLogic
{
    public class LoginBL : ILoginBL
    {
        private readonly PBase _dbContext;
        public LoginBL(PBase dbContext)
        {
            _dbContext = dbContext;

        }


        public Response ForgetPassword(string email)
        {
            try
            {
                var existingUser = _dbContext.Patient_Login.FirstOrDefault(u => u.User_Email == email);

                if (existingUser == null)
                {
                    return new Response { Message = "User Not Found" };
                }

                // Generate a reset token (for demonstration purposes, generate a simple token)
                string resetToken = Guid.NewGuid().ToString(); // Generate a unique token

                // In a real scenario, you'd store this resetToken in the database associated with the user

                // Send an email to the user with the reset token
                SendResetPasswordEmail(existingUser.User_Email, resetToken);

                return new Response { Message = "Password reset link sent to your email", Result=resetToken };
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
        }

        private void SendResetPasswordEmail(string email, string resetToken)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("mkamran8727419@gmail.com");
                    mail.To.Add(email);
                    mail.Subject = "Password Reset";
                    mail.Body = $"Please click the following link to reset your password:https://localhost:7258/swagger/index.html :{resetToken}";

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
                    {
                        smtp.Port = 587;
                        smtp.Credentials = new System.Net.NetworkCredential("mkamran8727419@gmail.com", "ccwr telt nqok bwhh");
                        smtp.EnableSsl = true;

                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending password reset email: " + ex.Message);
            }
        }






        public Response CreateLogin(PatientLogin patientLogin)
        {
            try
            {
                var login = _dbContext.Patient_Login.Where(p => p.User_Email == patientLogin.User_Email).FirstOrDefault();
                if (login != null)
                {
                    if (login.User_Email == patientLogin.User_Email)
                    {
                        return new Response { IsError = false, Message = "Email is Already Exist", Result = login };

                    }
                    else
                    {
                        var newCreate = new Patient_Login
                        {
                            First_Name = patientLogin.First_Name,
                            Last_Name = patientLogin.Last_Name,

                            User_Email = patientLogin.User_Email,
                            User_Contact = patientLogin.User_Contact,
                            User_Password = patientLogin.User_Password,
                            Role = patientLogin.Role,

                        };
                        _dbContext.Patient_Login.Add(newCreate);
                        _dbContext.SaveChanges();
                        return new Response { IsError = false, Message = "User Registered successfully!", Result = newCreate };

                    }
                }
                var newLogin = new Patient_Login
                {
                    First_Name = patientLogin.First_Name,
                    Last_Name = patientLogin.Last_Name,

                    User_Email = patientLogin.User_Email,
                    User_Contact = patientLogin.User_Contact,
                    User_Password = patientLogin.User_Password,
                    Role = patientLogin.Role,

                };
                _dbContext.Patient_Login.Add(newLogin);
                _dbContext.SaveChanges();
                return new Response { IsError = false, Message = "User Registered successfully!", Result = newLogin };


            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }
        }
        // GetUer

        public Response GetUser()
        {
            try
            {
                var login = _dbContext.Patient_Login.Select(p => new Patient_Login
                {
                    User_ID = p.User_ID,
                    First_Name = p.First_Name,
                    Last_Name = p.Last_Name,
                    User_Email = p.User_Email,
                    User_Contact = p.User_Contact,
                    User_Password = p.User_Password,
                    Role = p.Role,

                }).ToList();
                return new Response { IsError = false, Message = "User Registered successfully!", Result = login };
            }
            catch (Exception ex)
            {
                return new Response { IsError = true, Message = ex.Message };
            }

        }


        // Login User Method

        public Response LoginUser(string UserEmail, string UserPassword)
        {
            try
            {
                var LogUser = _dbContext.Patient_Login.Where(p => p.User_Email == UserEmail && p.User_Password == UserPassword).FirstOrDefault();

                if (LogUser != null)
                {
                    return new Response { IsError = false, Message = "User Login successfully!", Result = LogUser };
                }
                return new Response { IsError = false, Message = "No User Available With Email!", Result = LogUser, };


            }

            catch (Exception ex)
            {
                
                    return new Response { IsError = true, Message = ex.Message };
                

            }


        }

            // Update User

            public Response PutUser([FromBody] PatientLogin patientLogin, long user_ID)
            {
                try
                {
                    var da = _dbContext.Patient_Login.Where(p => p.User_ID == user_ID).FirstOrDefault();
                    if (da != null)
                    {

                        da.First_Name = patientLogin.First_Name;
                        da.Last_Name = patientLogin.Last_Name;
                        da.User_Email = patientLogin.User_Email;
                        da.User_Contact = patientLogin.User_Contact;
                        da.User_Password = patientLogin.User_Password;
                        da.Role = patientLogin.Role;
                        _dbContext.Patient_Login.Update(da);
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

            public Response DeleteUser(long user_ID)
            {
                try
                {
                    var del = _dbContext.Patient_Login.Where(p => p.User_ID == user_ID).FirstOrDefault();
                    if (del != null)
                    {
                        _dbContext.Patient_Login.Remove(del);
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

