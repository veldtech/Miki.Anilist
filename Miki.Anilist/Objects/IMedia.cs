namespace Miki.Anilist.Objects
{
    using System.Collections.Generic;

    public interface IMediaSearchResult
	{
		int Id { get; }

        MediaType Type { get; }

		/// <summary>
		/// User preferred title.
		/// </summary>
        string DefaultTitle { get; }

		/// <summary>
		/// English title, if the series is available everywhere.
		/// </summary>
		string EnglishTitle { get; }

		/// <summary>
		/// Native kanji title
		/// </summary>
		string NativeTitle { get; }

		/// <summary>
		/// Romaji title
		/// </summary>
		string RomajiTitle { get; }
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
