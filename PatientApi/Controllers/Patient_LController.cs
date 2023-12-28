using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using PatientApi.IBusinessLogic;
using PatientApi.Models;


namespace PatientApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Patient_LController : ControllerBase
    {
        private readonly PBase _dbContext;
        private readonly ILoginBL _iloginBL;
        public Patient_LController(PBase dbContext, ILoginBL iloginBL)
        {
            _dbContext = dbContext;
            _iloginBL = iloginBL;
        }

        // ------------------------Forget Password Method-----------------------------//
        [HttpPost("ForgetPassword")]
        public ApiResponse ForgetPassword(string email)
        {
            try
            {
                var response = _iloginBL.ForgetPassword(email);
                if (response != null && response.IsError == false)
                    return new ApiResponse { StatusCode = 200, Message = response.Message };

                return new ApiResponse { StatusCode = 500, Message = response.Message, Result = response.Result };
            }
            catch (Exception ex)
            {
                return new ApiResponse { StatusCode = 401, Message = ex.Message };
            }
        }









        // ------------------------CREATE USER Method-----------------------------//
        [HttpPost("CreateLogin")]
        public ApiResponse CreateLogin([FromBody] PatientLogin patientLogin)
        {
            try
            {
                var response = _iloginBL.CreateLogin(patientLogin);
                if (response != null && response.IsError == false)
                    return new ApiResponse { StatusCode = 200, Message = response.Message };

                return new ApiResponse { StatusCode = 500, Message = response.Message, Result = response.Result };
            }
            catch (Exception ex)
            {
                return new ApiResponse { StatusCode = 401, Message =ex.Message };
            }
        }

        // -------------------------GET USER Method---------------------------------//
        [HttpGet("GetUser")]
        public ApiResponse GetUser()
        {
            try
            {
                var response = _iloginBL.GetUser();
                if (response != null)
                    return new ApiResponse { StatusCode = 200, Message = "Success", Result = response.Result };

                return new ApiResponse { StatusCode = 404, Message = "Error Not Found!", Result = response.Result };


            }

            catch (Exception ex)
            {
                return new ApiResponse { StatusCode = 401, Message = ex.Message };
            }

        }

           // -------------------------Login User Method---------------------------------//
        [HttpGet("Login")]
        public ApiResponse LoginUser(string UserEmail,string UserPassword)
        {
            try
            {
                var response = _iloginBL.LoginUser(UserEmail,  UserPassword);
                if (response != null)
                {
                    return new ApiResponse { StatusCode = 200, Message = response.Message, Result = response.Result };
                }
                return new ApiResponse { StatusCode = 401, Message = response.Message};


            }

            catch (Exception ex)
            {
                return new ApiResponse { StatusCode = 401, Message = ex.Message };
            }

        }

       


            // -------------------------Updation of User Method---------------------------------//
            [HttpPut("UpdateUser")]
        public ApiResponse PutUser([FromBody] PatientLogin patientLogin,long user_ID)
        {
            try
            {
                var response = _iloginBL.PutUser(patientLogin,user_ID);
                if (response == null)
                {
                    return new ApiResponse { StatusCode = 401, Message = response.Message, Result = response.Result };
                }
                return new ApiResponse { StatusCode = 200, Message = response.Message, Result = response.Result };
            }
            catch (Exception ex)
            {
                return new ApiResponse { StatusCode = 401, Message = ex.Message };
            }
        }


        // -------------------------Delete a User Method---------------------------------//
        // Delete Method
        [HttpDelete("DeleteUser")]
        public ApiResponse DeleteUser(long user_ID)
        {
            try
            {
                var response = _iloginBL.DeleteUser(user_ID);
                if (response == null)
                {
                    return new ApiResponse { StatusCode = 404, Message = response.Message };
                }
                return new ApiResponse { StatusCode = 200, Message = response.Message, Result = response.Result };
            }
            catch (Exception ex)
            {
                return new ApiResponse { StatusCode = 401, Message = ex.Message };
            }
        }

    }
}
