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
                List<string> valami2 = valami[$"{ComboBoxCountry.SelectedValue.ToString()}"].Select(x => x.name).OrderBy(x => x).ToList();
                comboBoxCity.Text = "";
                comboBoxCity.Items.Clear();
                comboBoxCity.Items.AddRange(valami2.ToArray());

            }
            catch (Exception x)
            {

                comboBoxCity.Text = x.Message;
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {

            LocationLabel.Text = $"{comboBoxCity.Text}, {ComboBoxCountry.Text}";
            string[] parameters = {
                                    comboBoxMeasurement.Text,
                                    ComboBoxOptions.SelectedValue.ToString(),
                                    ComboBoxCountry.SelectedValue.ToString(),
                                    comboBoxCity.Text,
                                    comboBoxLang.SelectedValue.ToString(),
                                    comboBoxLimit.Text
                                  };
            try
            {
                if (ComboBoxOptions.Text == "Daily")
                {


                    WeatherData weatherData = methods.GetForeCast(parameters);
                    PrintOut.Clear();
                    Stickers.Controls.Clear();
                    PrintOut.Text += $"Coordinates: Longitude: {weatherData.coord.lon}\u00B0, Latitude: {weatherData.coord.lat}\u00B0 \n";
                    int i = 1;
                    string temperature = comboBoxMeasurement.Text == "metric" ? "\u00B0C" : comboBoxMeasurement.Text == "imperial" ? "\u00B0F" : "K";
                    string speed = comboBoxMeasurement.Text == "imperial" ? "mhp" : "m/s";
                        GroupBox groupBox = methods.AddGroupBox(i, weatherData, temperature, speed);
                        Stickers.Controls.Add(groupBox);
                    foreach (WeatherData.Weather item in weatherData.weather)
                    {
                        PrintOut.Text += $"[{i++}] main: {item.main} description: {item.description}\n";
                    }
                    PrintOut.Text += $"Temperature: {weatherData.main.temp}{temperature} Min Temp: {weatherData.main.temp_min}{temperature} " +
                        $"Max Temp: {weatherData.main.temp_max}{temperature} \n" +
                        $"Air Pressure: {weatherData.main.pressure} hPa, Humidity: {weatherData.main.humidity}%\n" +
                        $"Sea level: {weatherData.main.sea_level}m, Ground level: {weatherData.main.ground_level}m\n" +
                        $"Wind Speed: {weatherData.wind.speed}{speed}, Direction: {methods.DegreesToDirections(weatherData.wind.deg)}\n" +
                        $"Cloudiness: {weatherData.clouds.all}%, Visibility: {weatherData.visibility}m \n";
                    if (weatherData.rain != null)
                    {
                        PrintOut.Text += $"Rain in the past 3 hours: {weatherData.rain.last_hour}mm, in last hour {weatherData.rain.last_hour}mm\n";
                    }
                    if (weatherData.snow != null)
                    {
                        PrintOut.Text += $"Snow in the past 3 hours: {weatherData.snow.snow_past_3_hours}mm, in lasthour {weatherData.snow.last_hour}mm\n";
                    }
                    PrintOut.Text += $"Time of calculation: {methods.UnixTimeStampConverter(weatherData.dt)}\n" +
                         $"Sunrise: {methods.UnixTimeStampConverter(weatherData.sys.sunrise)}," +
                         $" Sunset: {methods.UnixTimeStampConverter(weatherData.sys.sunset)}\n" +
                         $"Location: {weatherData.name},{weatherData.sys.country}"; 
                }
                else
                {

                    WeatherForecast weatherForecast = methods.GetForeCast(parameters);
                    PrintOut.Clear();
                    Stickers.Controls.Clear();
                    string temperature = comboBoxMeasurement.Text == "metric" ? "\u00B0C" : comboBoxMeasurement.Text == "imperial" ? "\u00B0F" : "K";
                    string speed = comboBoxMeasurement.Text == "imperial" ? "mhp" : "m/s";
                    PrintOut.Text += $"Location: {weatherForecast.city.name}, {weatherForecast.city.country}\n" +
                        $"Coordinates: Latitude: {weatherForecast.city.coord.lat}\u00B0, Longitude: {weatherForecast.city.coord.lon}\u00B0 \n" +
                        $"Population: {weatherForecast.city.population}\n";
                    for (int i = 0; i < weatherForecast.list.Count; i++)
                    {
                        GroupBox groupBox = methods.AddGroupBox(i, weatherForecast, temperature, speed);
                        Stickers.Controls.Add(groupBox);
                        PrintOut.Text += $"Forecast[{i + 1}]: Forecasted to {weatherForecast.list[i].dt_txt}\n Temperature: " +
                        $"{weatherForecast.list[i].main.temp}{temperature}, Min Temps: {weatherForecast.list[i].main.temp_min}{temperature}" +
                        $", Max Temps: {weatherForecast.list[i].main.temp_max}{temperature}, Temps KF: {weatherForecast.list[i].main.temp_kf}{temperature}\n" +
                        $"Air Pressure: {weatherForecast.list[i].main.pressure}hPa, Humidity: {weatherForecast.list[i].main.humidity}%\n" +
                        $"Sea level: {weatherForecast.list[i].main.sea_level}m, Ground level: {weatherForecast.list[i].main.ground_level}m\n"; 
                        for (int j = 0; j < weatherForecast.list[i].weather.Count; j++)
                        {

                            PrintOut.Text += $"Weather[{j + 1}]: {weatherForecast.list[i].weather[j].main}, description: {weatherForecast.list[i].weather[j].description}\n";

                        }
                        PrintOut.Text += $"Cloudiness: {weatherForecast.list[i].clouds.all}%, Wind Speed: {weatherForecast.list[i].wind.speed}{speed}, Direction: {methods.DegreesToDirections(weatherForecast.list[i].wind.deg)}\n ";
                        if (weatherForecast.list[i].rain != null)
                        {
                            PrintOut.Text += $"Rain in the past 3 hours: {weatherForecast.list[i].rain.past_three_hours}mm\n";
                        }
                        if (weatherForecast.list[i].snow != null)
                        {
                            PrintOut.Text += $"Snow in the past 3 hours: {weatherForecast.list[i].snow.snow_past_3_hours}mm\n";
                        }
                        PrintOut.Text += $"Calculated at {methods.UnixTimeStampConverter(weatherForecast.list[i].dt)}\n\n";
                    }
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
                comboBoxLimit.Items.AddRange(options.limit);
                comboBoxLimit.SelectedIndex = 0;
                comboBoxMeasurement.Items.AddRange(options.measurement);
                comboBoxMeasurement.SelectedIndex = 0;
                comboBoxLang.DataSource = new BindingSource(options.lang, null);
                comboBoxLang.DisplayMember = "Key";
                comboBoxLang.ValueMember = "Value";
                comboBoxLang.SelectedIndex = 0;
                ComboBoxOptions.DataSource = new BindingSource(options.options, null);
                ComboBoxOptions.DisplayMember = "Key";
                ComboBoxOptions.ValueMember = "Value";
                ComboBoxOptions.SelectedIndex = 0;
                countries = methods.LoadJson();
                ComboBoxCountry.DataSource = new BindingSource(options.CountriesDic, null);
                ComboBoxCountry.DisplayMember = "Key";
                ComboBoxCountry.ValueMember = "Value";
                ComboBoxCountry.Text = "Select a country";
            }
            catch (Exception x)
            {

                PrintOut.Text = x.Message;
            }

        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            if (panel2.Width == 200)
            {
                panel2.Width = 50;
            }
            else
            {
                panel2.Width = 200;
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            foreach (Control cnl in Controls)
            {
                methods.ClearAll(cnl);
            }
            Stickers.Controls.Clear();
        }


    }
}
