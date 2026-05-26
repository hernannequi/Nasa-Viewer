using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NASAViewer.Models;
using NASAViewer.Services;
using System.Threading.Tasks;

namespace NASAViewer.Pages
{
    public class APODModel : PageModel
    {
        private readonly NasaApiService _nasaApiService;
        public ApodResponse Apod { get; set; }

        public APODModel(NasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
        }

        public async Task OnGetAsync()
        {
            Apod = await _nasaApiService.GetApodAsync();
        }
    }
}
