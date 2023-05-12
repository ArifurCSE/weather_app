using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Numerics;
using WeatherApp.Models;
using WeatherApp.Utility;

namespace WeatherApp.Interfaces
{
    public class DistrictsParameterFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (parameter.Name.Equals("FriendsLocation", StringComparison.InvariantCultureIgnoreCase) || parameter.Name.Equals("FriendsDestination", StringComparison.InvariantCultureIgnoreCase))
            {
                List<District> districts = UtilityManager.GetDistricts();

                parameter.Schema.Enum = districts.Select(p => new OpenApiString(p.name)).ToList<IOpenApiAny>();

                //using (var scope = _serviceScopeFactory.CreateScope())
                //{
                //    var planetsContext = scope.ServiceProvider.GetRequiredService<Database1Context>();
                //    IEnumerable<Planet> planets = planetsContext.Planets.ToArray();



                //}
            }
        }
    }
}
