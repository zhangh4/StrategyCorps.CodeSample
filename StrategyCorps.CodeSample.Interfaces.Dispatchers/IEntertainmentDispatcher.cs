using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Interfaces.Dispatchers
{
    public interface IEntertainmentDispatcher
    {
        TelevisionSearchResponseDto GetTelevisionShowsByQuery(string query);

        TelevisionSearchResponseDto GetSimilarTelevisionShowsById(int id);
    }
}