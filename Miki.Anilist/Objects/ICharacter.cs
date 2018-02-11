using System;
using System.Collections.Generic;
using System.Text;

namespace Miki.Anilist
{
	public interface ICharacterSearchResult
	{
		long Id { get; }

		string FirstName { get; }
		string LastName { get; }
	}

    public interface ICharacter : ICharacterSearchResult
    {
		string NativeName { get; }

		string Description { get; }

		string SiteUrl { get; }

		string LargeImageUrl { get; }
		string MediumImageUrl { get; }
    }
}
