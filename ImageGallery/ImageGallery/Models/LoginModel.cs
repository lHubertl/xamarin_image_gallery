using Newtonsoft.Json;

namespace ImageGallery.Models
{
    public class LoginModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("avatar")]
        public string AvatarUrl { get; set; }
    }
}
