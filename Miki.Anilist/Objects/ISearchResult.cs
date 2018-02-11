using Miki.Anilist.Internal.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miki.Anilist
{
    public interface ISearchResult<T>
    {
		PageInfo PageInfo { get; }
		List<T> Items { get; }
    }
}
