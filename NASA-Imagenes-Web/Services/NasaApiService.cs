using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NASAViewer.Models;
using Microsoft.Extensions.Caching.Memory;

namespace NASAViewer.Services
{

    public class NasaApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly IMemoryCache _cache;
        public NasaApiService(HttpClient httpClient,
                            IConfiguration configuration,
                            IMemoryCache cache)
        {
            _httpClient = httpClient;
            _apiKey = configuration["NasaApi:ApiKey"];
            _cache = cache;
        }

        public async Task<ApodResponse> GetApodAsync(DateTime? date = null)
        {
            string cacheKey = $"apod_{date?.ToString("yyyyMMdd") ?? "today"}";

            if (_cache.TryGetValue(cacheKey, out ApodResponse cacheApod))
            {
                return cacheApod;
            }

            string url = $"https://api.nasa.gov/planetary/apod?api_key={_apiKey}";

            if (date.HasValue)
            {
                url += $"&date={date.Value:yyyy-MM-dd}";
            }

            string? json = await SafeGetAsync(url);

            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }
            ApodResponse? result = JsonSerializer.Deserialize<ApodResponse>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (result != null)
            {
                _cache.Set(
                    cacheKey,
                    result,
                    TimeSpan.FromHours(1));
            }

            return result;
        }

        public async Task<List<EpicImageResponse>> GetEpicImagesAsync()
        {
            const string cacheKey = "epic_images";

            if (_cache.TryGetValue(cacheKey, out List<EpicImageResponse> cache))
            {
                return cache;
            }

            string? json = await SafeGetAsync($"https://api.nasa.gov/EPIC/api/natural?api_key={_apiKey}");

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<EpicImageResponse>();
            }

            List<EpicImageResponse>? result = JsonSerializer.Deserialize<List<EpicImageResponse>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (result != null)
            {
                _cache.Set(
                    cacheKey,
                    result,
                    TimeSpan.FromMinutes(30));
            }

            return result;
        }

        //public async Task<List<Photo>> GetMarsPhotosBySolAsync(int sol)
        //{
        //    try
        //    {
        //        string url = $"https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?sol={sol}&api_key={_apiKey}";
        //        HttpResponseMessage response = await _httpClient.GetAsync(url);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return new List<Photo>();
        //        }

        //        string json = await response.Content.ReadAsStringAsync();

        //        MarsPhotosResponse result =
        //            JsonSerializer.Deserialize<MarsPhotosResponse>
        //            (json, new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            });

        //        return result?.photos ?? new List<Photo>();
        //    }
        //    catch (Exception ex)
        //    {
        //        return new List<Photo>();
        //    }
        //}

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

        public async Task<List<PerseveranceImage>> GetPerseveranceImagesAsync(
        int page = 1,
        int num = 6)
        {
            string url =
                $"https://mars.nasa.gov/rss/api/" +
                $"?feed=raw_images" +
                $"&category=mars2020" +
                $"&feedtype=json" +
                $"&page={page}" +
                $"&num={num}" +
                $"&order=sol desc";

            HttpResponseMessage response =
                await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string json =
                await response.Content.ReadAsStringAsync();

            PerseveranceResponse result =
                JsonSerializer.Deserialize<PerseveranceResponse>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            return result?.Images ?? new List<PerseveranceImage>();
        }

        public async Task<ApodResponse> GetRandomApodAsync()
        {
            const string cacheKey = "random_apod";

            if (_cache.TryGetValue(cacheKey, out ApodResponse cache))
            {

                return cache;
            }

            string url = $"https://api.nasa.gov/planetary/apod?api_key={_apiKey}&count=1";

            string? json = await SafeGetAsync(url);

            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            List<ApodResponse>? result =
                JsonSerializer.Deserialize<List<ApodResponse>>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            ApodResponse? apod = result?.FirstOrDefault();

            if (apod != null)
            {
                _cache.Set(
                    cacheKey,
                    apod,
                    TimeSpan.FromMinutes(30));
            }

            return apod;
        }

        private async Task<string?> SafeGetAsync(string url)
        {
            try
            {
                HttpResponseMessage response =
                    await _httpClient.GetAsync(url);

                if (response.StatusCode ==
                    System.Net.HttpStatusCode.TooManyRequests)
                {
                    return null;
                }

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }
        }

    }
}