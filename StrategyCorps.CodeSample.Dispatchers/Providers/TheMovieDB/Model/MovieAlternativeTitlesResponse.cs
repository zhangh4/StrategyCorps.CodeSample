using System.Collections.Generic;
using Newtonsoft.Json;

namespace StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB.Model
{
    public class MovieAlternativeTitlesResponse
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int MovieId { get; set; }

        [JsonProperty("titles", NullValueHandling = NullValueHandling.Ignore)]
        public IList<MovieTitleResult> Titles { get; set; }
    }
}