using Newtonsoft.Json;

namespace StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB.Model
{
    public class MovieTitleResult
    {
        [JsonProperty("iso_3166_1", NullValueHandling = NullValueHandling.Ignore)]
        public string CountryCode { get; set; }

        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
    }
}