using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WeatherApp.Models
{
    public class Travel
    {
        [Required]
        public string FriendsLocation { get; set; }
        [Required]
        public string FriendsDestination { get; set; }

        //[Example("2023-06-01")]
        public DateTime? TravelDate { get; set; }
    }
}
