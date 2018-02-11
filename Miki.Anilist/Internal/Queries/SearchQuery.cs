using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miki.Anilist.Internal.Queries
{
    internal class SearchQuery
    {
		[JsonProperty("Page")]
		internal SearchPage Page;
    }

	internal class SearchPage
	{
		[JsonProperty("pageInfo")]
		internal PageInfo PageInfo;

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

		public int TotalPages => TotalItems / ItemsPerPage;
		public bool HasNextPage => CurrentPage < TotalPages;
	}
}
