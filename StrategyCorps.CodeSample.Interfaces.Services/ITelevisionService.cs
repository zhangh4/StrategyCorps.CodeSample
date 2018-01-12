using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Interfaces.Services
{
    public interface ITelevisionService
    {
        TelevisionSearchResponseDto GetTelevisionShowsByQuery(string query);

        TelevisionSearchResponseDto GetSimilarTelevisionShowsById(int id);
    }
}
