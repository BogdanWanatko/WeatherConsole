using System;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace WeatherConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter latitude: ");
            var latitude = Console.ReadLine();
            Console.WriteLine("Enter longitude: ");
            var longitude = Console.ReadLine();

            var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true&hourly=temperature_2m,relativehumidity_2m,windspeed_10m";
            var request = WebRequest.Create(url);
            var response = request.GetResponse();
            var rawJson = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var json = JObject.Parse(rawJson);

            Console.WriteLine("Current weather:");
            Console.WriteLine("Time: " + json["current_weather"]["time"]);
            Console.WriteLine("Temperature: " + json["current_weather"]["temperature"] + "°C");
            Console.WriteLine("Weather code: " + json["current_weather"]["weathercode"]);
            Console.WriteLine("Wind speed: " + json["current_weather"]["windspeed"] + "m/s");
            Console.WriteLine("Wind direction: " + json["current_weather"]["winddirection"] + "°");

            Console.WriteLine("Hourly forecast:");
            var hourly = json["hourly"];
            for (int i = 0; i < hourly["time"].Count(); i++)
            {
                Console.WriteLine("Time: " + hourly["time"][i]);
                Console.WriteLine("Temperature: " + hourly["temperature_2m"][i] + "°C");
                Console.WriteLine("Relative humidity: " + hourly["relativehumidity_2m"][i] + "%");
                Console.WriteLine("Wind speed: " + hourly["windspeed_10m"][i] + "m/s");
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
