using System;

namespace StrategyCorps.CodeSample.Models
{
    public class TelevisionResultDto
    {
        public decimal Popularity { get; set; }

        public int Id { get; set; }

        public decimal VoteAverage { get; set; }

        public string Overview { get; set; }

        public DateTime? FirstAirDate { get; set; }
        
        public string OriginalLanguage { get; set; }

        public int VoteCount { get; set; }

        public string Name { get; set; }

        public string OriginalName { get; set; }
    }
}
