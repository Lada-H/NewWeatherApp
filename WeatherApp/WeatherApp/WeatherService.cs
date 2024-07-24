using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace WeatherApp;

public class WeatherService
{
	private const string ApiKey = "a00097ff3fbe2bd2e348fc4e664254ab"; // Replace with your OpenWeatherMap API key
	private const string ApiUrl = "http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric";

	public async Task<JObject> FetchWeatherAsync(string cityName)
	{
		using (HttpClient client = new HttpClient())
		{
			string url = string.Format(ApiUrl, cityName, ApiKey);
			HttpResponseMessage response = await client.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				string json = await response.Content.ReadAsStringAsync();
				return JObject.Parse(json);
			}
			else
			{
				return null;
			}
		}
	}
}
