using Newtonsoft.Json;

namespace GraphQLExample.Models
{
    public class StarWarsFilms
    {
        [JsonProperty("allFilms")]
        public AllFilms? AllFilms { get; set; }
    }

    public class AllFilms 
    {
        [JsonProperty("films")]
        public List<Film>? Films { get; set; }
    }

    public class Film
    {
        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("episodeID")]
        public int EpisodeID { get; set; }
        [JsonProperty("director")]
        public string? Director { get; set; }
        [JsonProperty("releaseDate")]
        public string? ReleaseDate { get; set; }
    }
}
