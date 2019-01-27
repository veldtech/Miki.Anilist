using System;
using System.Collections.Generic;
using System.Text;

namespace Miki.Anilist
{
	public interface IMediaSearchResult
	{
		int Id { get; }

        MediaType Type { get; }

        string DefaultTitle { get; }

		string EnglishTitle { get; }

		string NativeTitle { get; }
	}

    public interface IMedia : IMediaSearchResult
	{
		int? Chapters { get; }

		string CoverImage { get; }

		string Description { get; }

		int? Duration { get; }

		int? Episodes { get; }

		IReadOnlyList<string> Genres { get; }

		int? Score { get; }

		string Status { get; }

		string Url { get; }

		int? Volumes { get; }
	}
}
