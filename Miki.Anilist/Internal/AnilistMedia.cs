using Miki.GraphQL.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Miki.Anilist.Internal
{
	[GraphQLSchema("Media")]
	internal class AnilistMedia : IMedia
	{
		[JsonProperty("id")]
		internal int id;

		[JsonProperty("idMal")]
		[GraphQLField("idMal")]
		internal int? malId;

		[JsonProperty("title")]
		internal AnilistTitle title;

		[JsonProperty("coverImage")]
		internal AnilistImage coverImage;

		[JsonProperty("description")]
		internal string description;

		[JsonProperty("countryOfOrigin")]
		[GraphQLField("countryOfOrigin")]
		internal string countryCode;

		[JsonProperty("isLicensed")]
		internal bool isLicensed;

		[JsonProperty("source")]
		internal string source;

		[JsonProperty("averageScore")]
		[GraphQLField("averageScore")]
		internal int? score;

		[JsonProperty("hashtag")]
		internal string hashtag;

		[JsonProperty("startDate")]
		internal AnilistDate startDate;

		[JsonProperty("endDate")]
		internal AnilistDate endDate;

		[JsonProperty("season")]
		internal string season;

		[JsonProperty("type")]
		[GraphQLField("type")]
		internal string mediaType;

		[JsonProperty("format")]
		[GraphQLField("format")]
		internal string mediaFormat;

		[JsonProperty("status")]
		[GraphQLField("status")]
		internal string mediaStatus;

		[JsonProperty("siteUrl")]
		internal string siteUrl;

		[JsonProperty("genres")]
		internal List<string> genres = new List<string>();

		[JsonProperty("episodes")]
		[GraphQLField("episodes")]
		internal int? episodeCount;

		[JsonProperty("duration")]
		internal int? duration;

		[JsonProperty("chapters")]
		internal int? chapters;

		[JsonProperty("volumes")]
		internal int? volumes;

		[JsonProperty("isAdult")]
		[GraphQLField("isAdult")]
		internal bool isAdultContent;

		public string CoverImage => coverImage?.large ?? Constants.NoImageUrl;
		public string DefaultTitle => title?.userPreferred;
		public string Description => WebUtility.HtmlDecode(description ?? "")
			.Replace("<br>", "\n");
		public int? Duration => duration;
		public int? Episodes => episodeCount;
		public int? Volumes => volumes;
		public int? Chapters => chapters;
		public string EnglishTitle => title?.english;
		public IReadOnlyList<string> Genres => genres;
		public int Id => id;
		public string NativeTitle => title?.native;
		public int? Score => score;
		public string Status => mediaStatus;
		public string Url => siteUrl;
	}
}
