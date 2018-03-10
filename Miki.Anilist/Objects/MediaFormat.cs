using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miki.Anilist
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MediaFormat
    {
		TV,
		OVA,
		ONA,
		MANGA,
		SPECIAL,
		TV_SHORT,
		ONE_SHOT,
		MUSIC,
		MOVIE,
		NOVEL,
    }
}
