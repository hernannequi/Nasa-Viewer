using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NASAViewer.Pages.API
{
    public class EPICImageModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public EPICImageModel(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync(
            string date,
            string image)
        {
            string apiKey =
                _configuration["NasaApi:ApiKey"];

            string url =
                $"https://api.nasa.gov/EPIC/archive/natural/" +
                $"{date}/png/{image}.png?api_key={apiKey}";

            HttpClient client =
                _httpClientFactory.CreateClient();

            byte[] bytes =
                await client.GetByteArrayAsync(url);

            return File(bytes, "image/png");
        }
    }
}
