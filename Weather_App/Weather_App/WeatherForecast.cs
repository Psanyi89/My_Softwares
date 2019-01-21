using Newtonsoft.Json;

namespace Weather_App
{
    public class WeatherForecast
    {

        [JsonProperty("message")]
        public double message { get; set; }
        [JsonProperty("cnt")]
        public int count { get; set; }


    }
}
