using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NASAViewer.Models;
using NASAViewer.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NASAViewer.Pages
{

    public class EPICModel : PageModel
    {
        private readonly NasaApiService _nasaApiService;
        public List<EpicImageResponse> EpicImages { get; set; }

        public EPICModel(NasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
        }

        public async Task OnGetAsync()
        {
            EpicImages = (await _nasaApiService.GetEpicImagesAsync())
                .Take(10)
                .ToList();
        }
    }

}
