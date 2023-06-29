using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Miki.Anilist
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MediaSeason
    {
        WINTER,
        SPRING,
        SUMMER,
        FALL
    }
}
