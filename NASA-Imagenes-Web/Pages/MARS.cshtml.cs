//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using NASAViewer.Models;
//using NASAViewer.Services;

//namespace NASAViewer.Pages
//{
//    public class MARSModel : PageModel
//    {
//        private readonly NasaApiService _nasaApiService;

//        [BindProperty(SupportsGet = true)]
//        public int Sol { get; set; } = 1000;

//        public List<Photo> Photos { get; set; }

//        public MARSModel(NasaApiService nasaApiService)
//        {
//            _nasaApiService = nasaApiService;
//        }

//        public async Task OnGetAsync()
//        {
//            Photos = await _nasaApiService.GetMarsPhotosBySolAsync(Sol);
//        }
//    }
//}
