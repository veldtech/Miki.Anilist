using Miki.Anilist.Internal;
using Miki.Anilist.Internal.Queries;
using Miki.GraphQL;
using Newtonsoft.Json;
using Rest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Miki.Anilist
{
    public class AnilistClient
    {
		GraphQLClient graph;

		public AnilistClient()
		{
			graph = new GraphQLClient("https://graphql.anilist.co");
		}

		/// <summary>
		/// Asynchronously searches and returns the first character
		/// </summary>
		/// <param name="name">The name of the character</param>
		/// <returns>The first character or null if nothing found.</returns>
		public async Task<ICharacter> GetCharacterAsync(string name)
			=> (await graph.QueryAsync<CharacterQuery>("query($p0: String){ Character(search: $p0){ name{ first last native } description siteUrl id image{ large } } }", name)).Character;
		/// <summary>
		/// Asynchronously gets the character paired to the id
		/// </summary>
		/// <param name="id">character id</param>
		/// <returns>character</returns>
		public async Task<ICharacter> GetCharacterAsync(long id)
			=> (await graph.QueryAsync<CharacterQuery>("query($p0: Int){ Character(id: $p0){ name{ first last native } description siteUrl id image{ large } } }", id)).Character;

		/// <summary>
		/// Searches a character and returns the id and full name of a character
		/// </summary>
		/// <param name="name">name to search for</param>
		/// <param name="page">current page</param>
		/// <returns></returns>
		public async Task<ISearchResult<ICharacterSearchResult>> SearchCharactersAsync(string name, int page = 0)
		{
			string query = "query($p0: Int, $p1: String){ Page(page: $p0, perPage: 25) { pageInfo{ total currentPage } characters(search: $p1) { id name{first last} } } }";

			return new SearchResult<AnilistCharacter>((await graph.QueryAsync<SearchQuery>(query, page, name)).Page)
				.ToInterface<ICharacterSearchResult>();
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
