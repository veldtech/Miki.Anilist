using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miki.Anilist.Internal
{
    internal class AnilistMedia
    {
		[JsonProperty("id")]
		public int Id;

		[JsonProperty("idMal")]
		public int MalId;

		[JsonProperty("title")]
		public string Title;

		[JsonProperty("description")]
		public string Description;

		[JsonProperty("countryOfOrigin")]
		public string CountryCode;

		[JsonProperty("isLicensed")]
		public bool IsLicensed;

		[JsonProperty("source")]
		public string Source;

		[JsonProperty("hashtag")]
		public string Hashtag;

		[JsonProperty("startDate")]
		public int StartDate;

		[JsonProperty("endDate")]
		public int EndDate;

		[JsonProperty("season")]
		public string Season;

		[JsonProperty("seasonYear")]
		public int SeasonYear;

		[JsonProperty("type")]
		public string MediaType;

		[JsonProperty("format")]
		public string MediaFormat;

		[JsonProperty("status")]
		public string MediaStatus;

		[JsonProperty("episodes")]
		public int EpisodeCount;

		[JsonProperty("duration")]
		public int Duration;

		[JsonProperty("chapters")]
		public int Chapters;

		[JsonProperty("volumes")]
		public int Volumes;

		[JsonProperty("isAdult")]
		public bool IsAdultContent;
	}
}
