using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PatientApi.Models
{
    public class Schedule
    {
        [Key]
        public long Serial_No {  get; set; }
        public long User_ID {  get; set; }
        public string? Patient_Name { get; set; }
        public string? Doctor_Name { get; set; }
        public string? Patient_Contact { get; set; }
        public string? Appointment_Status { get; set; }
        public DateTime? Appoinment_Date_Time { get; set; }
        [JsonIgnore]
        public virtual Patient_Login? Patient_Login {  get; set; }
    }
}
