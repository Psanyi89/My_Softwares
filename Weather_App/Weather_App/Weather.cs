using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Weather_App
{
    public class WeatherData
    {
        [JsonProperty("coord")]
        public Coord coord { get; set; }
        [JsonProperty("weather")]
        public List<Weather> weather { get; set; }
        [JsonProperty("base")]
        public string bas { get; set; }
        [JsonProperty("main")]
        public Main main { get; set; }
        [JsonProperty("visibility")]
        public int visibility { get; set; }
        [JsonProperty("wind")]
        public Wind wind { get; set; }
        [JsonProperty("clouds")]
        public Cloud clouds { get; set; }
        [JsonProperty("dt")]
        public int dt { get; set; }
        [JsonProperty("sys")]
        public Sys sys { get; set; }
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("cod")]
        public int cod { get; set; }
        [JsonProperty("rain")]
        public Rain rain { get; set; }
        [JsonProperty("snow")]
        public Snow snow{ get; set; }
        [JsonProperty("dt_txt")]
        public DateTime dt_txt { get; set; }
        public City city { get; set; }
        public class Snow
        {
            [JsonProperty("1h")]
            public double last_hour { get; set; }
            [JsonProperty("3h")]
            public double snow_past_3_hours { get; set; }
        }
        public class Rain
        {
            [JsonProperty("1h")]
            public double last_hour{ get; set; }
            [JsonProperty("3h")]
            public double past_three_hours { get; set; }
        }
        public class Sys
        {
            [JsonProperty("type")]
            public int type { get; set; }
            [JsonProperty("id")]
            public int id { get; set; }
            [JsonProperty("message")]
            public double message { get; set; }
            [JsonProperty("country")]
            public string country { get; set; }
            [JsonProperty("sunrise")]
            public int sunrise { get; set; }
            [JsonProperty("sunset")]
            public int sunset { get; set; }
            [JsonProperty("pod")]
            public string pod { get; set; }

        }
        public class Cloud
        {
            [JsonProperty("all")]
            public double all { get; set; }
        }
        public class Wind
        {
            [JsonProperty("speed")]
            public double speed { get; set; }
            [JsonProperty("deg")]
            public double deg { get; set; }

        }
        public class Main
        {
            [JsonProperty("temp")]
            public double temp { get; set; }
            [JsonProperty("pressure")]
            public double pressure { get; set; }
            [JsonProperty("humidity")]
            public double humidity { get; set; }
            [JsonProperty("temp_min")]
            public double temp_min { get; set; }
            [JsonProperty("temp_max")]
            public double temp_max { get; set; }
            [JsonProperty("sea_level")]
            public double sea_level { get; set; }
            [JsonProperty("grnd_level")]
            public double ground_level { get; set; }
            [JsonProperty("temp_kf")]
            public double temp_kf { get; set; }

        }
        public class Weather
        {
            [JsonProperty("id")]
            public int id { get; set; }
            [JsonProperty("main")]
            public string main { get; set; }
            [JsonProperty("description")]
            public string description { get; set; }
            [JsonProperty("icon")]
            public string icon { get; set; }


        }
        public class Coord
        {
            [JsonProperty("lon")]
            public double lon { get; set; }
            [JsonProperty("lat")]
            public double lat { get; set; }

        }
    }

}
