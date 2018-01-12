using System.Configuration;
using RestSharp;
using StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB;
using StrategyCorps.CodeSample.Interfaces.Dispatchers;
using StructureMap;

namespace StrategyCorps.CodeSample.Dispatchers.Registries
{
    public class DefaultDispatchersRegistry : Registry
    {
        public DefaultDispatchersRegistry()
        {
            For<IEntertainmentDispatcher>().Use<TheMovieDbDispatcher>();
            For<IRestClient>().Use(restClient => new RestClient(ConfigurationManager.AppSettings.Get("TheMovieDbBaseUrl")));
        }
    }
}