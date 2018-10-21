using Miki.Anilist.Internal.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Miki.Anilist.Internal
{
	internal class SearchResult<T> : ISearchResult<T>
	{
		public PageInfo PageInfo { get; }
		public List<T> Items { get; } = new List<T>();

		internal SearchResult(BasePage q)
		{
			PageInfo = q.PageInfo;

			// TODO: maybe make this not use reflection?
			var fields = q.GetType()
				.GetRuntimeFields();

			FieldInfo f = fields
				.FirstOrDefault(x => x.FieldType == Items.GetType());

			Items = f.GetValue(q) as List<T>;
		}
		internal SearchResult(PageInfo info, List<T> list)
		{
			this.PageInfo = info;
			Items = list;
		}

		internal ISearchResult<U> ToInterface<U>()
		{
			return new SearchResult<U>(PageInfo, Items.Cast<U>().ToList());
		}
	}
}
