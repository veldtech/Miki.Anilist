using Miki.Anilist.Internal.Queries;
using System.Collections.Generic;
using System.Linq;

namespace Miki.Anilist.Internal
{
	internal class SearchResult<T> : ISearchResult<T>
	{
		public PageInfo PageInfo { get; }
		public IReadOnlyList<T> Items { get; }

		internal SearchResult(BasePage<T> q)
		{
			PageInfo = q.PageInfo;
            Items = q.Items;
        }

		internal SearchResult(PageInfo info, IReadOnlyList<T> list)
		{
			PageInfo = info;
			Items = list;
		}

		internal ISearchResult<U> ToInterface<U>()
		{
			return new SearchResult<U>(PageInfo, Items.Cast<U>().ToList());
		}
    }
}
