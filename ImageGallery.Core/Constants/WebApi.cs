namespace ImageGallery.Core.Constants
{
    public class WebApi
    {
        public static readonly string UrlScheme = "http";
        public static readonly string ServerAddress = "api.doitserver.in.ua";
        public static readonly string Url = $"{UrlScheme}://{ServerAddress}";

        public static readonly string SignIn = $"{Url}/login";
        public static readonly string SignUp = $"{Url}/create";
        public static readonly string AllImages = $"{Url}/all";
        public static readonly string Gif = $"{Url}/gif";
        public static readonly string Image = $"{Url}/image";

    }
}
