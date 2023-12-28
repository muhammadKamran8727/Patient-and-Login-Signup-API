
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PatientApi.Models;

namespace PatientApi.IBusinessLogic
{
    public interface ILoginBL
    {
        Response CreateLogin(PatientLogin patientLogin);
        Response ForgetPassword(string email);
        Response GetUser();
        Response PutUser(PatientLogin patientLogin, long user_ID);
        Response DeleteUser(long user_ID);
        // For Login 
        Response LoginUser(string UserEmail, string UserPassword);
      
    }
}
