using Newtonsoft.Json;
using System.Collections.Generic;

namespace Weather_App
{
    public class WeatherForecast
    {
        [JsonProperty("cod")]
        public int cod { get; set; }
        [JsonProperty("message")]
        public double message { get; set; }
        [JsonProperty("cnt")]
        public int count { get; set; }
        [JsonProperty("list")]
        public List<WeatherData> list{ get; set; }
        public City city { get; set; }
    }
}
