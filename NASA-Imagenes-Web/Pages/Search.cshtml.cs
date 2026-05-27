using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NASAViewer.Models;
using NASAViewer.Services;

namespace NASAViewer.Pages
{
    public class SearchModel : PageModel
    {
        private readonly NasaApiService _nasaApiService;

        public List<Item> Results { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string Query { get; set; } = "mars curiosity surface";

        public SearchModel(NasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
        }

        public async Task OnGetAsync()
        {
            Results =
                await _nasaApiService.SearchImagesAsync(Query);
        }
    }
}
