namespace PatientApi.Models
{
    public class appoinment_schedule
    {
        public long User_Id { get; set; }
        public string? Patient_Name { get; set; }
        public string? Doctor_Name { get; set; }
        public string? Patient_Contact { get; set; }
        public string? Appointment_Status { get; set; }
        public DateTime? Appoinment_Date_Time { get; set; }
    }
}
