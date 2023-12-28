using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientApi.BusinessLogic;
using PatientApi.IBusinessLogic;
using PatientApi.Models;

namespace PatientApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Appoinment_ScheduleController : ControllerBase
    {
        private readonly PBase _dbContext;
        private readonly IScheduleBL _ischeduleBL;
        public Appoinment_ScheduleController(PBase dbContext, IScheduleBL ischeduleBL)
        {
            _dbContext = dbContext;
            _ischeduleBL = ischeduleBL;
        }

        //---------------------HttpPost-------------------------
        [HttpPost("AddAppoinment")]

        public ApiResponse ADDAppoinmnet([FromBody] appoinment_schedule appoinmetnschedule)
        {
            try
            {
                var response = _ischeduleBL.ADDAppoinmnet(appoinmetnschedule);
                if (response != null && response.IsError == false)
                    return new ApiResponse { StatusCode = 200, Message = response.Message };

                return new ApiResponse { StatusCode = 500, Message = response.Message, Result = response.Result };
            }
            catch (Exception ex)
            {
                return new ApiResponse { StatusCode = 401, Message = ex.Message };
            }
        }

        // -------------------------GET USER Method---------------------------------//
        [HttpGet("GetUser")]
        public ApiResponse GetUser()
        {
            try
            {
                var response = _ischeduleBL.GetUser();
                if (response != null)
                    return new ApiResponse { StatusCode = 200, Message = "Success", Result = response.Result };

                return new ApiResponse { StatusCode = 404, Message = "Error Not Found!", Result = response.Result };


            }

            catch (Exception ex)
            {
                return new ApiResponse { StatusCode = 401, Message = ex.Message };
            }

        }

        // -------------------------Get a specific User By ID---------------------------------//
        [HttpGet("GetUserByID")]
        public ApiResponse GetUserById(long user_ID)
        {
            try
            {
                var response = _ischeduleBL.GetUserById(user_ID);
                if (response != null)
                    return new ApiResponse { StatusCode = 200, Message = response.Message, Result = response.Result };

                return new ApiResponse { StatusCode = 404, Message = "Error Not Found!", };


            }

            catch (Exception ex)
            {
                return new ApiResponse { StatusCode = 401, Message = ex.Message };
            }

        }
        // -------------------------Updation of User Method---------------------------------//
        [HttpPut("UpdateUser")]
        public ApiResponse UpdateUser([FromBody] appoinment_schedule appoinmetnschedule, long serial_No)
        {
            try
            {
                var response = _ischeduleBL.UpdateUser(appoinmetnschedule, serial_No);
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
        public ApiResponse DeleteUser(long serial_No)
        {
            try
            {
                var response = _ischeduleBL.DeleteUser(serial_No);
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
