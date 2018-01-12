using System.Configuration;

namespace StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB
{
    public class TheMovieDbDispatcherBase
    {
        protected readonly string TheMovieDbApiKey = ConfigurationManager.AppSettings.Get("TheMovieDbApiKey");
        protected readonly string TheMovieDbBaseUrl = ConfigurationManager.AppSettings.Get("TheMovieDbBaseUrl");
    }
}