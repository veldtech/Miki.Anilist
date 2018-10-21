using Miki.GraphQL.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miki.Anilist.Internal
{
	[GraphQLSchema]
	public class AnilistDate
	{
		internal int year;
		internal int month;
		internal int day;
	}
}
