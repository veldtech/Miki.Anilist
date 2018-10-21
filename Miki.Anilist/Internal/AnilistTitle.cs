using Miki.GraphQL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miki.Anilist.Internal
{
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
