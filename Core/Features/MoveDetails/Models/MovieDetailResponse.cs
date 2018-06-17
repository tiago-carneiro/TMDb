using Newtonsoft.Json;
using System.Collections.Generic;

namespace TMDb.Core
{
    public class MovieDetailResponse : BaseMovieResponse
    {
        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("tagline")]
        public string Tagline { get; set; }

        [JsonProperty("vote_average")]
        public decimal VoteAverage { get; set; }

        [JsonProperty("genres")]
        public IEnumerable<GenresItemResponse> Genres { get; set; }
    }
}
