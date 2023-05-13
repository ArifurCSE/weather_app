
# Weather API Application (Coolest Area & Choose Travel Destination)

Firstly, you may show "GetCoolestArea" API for the coolest 10 districts (by default) based on the average temperature at 2pm for the next 7 days.If you want you may change 10 and you may input with take variable more than 10.  
Secondly, you may show "GetTravelDestinationTemperature" API you take your friend's location, their destination, and the date of travel. This on will be shown the temperature of those two locations at 2 PM on that day and return a response deciding if they should travel there or not.

## Installation

Install WeatherApp with Visual Studio 2022 Software.
Visual Studio 2022 is the most comprehensive Integrated Development Environment (IDE) for .NET and C++ developers on Windows for building web, cloud, desktop, mobile apps, services and games.

## Weather App Build & Run
using Visual Studio 2022 software First Build press the (Ctrl+Shift+B) and run weather app press the (F5).Then you will be shown GUI which is the Swagger UI as like Postman. Firstly, you expand your REST API and press the Try it Out(Right side) Button.After that you may show Execute buton.

## API Reference

#### Get By default 10 coolest Area

```http
  GET /api/Weather/GetCoolestArea
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `take` | `int` | Not mandatory |

#### Get Travel Destination Area

```http
  GET /api/Weather/GetTravelDestinationTemperature
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `FriendsLocation`      | `string` | **Required**. you may select from dropdown |
| `FriendsDestination`      | `string` | **Required**. you may select from dropdown |
| `TravelDate`      | `string` | **Required**. you may input your Travel Date Exact format (e.g. "yyyy-MM-dd") |


