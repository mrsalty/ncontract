using System;
using System.Net.Http.Headers;
using System.Text;

namespace NContract.Core.RestApi
{
    public static class MediaTypeWithQualityHeaderValueExtensions
    {
        public static string Display(this MediaTypeWithQualityHeaderValue mediaType)
        {
            var mediaTypeSb = new StringBuilder();
            if (mediaType.CharSet != null)
                mediaTypeSb.Append($"CharSet:{mediaType.CharSet}{Environment.NewLine}");
            if (mediaType.MediaType != null)
                mediaTypeSb.Append($"MediaType:{mediaType.MediaType}{Environment.NewLine}");
            if (mediaType.Quality != null)
                mediaTypeSb.Append($"Quality:{mediaType.Quality}{Environment.NewLine}");
            return mediaTypeSb.ToString();
            //TODO all props
        }
    }
}