using System;
using Xamarin.Forms;

namespace MarvelComicsXF.Converters
{

    public class ThumbnailToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Models.Image thumbnail)
            {
                // Concatenate the path and extension to form the complete URL
                string imageUrl = $"{ConvertHttpToHttps(thumbnail.Path)}.{thumbnail.Extension}";
                return imageUrl;
            }

            // Return null if value is not of type Url
            return null;
        }

        public string ConvertHttpToHttps(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return url;

            // Check if the URL already starts with "https://"
            if (url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                return url; // Already in https, no need to convert

            // Check if the URL starts with "http://"
            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                // Replace "http://" with "https://"
                return "https://" + url.Substring("http://".Length);
            }

            // If the URL doesn't start with either "http://" or "https://",
            // return the original URL as it may be a different protocol or not a valid URL.
            return url;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
