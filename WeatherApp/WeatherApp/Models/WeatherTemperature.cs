namespace WeatherApp.Models
{
    public class CalculateTemperature
    {
        public double? TotalTemperature { get; set; }
        public double? AvegTemp { get; set; }

    }
    public class WeatherTemperature
    {
        public DateTime DateTemp { get; set; }
        public decimal Temperature { get; set; }
    }
}
