using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Weather_App
{
    public class Methods
    {
        #region OpenWeatherMap Web Api AppId
        private const string AppId = "06542feecca333ba6d70ab13ebf5e2cb";
        #endregion
        #region Convert Unix Timestamp to DateTime
        /// <summary>
        /// Convert Unix Timestamp to DateTime
        /// </summary>
        /// <param name="timestamp">time in seconds since 1970</param>
        /// <returns></returns>
        public DateTime UnixTimeStampConverter(int timestamp)
        {
            try
            {
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                dateTime = dateTime.AddSeconds(timestamp).ToLocalTime();
                return dateTime;

            }
            catch (Exception x)
            {

                MessageBox.Show(x.Message);
                return DateTime.MaxValue;
            }
        }
        #endregion
        #region Wind direction degrees to compass directions
        /// <summary>
        /// Transforms wind direction degrees to compass directions
        /// </summary>
        /// <param name="degree">wind direction in degrees</param>
        /// <returns></returns>
        public string DegreesToDirections(double degree)
        {
            string[] directions = { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW", "N" };
            double index = degree / 22.5 + 0.5;
            return directions[(int)index];
        }
        #endregion
        #region Daily or 5 days forcast call
        public dynamic getForeCast(string type, string country, string city, string lang)
        {

            using (WebClient web = new WebClient())
            {

                string url = string.Format($"https://api.openweathermap.org/data/2.5/{type}?q={city},{country}&appid={AppId}&units=metric&lang={lang}");
                string json = web.DownloadString(url);
                json = Encoding.UTF8.GetString(Encoding.Default.GetBytes(json));
                if (type == "forecast")
                {
                    return JsonConvert.DeserializeObject<WeatherForecast>(json);
                }
                else
                {
                    return JsonConvert.DeserializeObject<WeatherData>(json);
                }

            }

        }
        #endregion
        #region Load Data From Json
        public List<City> LoadJson()
        {

            using (StreamReader r = new StreamReader(Application.StartupPath + @"/Resources/city.list.min.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<City>>(json).OrderBy(x => x.country).ToList();
            }

        }
        #endregion
    }
}
