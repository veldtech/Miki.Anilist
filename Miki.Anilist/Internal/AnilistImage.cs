using Miki.GraphQL.Queries;
using Newtonsoft.Json;

namespace Miki.Anilist.Internal
{
	[GraphQLSchema("coverImage")]
	internal class AnilistImage
	{
		[JsonProperty("large")]
		internal string large;

		[JsonProperty("medium")]
		internal string medium;
	}
}
