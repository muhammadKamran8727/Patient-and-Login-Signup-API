namespace PatientApi.Models
{
    public class PatientLogin
    {
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }

        public string? User_Email { get; set; }
        public string? User_Contact { get; set; }
        public string? User_Password { get; set; }
        public string? Role { get; set; }
    }
}
