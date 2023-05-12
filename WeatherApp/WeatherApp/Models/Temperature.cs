namespace WeatherApp.Models
{
    public class Temperature
    {
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public decimal elevation { get; set; }
        public Hourly hourly { get; set; }

    }
    public class Hourly
    {
        public string[] time { get; set; }
        public double?[] temperature_2m { get; set; }
    }
}
