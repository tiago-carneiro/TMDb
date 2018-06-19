using Newtonsoft.Json;
using System.Collections.Generic;

namespace TMDb.Core
{
    public class MovieApiResponse
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("results")]
        public IEnumerable<MovieItemResponse> Movies { get; set; }
    }
}
