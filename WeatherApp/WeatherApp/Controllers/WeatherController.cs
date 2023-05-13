using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Numerics;
using WeatherApp.Models;
using WeatherApp.Utility;

namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        public WeatherController()
        {
            
        }


        /// <summary>
        /// This API return Coolest Area.take means that how many coolest area to show. by deafult 10 coolest area showing
        /// </summary>
        ///   /// <param name="take"></param>
        /// <returns></returns>

        [HttpGet("GetCoolestArea")]
        public IActionResult GetCoolestArea(int? take)
        {
            try
            {
                int getArea = Convert.ToInt32(take == null ? 10 : take);
                List<District> districts = UtilityManager.GetDistricts();
                foreach (var item in districts)
                {
                    CalculateTemperature calculateTemperature = UtilityManager.GetTemperatures(Convert.ToDouble(item.lat), Convert.ToDouble(item.Long));
                    item.TotalTemperature = calculateTemperature.TotalTemperature;
                    item.AvegTemp = calculateTemperature.AvegTemp;
                }

                return Ok(districts.OrderBy(e => e.AvegTemp).Take(getArea));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString(), isSucceed = false });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TravelDate"></param>
        /// <returns></returns>
        [HttpGet("GetTravelDestinationTemperature")]
       
        public IActionResult GetTravelDestinationTemperature([FromQuery] Travel query)
        {
            try
            {
                if (query.TravelDate==null)
                {
                    throw new Exception("Please Enter Formative Travel Date.");
                }
                List<District> districts = UtilityManager.GetDistricts();
                District district = districts.Where(e => e.name == query.FriendsLocation).FirstOrDefault();
                District destinationDis = districts.Where(e => e.name == query.FriendsDestination).FirstOrDefault();
                double? locationTemperature = UtilityManager.GetTemperaturesForTravel(Convert.ToDouble(district.lat), Convert.ToDouble(district.Long), Convert.ToDateTime(query.TravelDate));
                double? destinationTemperature = UtilityManager.GetTemperaturesForTravel(Convert.ToDouble(destinationDis.lat), Convert.ToDouble(destinationDis.Long), Convert.ToDateTime(query.TravelDate));

                if (locationTemperature > destinationTemperature)
                {
                    return Ok(new { message = "You Should Travel there.", LocationTemperature = locationTemperature, DestinationTemperature = destinationTemperature, });
                }
                else
                {
                    return Ok(new { message = "You Should not Travel there.", LocationTemperature = locationTemperature, DestinationTemperature = destinationTemperature, });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new {message=ex.Message.ToString(), isSucceed = false});
            }
            //return Ok(district);
        }

    }
}
