using Newtonsoft.Json;
using System.Collections.Generic;

namespace Weather_App
{
    public class WeatherForecast
    {
        [JsonProperty("cod")]
        public int Cod { get; set; }
        [JsonProperty("message")]
        public double Message { get; set; }
        [JsonProperty("cnt")]
        public int Count { get; set; }
        [JsonProperty("list")]
        public List<WeatherData> list{ get; set; }
        [JsonProperty("city")]
        public City City { get; set; }
    }
}
