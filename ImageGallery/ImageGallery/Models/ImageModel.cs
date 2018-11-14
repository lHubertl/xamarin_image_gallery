using Newtonsoft.Json;

namespace ImageGallery.Models
{
    public class ImageModel
    {
        public int Id { get; set; }

        public ImageParametersModel Parameters { get; set; }

        [JsonProperty("smallImagePath")]
        public string SmallImageUrl { get; set; }

        [JsonProperty("bigImagePath")]
        public string ImageUrl { get; set; }

        [JsonProperty("created")]
        public string CreatedDateTime { get; set; }

        public string Description { get; set; }
        public string Hashtag { get; set; }
    }
}
