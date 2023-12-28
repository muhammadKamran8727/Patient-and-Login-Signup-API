namespace PatientApi.Models
{
    public class Response
    {
        int Status { get; set; }
        public object Result { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
        public bool IsError { get; set; }
    }
}
