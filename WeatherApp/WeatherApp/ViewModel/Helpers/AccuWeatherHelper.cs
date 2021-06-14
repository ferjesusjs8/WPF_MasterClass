using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel.Helpers
{
    public class AccuWeatherHelper
    {
        public static async Task<List<City>> GetCities(string query)
        {
            string BASE_URL = string.Empty;
            string AUTOCOMPLETE_ENDPOINT = string.Empty;
            string API_KEY = string.Empty;
            string API_KEY_2 = string.Empty;
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "WeatherApp.txt");

            using (var reader = new StreamReader(path))
            {
                var text = reader.ReadToEnd();

                var @params = text.Split(";");
                BASE_URL = @params[0];
                AUTOCOMPLETE_ENDPOINT = @params[1];
                API_KEY = @params[3];
                API_KEY_2 = @params[4];
            }

            return await GetAsync<List<City>>(query, AUTOCOMPLETE_ENDPOINT, BASE_URL, API_KEY, API_KEY_2);
        }

        public static async Task<CurrentConditions> GetCurrentConditions(string cityKey)
        {
            string BASE_URL = string.Empty;
            string CURRENT_CONDITIONS_ENDPOINT = string.Empty;
            string API_KEY = string.Empty;
            string API_KEY_2 = string.Empty;
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "WeatherApp.txt");

            using (var reader = new StreamReader(path))
            {
                var text = reader.ReadToEnd();

                var @params = text.Split(";");
                BASE_URL = @params[0];
                CURRENT_CONDITIONS_ENDPOINT = @params[2];
                API_KEY = @params[3];
                API_KEY_2 = @params[4];
            }
            return (await GetAsync<List<CurrentConditions>>(cityKey, CURRENT_CONDITIONS_ENDPOINT, BASE_URL, API_KEY, API_KEY_2)).FirstOrDefault();
        }


        public static async Task<T> GetAsync<T>(string parameter, string resource, string BASE_URL, string API_KEY, string API_KEY_2)
        {
            string url = $"{BASE_URL}{string.Format(resource, API_KEY, parameter)}";

            using (var cliente = new HttpClient())
            {
                var response = await cliente.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    url = $"{BASE_URL}{string.Format(resource, API_KEY_2, parameter)}";
                    response = await cliente.GetAsync(url);
                }

                string json = await response.Content.ReadAsStringAsync();

                var responseObject = JsonConvert.DeserializeObject<T>(json);

                return responseObject;
            }
        }
    }
}