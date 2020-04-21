namespace Miki.Anilist.Internal
{
    using Miki.GraphQL.Queries;
    using Newtonsoft.Json;

    [GraphQLSchema]
    internal class AnilistTitle
    {
		[JsonProperty("romaji")]
		internal string romaji;

		[JsonProperty("english")]
		internal string english;

		[JsonProperty("native")]
		internal string native;

		[JsonProperty("userPreferred")]
		internal string userPreferred;
	}
}
