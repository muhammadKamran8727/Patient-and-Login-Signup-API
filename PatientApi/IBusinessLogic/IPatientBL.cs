using PatientApi.Models;

namespace PatientApi.IBusinessLogic
{
    public interface IPatientBL
    {
        // -------------------------Get a specific User By ID---------------------------------//
        Response GetUserById(long user_ID);
        Response GetUserByAnyParams(long? user_ID = null, string? FirstName = null, string? LastName = null, string? UserEmail = null, string? UserContact = null, string? UserPassword = null);

        Response GetPatientByAllParams(long? user_ID = null, string? FirstName = null, string? LastName = null, string? UserEmail = null, string? UserContact = null, string? UserPassword = null);
    }
}
