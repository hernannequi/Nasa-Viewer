using System.Text.Json.Serialization;

namespace NASAViewer.Models
{
    public class PerseveranceResponse
    {
        [JsonPropertyName("images")]
        public List<PerseveranceImage> Images { get; set; }
    }

    public class PerseveranceImage
    {
        [JsonPropertyName("imageid")]
        public string ImageId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("caption")]
        public string Caption { get; set; }

        [JsonPropertyName("image_files")]
        public ImageFiles ImageFiles { get; set; }

        [JsonPropertyName("date_taken")]
        public string DateTaken { get; set; }

        [JsonPropertyName("camera")]
        public CameraInfo Camera { get; set; }

        [JsonPropertyName("sol")]
        public int Sol { get; set; }
    }

    public class ImageFiles
    {
        [JsonPropertyName("full_res")]
        public string FullRes { get; set; }

        [JsonPropertyName("medium")]
        public string Medium { get; set; }

        [JsonPropertyName("thumb")]
        public string Thumb { get; set; }
    }

    public class CameraInfo
    {
        [JsonPropertyName("camera_vector")]
        public string CameraVector { get; set; }
    }
}
