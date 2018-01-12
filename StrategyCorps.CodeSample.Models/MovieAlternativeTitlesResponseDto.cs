using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyCorps.CodeSample.Models
{
    public class MovieAlternativeTitlesResponseDto
    {
        public int MovieId { get; set; }
        public IEnumerable<MovieTitleDto> Titles { get; set; }
    }
}
