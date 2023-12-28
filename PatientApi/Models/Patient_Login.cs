using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PatientApi.Models
{
    public class Patient_Login
    {
        public Patient_Login() 
        {
            Schedule = new HashSet<Schedule>();
        }
        [Key]
        public long User_ID { get; set;}
        public string? First_Name { get; set; }
        public string? Last_Name { get; set;}    
        public string? User_Email { get; set;}
        public string? User_Contact { get; set;}
        public string? User_Password { get; set;}
        public string? Role{ get; set;}
        [JsonIgnore]
        public virtual ICollection<Schedule> Schedule { get; set; }

    }
}
