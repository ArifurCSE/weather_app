namespace WeatherApp.Models
{
    public class ApiResponse
    {
        public List<District> districts { get; set; }
    }
    public class District : CalculateTemperature
    {
        public string id { get; set; }
        public string division_id { get; set; }
        public string name { get; set; }
        public string bn_name { get; set; }
        public string lat { get; set; }
        public string Long { get; set; } 
    }
}
