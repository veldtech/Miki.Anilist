using Miki.Anilist.Internal;
using Miki.Anilist.Internal.Queries;
using Miki.GraphQL;
using Newtonsoft.Json;
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
		/// Asynchronously searches and returns the first anime
		/// </summary>
		/// <param name="name">The name of the anime</param>
		/// <returns>The first anime or null if nothing found.</returns>
		public async Task<IMedia> GetMediaAsync(string name, params MediaFormat[] filter)
			=> (await graph.QueryAsync<MediaQuery>("query($p0: String, $p1: [MediaFormat]) { Media(search: $p0, format_not_in: $p1) { averageScore, coverImage { large }, title { romaji english native userPreferred }, description, duration, status, episodes, siteUrl, status, genres } }", 
				new GraphQLParameter(name), 
				new GraphQLParameter(filter, "[MediaFormat]")))?.Media ?? null;
		/// <summary>
		/// Asynchronously searches and returns the first anime
		/// </summary>
		/// <param name="filter"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public async Task<IMedia> GetMediaAsync(string filter, params GraphQLParameter[] parameters)
			=> (await graph.QueryAsync<MediaQuery>("Media(" + filter + ") { averageScore, coverImage { large }, title { romaji english native userPreferred }, description, duration, status, episodes, siteUrl, status, genres, volumes, chapters }", parameters))?.Media ?? null;
		/// <summary>
		/// Asynchronously gets the anime paired to the id
		/// </summary>
		/// <param name="name">The name of the anime</param>
		/// <returns>The first anime or null if nothing found.</returns>
		public async Task<IMedia> GetMediaAsync(int id)
			=> (await graph.QueryAsync<MediaQuery>("query($p0: Int){ Media(id: $p0) { averageScore, coverImage { large }, title { romaji english native userPreferred }, description, duration, episodes, siteUrl, status, genres, volumes, chapters } }", id))?.Media ?? null;

		/// <summary>
		/// Asynchronously searches and returns the first character
		/// </summary>
		/// <param name="name">The name of the character</param>
		/// <returns>The first character or null if nothing found.</returns>
		public async Task<ICharacter> GetCharacterAsync(string name)
			=> (await graph.QueryAsync<CharacterQuery>("query($p0: String){ Character(search: $p0){ name{ first last native } description siteUrl id image{ large } } }", name))?.Character ?? null;
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

			return new SearchResult<AnilistCharacter>((await graph.QueryAsync<SearchQuery<CharacterPage>>(query, page, name)).Page)
				.ToInterface<ICharacterSearchResult>();
		}

		/// <summary>
		/// Searches a character and returns the id and full name of a character
		/// </summary>
		/// <param name="name">name to search for</param>
		/// <param name="page">current page</param>
		/// <returns></returns>
		public async Task<ISearchResult<IMediaSearchResult>> SearchMediaAsync(string name, int page = 0, bool allowAdult = true, params MediaFormat[] filter)
		{
			// VERY BAD IMPLEMENTATION!!!
			string query = "query($p0: Int, $p1: String, $p2 : [MediaFormat]){ Page(page: $p0, perPage: 25) { pageInfo{ total currentPage perPage } media(search: $p1" +
				(allowAdult? "" : " isAdult: false ") + "format_not_in: $p2) { id title{userPreferred native english} } } }";

			return new SearchResult<AnilistMedia>((await graph.QueryAsync<SearchQuery<MediaPage>>(query, page, name, filter)).Page)
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
