using Miki.GraphQL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Miki.Anilist.Internal.Queries
{
    using Miki.Anilist.Objects;

    internal class SearchQuery<T>
    {
		[JsonProperty("Page")]
		[GraphQLField("Page")]
		internal T Page;
    }

    internal abstract class BasePage<TItem>
	{
		[JsonProperty("pageInfo")]
		internal PageInfo PageInfo;

        internal abstract IReadOnlyList<TItem> Items { get; }
    }

    internal class MediaPage : BasePage<IMedia>
	{
		[JsonProperty("media")]
		internal List<AnilistMedia> Medias;

        internal override IReadOnlyList<IMedia> Items => Medias;
    }

    internal class CharacterPage : BasePage<ICharacter>
	{
		[JsonProperty("characters")]
		internal List<AnilistCharacter> Characters;

        internal override IReadOnlyList<ICharacter> Items => Characters;
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
