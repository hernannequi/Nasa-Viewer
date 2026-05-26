using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NASAViewer.Services;

namespace NASAViewer.Pages
{
    public class BLACKMARBLEModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Date { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd");

        public string TileUrlTemplate { get; set; }

        public void OnGet()
        {
            GibsTileService service = new GibsTileService();
            TileUrlTemplate = service.GetTileUrl("VIIRS_NOAA20_DayNightBand_At_Sensor_Radiance", Date);
        }
    }
}
