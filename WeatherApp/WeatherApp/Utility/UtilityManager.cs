using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using WeatherApp.Models;

namespace WeatherApp.Utility
{
    public  class UtilityManager
    {
        public static List<District> GetDistricts()
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                using (var client = new HttpClient())
                {
                    var baseAddress = "https://raw.githubusercontent.com/strativ-dev/technical-screening-test/"; 
                    client.Timeout = TimeSpan.FromMinutes(20000);
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var postTask = client.GetAsync("main/bd-districts.json");
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        apiResponse = JsonConvert.DeserializeObject<ApiResponse>(result.Content.ReadAsStringAsync().Result);
                      
                    }
                    else
                    {

                    }
                  
                }
                return apiResponse.districts.OrderBy(e=>e.name).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        public static CalculateTemperature GetTemperatures(double latitude,double longitude)
        {
            Temperature apiResponse = new Temperature();
            CalculateTemperature calculateTemperature = new CalculateTemperature();
            try
            {
                using (var client = new HttpClient())
                {
                    var baseAddress = "https://api.open-meteo.com/v1/";
                    client.Timeout = TimeSpan.FromMinutes(20000);
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var postTask = client.GetAsync($"forecast?latitude={latitude}&longitude={longitude}&hourly=temperature_2m");
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        //List<WeatherTemperature> weathers = new List<WeatherTemperature>();
                        apiResponse = JsonConvert.DeserializeObject<Temperature>(result.Content.ReadAsStringAsync().Result);
                        double? totalTemp = 0;
                        //for (int i = 0; i < apiResponse.hourly.time.Length; i++)
                        //{
                        //    weathers.Add(new WeatherTemperature()
                        //    {
                        //        DateTemp = Convert.ToDateTime(apiResponse.hourly.time[i]),
                        //        Temperature = apiResponse.hourly.temperature_2m[i]
                        //    });
                        //}
                        for (int i = 0; i < apiResponse.hourly.time.Length; i++)
                        {
                            if (Convert.ToDateTime(apiResponse.hourly.time[i]).Hour == 14)
                            {
                                totalTemp += apiResponse.hourly.temperature_2m[i];
                            }
                        }

                        //foreach (var item in weathers)
                        //{                         
                        //    if (item.DateTemp.Hour==14)
                        //    {
                        //        totalTemp += item.Temperature;
                        //    }
                        //}
                        calculateTemperature.TotalTemperature = totalTemp;
                        calculateTemperature.AvegTemp = (totalTemp / 7);

                    }
                    else
                    {

                    }

                }
                return calculateTemperature;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static double? GetTemperaturesForTravel(double latitude, double longitude,DateTime travelDate)
        {
            Temperature apiResponse = new Temperature();
         
            double? totalTemp = 0;
            try
            {
                using (var client = new HttpClient())
                {
                    var baseAddress = "https://api.open-meteo.com/v1/";
                    client.Timeout = TimeSpan.FromMinutes(20000);
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string pathurl = $"forecast?latitude={latitude}&longitude={longitude}&hourly=temperature_2m&start_date={travelDate.Date.ToString("yyyy-MM-dd")}&end_date={travelDate.Date.ToString("yyyy-MM-dd")}";
                    var postTask = client.GetAsync(pathurl);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        //List<WeatherTemperature> weathers = new List<WeatherTemperature>();
                        apiResponse = JsonConvert.DeserializeObject<Temperature>(result.Content.ReadAsStringAsync().Result);
            
                        //for (int i = 0; i < apiResponse.hourly.time.Length; i++)
                        //{
                        //    weathers.Add(new WeatherTemperature()
                        //    {
                        //        DateTemp = Convert.ToDateTime(apiResponse.hourly.time[i]),
                        //        Temperature = apiResponse.hourly.temperature_2m[i]
                        //    });
                        //}
                        for (int i = 0; i < apiResponse.hourly.time.Length; i++)
                        {
                            if (Convert.ToDateTime(apiResponse.hourly.time[i]).Hour == 14 && Convert.ToDateTime(apiResponse.hourly.time[i]).Date == travelDate.Date)
                            {
                                if (apiResponse.hourly.temperature_2m[i]!=null)
                                {
                                    totalTemp = apiResponse.hourly.temperature_2m[i];
                                }
                                else
                                {
                                    throw new Exception($"Temperature not found. please check below the url {baseAddress+pathurl}");
                                }


                            }
                        }


                    }
                    else
                    {

                    }

                }
                return totalTemp;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
