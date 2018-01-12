using Newtonsoft.Json;

namespace StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB.Model
{
    public class TelevisionResult
    {
        [JsonProperty("poster_path", NullValueHandling = NullValueHandling.Ignore)]
        public string PosterPath { get; set; }

        [JsonProperty("popularity", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Popularity { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }

        [JsonProperty("backdrop_path", NullValueHandling = NullValueHandling.Ignore)]
        public string BackdropPath { get; set; }

        [JsonProperty("vote_average", NullValueHandling = NullValueHandling.Ignore)]
        public decimal VoteAverage { get; set; }

        [JsonProperty("overview", NullValueHandling = NullValueHandling.Ignore)]
        public string Overview { get; set; }

        [JsonProperty("first_air_date", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstAirDate { get; set; }

        //[JsonProperty("origin_country", NullValueHandling = NullValueHandling.Ignore)]
        //public string OriginCountry { get; set; }

        //[JsonProperty("genre_ids", NullValueHandling = NullValueHandling.Ignore)]
        //public IList<GenreId> GenreIds { get; set; }

        [JsonProperty("original_language", NullValueHandling = NullValueHandling.Ignore)]
        public string OriginalLanguage { get; set; }

        [JsonProperty("vote_count", NullValueHandling = NullValueHandling.Ignore)]
        public int VoteCount { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("original_name", NullValueHandling = NullValueHandling.Ignore)]
        public string OriginalName { get; set; }
    }
}
