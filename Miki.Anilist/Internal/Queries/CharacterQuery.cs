using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miki.Anilist.Internal.Queries
{
	internal class CharacterQuery
	{
		[JsonProperty("Character")]
		internal AnilistCharacter Character;
	}

	internal class MediaQuery
	{
		[JsonProperty("Media")]
		internal AnilistMedia Media;
	}
}
