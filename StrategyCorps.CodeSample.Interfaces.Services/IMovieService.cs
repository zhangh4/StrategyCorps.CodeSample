using System.Collections.Generic;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Interfaces.Services
{
    public interface IMovieService
    {
        MovieAlternativeTitlesResponseDto GetAlternativeMovieTitlesById(int id);
    }
}