using Miki.GraphQL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miki.Anilist.Internal.Queries
{
    internal class SearchQuery<T>
    {
		[JsonProperty("Page")]
		[GraphQLField("Page")]
		internal T Page;
    }

	internal class BasePage
	{
		[JsonProperty("pageInfo")]
		internal PageInfo PageInfo;
	}

	internal class MediaPage : BasePage
	{
		[JsonProperty("media")]
		internal List<AnilistMedia> Characters;
	}

	internal class CharacterPage : BasePage
	{
		[JsonProperty("characters")]
		internal List<AnilistCharacter> Characters;
	}

	public class PageInfo
	{
		[JsonProperty("total")]
		public int TotalItems { get; internal set; }

		[JsonProperty("currentPage")]
		public int CurrentPage { get; internal set; }

		[JsonProperty("perPage")]
		public int ItemsPerPage { get; internal set; }

		public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);
		public bool HasNextPage => CurrentPage < TotalPages;
	}
}
