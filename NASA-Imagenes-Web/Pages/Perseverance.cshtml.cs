using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NASAViewer.Models;
using NASAViewer.Services;

namespace NASAViewer.Pages
{
    public class PerseveranceModel : PageModel
    {

        private readonly NasaApiService _nasaApiService;

        public List<PerseveranceImage> Images { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 0;

        public PerseveranceModel(
            NasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
        }

        public async Task OnGetAsync()
        {
            Images =
                await _nasaApiService
                    .GetPerseveranceImagesAsync(PageNumber);
        }
    }
}
