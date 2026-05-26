namespace NASAViewer.Services
{
    public class GibsTileService
    {
        private readonly string baseUrl = "https://gibs.earthdata.nasa.gov/wmts/epsg4326/best";

        public string GetTileUrl(string layer, string date, string resolution = "250m")
        {
            return $"{baseUrl}/{layer}/default/{date}/{resolution}/{{z}}/{{y}}/{{x}}.png";
        }
    }
}
