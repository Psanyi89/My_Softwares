using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
        /// <summary>
        /// Retreiving weather reports
        /// </summary>
        /// <param name="parameters[]"> 
        /// parameters[0] = measurement units
        /// parameters[1] = type of forecast
        /// parameters[2] = country code
        /// parameters[3] = city name
        /// parameters[4] = language
        /// </param>
        /// <returns></returns>
        public dynamic GetForeCast(string[] parameters)
        {

            using (HttpClient web = new HttpClient())
            {
                parameters[2] = string.IsNullOrEmpty(parameters[2]) || parameters[2] == "XK" ? "" : $",{parameters[2]}";
                string url = string.Format($"https://api.openweathermap.org/data/2.5/{parameters[1]}?q={parameters[3]}{parameters[2]}&appid={AppId}&units={parameters[0]}&lang={parameters[4]}&cnt={parameters[5]}");
                string json = web.GetStringAsync(url).Result;
                json = Encoding.UTF8.GetString(Encoding.Default.GetBytes(json));
                if (parameters[1] == "forecast")
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
        public async Task<List<City>> LoadJson()
        {
            using (Stream s = new FileStream(Path.Combine("Resources", "city.list.min.json"), FileMode.Open, FileAccess.Read))
            using (StreamReader r = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(r))
            {
                JsonSerializer serializer = new JsonSerializer();

                return await Task.Run(() => serializer.Deserialize<List<City>>(reader).OrderBy(x => x.country).ToList());
            }

        }
        #endregion
        #region create GroupBox
        public GroupBox AddGroupBox(int i, dynamic forecast, string temperature, string speed)
        {
            string date, temp, icon, time;


            if (forecast is WeatherForecast)
            {
                date = forecast.list[i].dt_txt.ToString("MMMM dd dddd");

                temp = forecast.list[i].main.temp.ToString();
                icon = forecast.list[i].weather[0].icon;
                time = forecast.list[i].dt_txt.ToString("HH:mm");
            }
            else
            {
                date = UnixTimeStampConverter(forecast.dt).ToString("MMMM dd dddd");

                temp = forecast.main.temp.ToString();
                icon = forecast.weather[0].icon;
                time = UnixTimeStampConverter(forecast.dt).ToString("HH:mm");
            }
            GroupBox group = new GroupBox
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            FlowLayoutPanel main = new FlowLayoutPanel
            {
                Parent = group,
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink

            };
            FlowLayoutPanel buffer = new FlowLayoutPanel
            {
                Parent = main,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink

            };
            FlowLayoutPanel Child1 = new FlowLayoutPanel
            {
                Name = $"Child1{i}",
                Parent = buffer,
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink

            };
            FlowLayoutPanel Child2 = new FlowLayoutPanel
            {
                Name = $"Child2{i}",
                Parent = buffer,
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink

            };
            Label Day = new Label
            {
                Text = $"{date}",
                Parent = main,
                Dock = DockStyle.Top,
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold),
                ForeColor = Color.LightBlue
            };


            PictureBox picture = new PictureBox
            {
                Parent = Child1,
                ImageLocation = $"Resources\\{icon}.png",
                SizeMode = PictureBoxSizeMode.CenterImage,
                Margin = new Padding(0, 5, 0, 0),
            };

            Label Temp = new Label
            {
                Text = $"{temp}{temperature}",
                Parent = Child2,
                Dock = DockStyle.Top,
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold),
                ForeColor = Color.Black
            };
            Label Time = new Label
            {
                Text = time,
                Parent = Child2,
                Dock = DockStyle.Bottom,
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold),
                ForeColor = Color.Black
            };
            return group;
        }
        #endregion
        #region Reset controls

        public void ClearAll(Control control)
        {
            foreach (Control c in control.Controls)
            {

                if (c is TextBox texbox)
                {
                    texbox.Clear();
                }

                if (c is ComboBox comboBox && !string.IsNullOrEmpty(comboBox.Text))
                {
                    comboBox.SelectedIndex = 0;
                }

                if (c is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.Format = DateTimePickerFormat.Short;
                    dateTimePicker.CustomFormat = " ";
                }
                if (c is RichTextBox richTextBox)
                {
                    richTextBox.Clear();
                }
                if (c.HasChildren)
                {
                    ClearAll(c);
                }
            }
        }
        #endregion
    }
}
