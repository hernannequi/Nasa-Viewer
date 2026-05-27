using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NASAViewer.Models;

namespace NASAViewer.Services
{

    public class NasaApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public NasaApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["NasaApi:ApiKey"];
        }

        public async Task<ApodResponse> GetApodAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://api.nasa.gov/planetary/apod?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApodResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<EpicImageResponse>> GetEpicImagesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://api.nasa.gov/EPIC/api/natural?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<EpicImageResponse>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<List<Photo>> GetMarsPhotosBySolAsync(int sol)
        {
            try
            {
                string url = $"https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?sol={sol}&api_key={_apiKey}";
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                {
                    return new List<Photo>();
                }

                string json = await response.Content.ReadAsStringAsync();
                
                MarsPhotosResponse result = 
                    JsonSerializer.Deserialize<MarsPhotosResponse>
                    (json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result?.photos ?? new List<Photo>();
            }
            catch (Exception ex)
            {
                return new List<Photo>();
            }
        }

        public async Task<List<Item>> SearchImagesAsync(string query)
        {
            string url =
        $"https://images-api.nasa.gov/search?q={query}&media_type=image";

            HttpResponseMessage response =
                await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string json =
                await response.Content.ReadAsStringAsync();

            NasaImageSearchResponse result =
                JsonSerializer.Deserialize<NasaImageSearchResponse>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            return result?.Collection?.Items ?? new List<Item>();
        }
    }

}
