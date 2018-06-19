using Newtonsoft.Json;

namespace TMDb.Core
{
    public class GenresItemResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
