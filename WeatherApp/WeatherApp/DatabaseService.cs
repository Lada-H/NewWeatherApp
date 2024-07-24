using System.Data.SQLite;

namespace WeatherApp;

public class DatabaseService
{
	private const string ConnectionString = "Data Source=weather_history.db;Version=3;";

	public DatabaseService()
	{
		using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
		{
			conn.Open();
			string sql = "CREATE TABLE IF NOT EXISTS history (id INTEGER PRIMARY KEY AUTOINCREMENT, city_name TEXT, timestamp TEXT)";
			SQLiteCommand command = new SQLiteCommand(sql, conn);
			command.ExecuteNonQuery();
		}
	}

	public void InsertHistory(string cityName)
	{
		using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
		{
			conn.Open();
			string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			string sql = "INSERT INTO history (city_name, timestamp) VALUES (@cityName, @timestamp)";
			SQLiteCommand command = new SQLiteCommand(sql, conn);
			command.Parameters.AddWithValue("@cityName", cityName);
			command.Parameters.AddWithValue("@timestamp", timestamp);
			command.ExecuteNonQuery();
		}
	}
}
