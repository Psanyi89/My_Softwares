using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Weather_App
{
    public partial class WeatherApp : Form
    {
        private Methods methods = new Methods();
        private List<City> countries;
        private ForeCastOptions options = new ForeCastOptions();

        public WeatherApp()
        {
            InitializeComponent();
        }

        private void ComboBoxCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ILookup<string, City> valami = countries.ToLookup(x => x.country, x => x);
                List<string> valami2 = valami[$"{ComboBoxCountry.Text}"].Select(x => x.name).OrderBy(x => x).ToList();
                comboBoxCity.Text = "";
                comboBoxCity.Items.Clear();
                comboBoxCity.Items.AddRange(valami2.ToArray());

            }
            catch (Exception x)
            {

                comboBoxCity.Text = x.Message;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LocationLabel.Text = $"{comboBoxCity.Text}, {ComboBoxCountry.Text}";
            try
            {
                if (ComboBoxOptions.Text == "Daily")
                {

                    WeatherData weatherData = methods.getForeCast(ComboBoxOptions.SelectedValue.ToString(), ComboBoxCountry.Text, comboBoxCity.Text, comboBoxLang.SelectedValue.ToString());
                    PrintOut.Clear();
                    PrintOut.Text += $"Coordinates: Longitude: {weatherData.coord.lon}\u00B0, Latitude: {weatherData.coord.lat}\u00B0 \n";
                    int i = 1;
                    foreach (WeatherData.Weather item in weatherData.weather)
                    {
                        PrintOut.Text += $"[{i++}] main: {item.main} description: {item.description}\n";
                    }
                    PrintOut.Text += $"Temperature: {weatherData.main.temp}\u00B0C Min Temp: {weatherData.main.temp_min}\u00B0C Max Temp: {weatherData.main.temp_max}\u00B0C \n" +
                            $"Air Pressure: {weatherData.main.pressure} hPa, Humidity: {weatherData.main.humidity}%\n" +
                            $"Sea level: {weatherData.main.sea_level}m, Ground level: {weatherData.main.ground_level}m\n" +
                            $"Wind Speed: {weatherData.wind.speed}km/h, Direction: {methods.DegreesToDirections(weatherData.wind.deg)}\n" +
                            $"Cloudiness: {weatherData.clouds.all}%\n" +
                            $"Time of calculation: {methods.UnixTimeStampConverter(weatherData.dt)}\n" +
                            $"Sunrise: {methods.UnixTimeStampConverter(weatherData.sys.sunrise)}, Sunset: {methods.UnixTimeStampConverter(weatherData.sys.sunset)}\n" +
                            $"Location: {weatherData.name},{weatherData.sys.country}";
                }
            }
            catch (Exception x)
            {

                PrintOut.Text = x.Message;
            }
        }

        private void WeatherApp_Load(object sender, EventArgs e)
        {
            try
            {
                comboBoxLang.DataSource = new BindingSource(options.lang, null);
                comboBoxLang.DisplayMember = "Key";
                comboBoxLang.ValueMember = "Value";
                comboBoxLang.SelectedIndex = 0;
                ComboBoxOptions.DataSource = new BindingSource(options.options, null);
                ComboBoxOptions.DisplayMember = "Key";
                ComboBoxOptions.ValueMember = "Value";
                ComboBoxOptions.SelectedIndex = 0;
                countries = methods.LoadJson();
                string[] CountryList = countries.Select(x => x.country).Distinct().ToArray();
                ComboBoxCountry.Items.AddRange(CountryList);
            }
            catch (Exception x)
            {

                PrintOut.Text = x.Message;
            }

        }
    }
}
