using System.Net.Http.Headers;
using System.Text;

namespace NContract.Core.RestApi
{
    public static class MediaTypeWithQualityHeaderValueExtensions
    {
        public static string Display(this MediaTypeWithQualityHeaderValue mediaType)
        {
            var mediaTypeSb = new StringBuilder();
            mediaTypeSb.Append($"CharSet:{mediaType?.CharSet}\n");
            mediaTypeSb.Append($"MediaType:{mediaType?.MediaType}\n");
            mediaTypeSb.Append($"Quality:{mediaType?.Quality}\n");
            return mediaTypeSb.ToString();
        }
    }
}