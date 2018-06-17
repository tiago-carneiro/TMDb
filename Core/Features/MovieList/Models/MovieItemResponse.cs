using Newtonsoft.Json;

namespace TMDb.Core
{
    public class MovieItemResponse : BaseMovieResponse
    {
        [JsonProperty("genre_ids")]
        public int[] GenreIds { get; set; }
    }
}
