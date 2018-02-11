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
		PageInfo info;
		List<T> items = new List<T>();

		internal SearchResult(SearchPage q)
		{
			info = q.PageInfo;

			// TODO: maybe make this not use reflection?
			var fields = q.GetType()
				.GetRuntimeFields();

			FieldInfo f = fields
				.FirstOrDefault(x => x.FieldType == items.GetType());

			items = f.GetValue(q) as List<T>;
		}
		internal SearchResult(PageInfo info, List<T> list)
		{
			this.info = info;
			items = list;
		}

		internal ISearchResult<U> ToInterface<U>()
		{
			return new SearchResult<U>(info, items.Cast<U>().ToList());
		}

		public PageInfo PageInfo => info;
		public List<T> Items => items;
	}
}
