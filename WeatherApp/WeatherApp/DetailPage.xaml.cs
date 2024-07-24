using Newtonsoft.Json.Linq;
using System.Windows.Controls;

namespace WeatherApp;

/// <summary>
/// Interaction logic for DetailPage.xaml
/// </summary>
public partial class DetailPage : Page
{
	private JObject _weatherData;

	public DetailPage(JObject weatherData)
	{
		InitializeComponent();
		_weatherData = weatherData;
		DisplayWeatherData();
	}

	private void DisplayWeatherData()
	{
		CityLabel.Content = _weatherData["name"];
		TemperatureLabel.Content = $"Temperature: {_weatherData["main"]["temp"]}°C";
		DescriptionLabel.Content = $"Description: {_weatherData["weather"][0]["description"]}";
	}
}
