using System.Text.Json.Serialization;

namespace NASAViewer.Models
{
    public class NasaImageSearchResponse
    {
        [JsonPropertyName("collection")]
        public Collection Collection { get; set; }
    }

    public class Collection
    {
        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        [JsonPropertyName("data")]
        public List<ItemData> Data { get; set; }

        [JsonPropertyName("links")]
        public List<ItemLink> Links { get; set; }
    }

    public class ItemData
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("date_created")]
        public string DateCreated { get; set; }
    }

    public class ItemLink
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }
    }
} 