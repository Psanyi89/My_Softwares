using Newtonsoft.Json;

namespace Weather_App
{
    public class City
    {
        //[JsonProperty("id")]
        //public int id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("country")]
        public string country { get; set; }
        [JsonProperty("coord")]
        public Coord coord { get; set; }
        [JsonProperty("population")]
        public int population { get; set; }
        public class Coord
        {
            [JsonProperty("lat")]
            public double lat { get; set; }
            [JsonProperty("lon")]
            public double lon { get; set; }
        }
    }

}
