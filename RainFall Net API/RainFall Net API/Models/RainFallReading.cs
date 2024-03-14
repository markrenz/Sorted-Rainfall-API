namespace RainFall_Net_API.Models
{
    public class RainFallReading
    {
        public string id { get; set; }
        public DateTime dateTime { get; set; }
        public string measure { get; set; }
        public decimal value { get; set; }
    }
}
