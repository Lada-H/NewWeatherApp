using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WeatherApp;

/// <summary>
/// Interaction logic for SearchPage.xaml
/// </summary>
public partial class SearchPage : Page
{
	private WeatherService _weatherService;
	private DatabaseService _databaseService;

	public SearchPage()
	{
		InitializeComponent();
		_weatherService = new WeatherService();
		_databaseService = new DatabaseService();
	}

	private async void SearchButton_Click(object sender, RoutedEventArgs e)
	{
		string cityName = CityTextBox.Text;

		if (!IsInternetAvailable())
		{
			ErrorLabel.Content = "No internet connection.";
			return;
		}

		try
		{
			JObject weatherData = await _weatherService.FetchWeatherAsync(cityName);

			if (weatherData != null)
			{
				_databaseService.InsertHistory(cityName);
				NavigationService.Navigate(new DetailPage(weatherData));
			}
			else
			{
				ErrorLabel.Content = "City not found.";
			}
		}
		catch (Exception ex)
		{
			ErrorLabel.Content = $"Error: {ex.Message}";
		}
	}

	private bool IsInternetAvailable()
	{
		try
		{
			using (var client = new HttpClient())
			using (client.GetAsync("http://www.google.com").Result)
				return true;
		}
		catch
		{
			return false;
		}
	}
}
