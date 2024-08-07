using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQLExample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GraphQLExample.Pages.Protected
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public StarWarsFilms? Films { get; set; }

        private ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger) 
        {
            _logger = logger;
        }

        public void OnGet()
        {
            bool ResultsFlag = false;
            if (Films is null)
            {
                ResultsFlag = AsyncGetGraphQLResult().Result;
            }
            if (!ResultsFlag)
            {
                RedirectToPage("/Error");
            }
        }

        private async Task<bool> AsyncGetGraphQLResult()
        {
            try
            {
                var graphQLClient = new GraphQLHttpClient("https://swapi-graphql.netlify.app/.netlify/functions/index", new NewtonsoftJsonSerializer());
                var request = new GraphQLRequest
                {
                    Query = @"
                            query {
                                allFilms {
                                    films {
                                        title
                                        episodeID
                                        director
                                        releaseDate
                                    }
                                }
                            }"
                };
                var response = await graphQLClient.SendQueryAsync<dynamic>(request);
                string data = response.Data.ToString();
                if (string.IsNullOrEmpty(data)) return false;
                Films = JsonConvert.DeserializeObject<StarWarsFilms>(data);
                if (Films is null) return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting GraphQL data");
                return false;
            }
                
            return true;
        }
    }
}
