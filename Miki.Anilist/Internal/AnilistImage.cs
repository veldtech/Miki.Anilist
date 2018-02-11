using Newtonsoft.Json;

namespace Miki.Anilist.Internal
{
	internal class AnilistImage
	{
		[JsonProperty("large")]
		internal string Large;

		[JsonProperty("medium")]
		internal string Medium;
	}
}
