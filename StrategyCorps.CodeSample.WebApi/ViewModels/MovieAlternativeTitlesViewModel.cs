using System.Collections.Generic;

namespace StrategyCorps.CodeSample.WebApi.ViewModels
{
    /// <summary>
    /// MovieAlternativeTitlesViewModel
    /// </summary>
    public class MovieAlternativeTitlesViewModel
    {
        /// <summary>
        /// MovieId
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// Results
        /// </summary>
        public IEnumerable<MovieTitleViewModel> Titles { get; set; }
    }
}