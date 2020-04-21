namespace Miki.Anilist
{
    using System.Text;
    using Miki.Anilist.Internal;
    using Miki.Anilist.Internal.Queries;
	using Miki.Anilist.Objects;
    using Miki.GraphQL;
    using Miki.GraphQL.Queries;
    using System.Threading.Tasks;
    
    public class AnilistClient
    {
		private readonly GraphQLClient graph;

		private readonly IGraphQLQuery getMediaByNameQuery;
        private readonly IGraphQLQuery getMediaByIdQuery;

		public AnilistClient()
		{
			graph = new GraphQLClient("https://graphql.anilist.co");

			getMediaByNameQuery = graph.CreateQuery()
				.WithSchema<AnilistMedia, IQueryBuilder>(x =>
					x.WithDynamicParameter<string>("search")
					 .WithDynamicParameter<MediaFormat[]>("format_not_in"))
				.Compile();

			getMediaByIdQuery = graph.CreateQuery()
				.WithSchema<AnilistMedia, IQueryBuilder>(x => x.WithDynamicParameter<int>("id", true))
				.Compile();
		} 

		/// <summary>
		/// Asynchronously searches and returns the first media
		/// </summary>
		/// <param name="name">The name of the media</param>
		/// <returns>The first media or null if nothing found.</returns>
		public async Task<IMedia> GetMediaAsync(string name, params MediaFormat[] filter)
			=> (await getMediaByNameQuery.ExecuteAsync<MediaQuery>(("search", name), ("format_not_in", filter)))?.Media ?? null;

		/// <summary>
		/// Asynchronously gets the anime paired to the id
		/// </summary>
		/// <param name="id">The id of the media</param>
		/// <returns>The first anime or null if nothing found.</returns>
		public async Task<IMedia> GetMediaAsync(int id)
			=> (await getMediaByIdQuery.ExecuteAsync<MediaQuery>(("id", id)))?.Media;

		/// <summary>
		/// Asynchronously searches and returns the first character
		/// </summary>
		/// <param name="name">The name of the character</param>
		/// <returns>The first character or null if nothing found.</returns>
		public async Task<ICharacter> GetCharacterAsync(string name)
			=> (await graph.QueryAsync<CharacterQuery>(
                   "query($p0: String){ Character(search: $p0){ name{ first last native } description siteUrl id image{ large } } }", name))?.Character;
		/// <summary>
		/// Asynchronously gets the character paired to the id
		/// </summary>
		/// <param name="id">character id</param>
		/// <returns>character</returns>
		public async Task<ICharacter> GetCharacterAsync(long id)
			=> (await graph.QueryAsync<CharacterQuery>("query($p0: Int){ Character(id: $p0){ name{ first last native } description siteUrl id image{ large } } }", id))?.Character ?? null;

		/// <summary>
		/// Searches a character and returns the id and full name of a character
		/// </summary>
		/// <param name="name">name to search for</param>
		/// <param name="page">current page</param>
		/// <returns></returns>
		public async Task<ISearchResult<ICharacterSearchResult>> SearchCharactersAsync(string name, int page = 0)
		{
			string query = "query($p0: Int, $p1: String){ Page(page: $p0, perPage: 25) { pageInfo{ total currentPage perPage } characters(search: $p1) { id name{first last} } } }";

			return new SearchResult<ICharacter>((await graph.QueryAsync<SearchQuery<CharacterPage>>(query, page, name)).Page)
				.ToInterface<ICharacterSearchResult>();
		}

		/// <summary>
		/// Searches a character and returns the id and full name of a character
		/// </summary>
		/// <param name="name">name to search for</param>
		/// <param name="page">current page</param>
		/// <returns></returns>
		public async Task<ISearchResult<IMediaSearchResult>> SearchMediaAsync(
            string name, int page = 0, bool allowAdult = true, MediaType? type = null, params MediaFormat[] filter)
		{
            //Build first line of query `query(params) {`
            var query = new StringBuilder("query ($p0: Int, $p1: String");
            if (filter.Length > 0)
                query.Append(",$p2 : [MediaFormat]");
            if (type.HasValue)
                query.Append(", $p3: MediaType");
            query.Append(") {");

            //Append `Page` part of query
            query.Append("Page(page: $p0, perPage: 25) { pageInfo { total currentPage perPage }");

            //Insert the parameters of the `media` section
            query.Append("media(search: $p1");
            if (filter.Length > 0)
                query.Append(", format_not_in: $p2");
            if (type.HasValue)
                query.Append(", type: $p3");

            //Add the main body of the media query and balance all the braces
            query.Append(") { id type title { userPreferred native english romaji } } } }");

            return new SearchResult<IMedia>(
                    (await graph.QueryAsync<SearchQuery<MediaPage>>(
                        query.ToString(), page, name, filter, type)).Page)
                .ToInterface<IMediaSearchResult>();
        }

		/// <summary>
		/// Wrapper for the base query of Miki.GraphQL for when you need more than the current features
		/// </summary>
		/// <typeparam name="T">Type to serialize to</typeparam>
		/// <param name="query">Graphql query text</param>
		/// <param name="variables">Variables, use them like $p0, $p1... etc.</param>
		/// <returns></returns>
		public async Task<T> QueryAsync<T>(string query, params object[] variables)
			=> await graph.QueryAsync<T>(query, variables);
	}
}
