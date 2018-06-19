using Newtonsoft.Json;
using System.Collections.Generic;

namespace TMDb.Core
{
    public class GenresApiResponse
    {
        [JsonProperty("genres")]
        public IEnumerable<GenresItemResponse> Genres { get; set; }
    }
}
