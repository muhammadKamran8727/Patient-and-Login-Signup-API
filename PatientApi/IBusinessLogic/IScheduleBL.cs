using Azure;
using Microsoft.AspNetCore.Mvc;
using PatientApi.Models;
using Response = PatientApi.Models.Response;

namespace PatientApi.IBusinessLogic
{
    public interface IScheduleBL
    {
        Response ADDAppoinmnet(appoinment_schedule appoinmetnschedule);
        Response GetUser();
        Response UpdateUser(appoinment_schedule appoinmetnschedule, long serial_No);
        Response DeleteUser(long serial_No);
        Response GetUserById(long user_ID);

    }
}
