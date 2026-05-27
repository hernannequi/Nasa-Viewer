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

        public DateTime MinDate =>
            new(1995, 6, 16);

        public DateTime MaxDate =>
            DateTime.Today;
        
        [BindProperty(SupportsGet = true)]
        public DateTime? Date { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool Random { get; set; }

        public APODModel(NasaApiService nasaApiService)
        {
            _nasaApiService = nasaApiService;
        }

        public async Task OnGetAsync()
        {
            Date ??= DateTime.Today;

            // Limitar fechas
            if (Date < MinDate)
            {
                Date = MinDate;
            }

            if (Date > MaxDate)
            {
                Date = MaxDate;
            }

            if (Random)
            {
                Apod =
                    await _nasaApiService
                        .GetRandomApodAsync();

                    Date = Apod.Date;
            }
            else
            {
                Apod =
                    await _nasaApiService
                        .GetApodAsync(Date);
            }
        }
    }
}
