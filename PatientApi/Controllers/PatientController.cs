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
    public class PatientController : ControllerBase
    {
        private readonly PBase _dbContext;
        private readonly IPatientBL _ipatientBL;
        public PatientController(PBase dbContext, IPatientBL ipatientBL)
        {
            _dbContext = dbContext;
            _ipatientBL = ipatientBL ;
        }

        // -------------------------Get a specific User By ID---------------------------------//
        [HttpGet("GetUserByID")]
        public ApiResponse GetUserById(long user_ID)
        {
            try
            {
                var response = _ipatientBL.GetUserById(user_ID);
                if (response != null)
                    return new ApiResponse { StatusCode = 200, Message = response.Message, Result = response.Result };

                return new ApiResponse { StatusCode = 404, Message = "Error Not Found!",};


            }

            catch (Exception ex)
            {
                return new ApiResponse { StatusCode = 401, Message = ex.Message };
            }

        }

        // ---------------------- Giving any params search records or get------------------------

        [HttpGet("GetUserByAnyParameter")]
        public ApiResponse GetUserByAnyParams(long? user_ID=null, string? FirstName = null, string? LastName = null, string? UserEmail = null, string? UserContact = null, string? UserPassword = null)
        {
            try
            {
                var response = _ipatientBL.GetUserByAnyParams(user_ID, FirstName, LastName, UserEmail, UserContact, UserPassword);
                if (response != null)
                {
                    return new ApiResponse { StatusCode = 200, Message = response.Message, Result = response.Result };
                }
                return new ApiResponse { StatusCode = 401, Message = response.Message };


            }

            catch (Exception ex)
            {
                return new ApiResponse { StatusCode = 401, Message = ex.Message };
            }
        }




        // Get method For Stored Procedure

        [HttpGet("GetPatientByAllParams")]
        public ApiResponse GetPatientByAllParams(long? user_ID = null, string? FirstName = null, string? LastName = null, string? UserEmail = null, string? UserContact = null, string? UserPassword = null)
        {
            try
            {
                var response = _ipatientBL.GetPatientByAllParams(user_ID, FirstName, LastName, UserEmail, UserContact, UserPassword);
                if (response != null)
                {
                    return new ApiResponse { StatusCode = 200, Message = response.Message, Result = response.Result };
                }
                return new ApiResponse { StatusCode = 401, Message = response.Message };


            }

            catch (Exception ex)
            {
                return new ApiResponse { StatusCode = 401, Message = ex.Message };
            }
        }
    }
}
