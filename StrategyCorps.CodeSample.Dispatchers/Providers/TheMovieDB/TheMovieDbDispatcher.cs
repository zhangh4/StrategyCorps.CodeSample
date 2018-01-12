using System;
using System.Net;
using AutoMapper;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using StrategyCorps.CodeSample.Core;
using StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB.Model;
using StrategyCorps.CodeSample.Interfaces.Dispatchers;
using StrategyCorps.CodeSample.Models;
using StrategyCorps.CodeSample.Core.Exceptions;

namespace StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB
{
    public class TheMovieDbDispatcher : TheMovieDbDispatcherBase, IEntertainmentDispatcher
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IRestClient _restClient;

        public TheMovieDbDispatcher(IRestClient restClient, ILogger logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _restClient = restClient;
        }

        /// <summary>
        /// Gets television shows that meet the query criteria
        /// </summary>
        /// <param name="query">The criteria used to search for television shows</param>
        /// <returns cref="TelevisionSearchResponseDto"></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="StrategyCorpsException"></exception>
        /// <exception cref="Exception"></exception>
        public TelevisionSearchResponseDto GetTelevisionShowsByQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) throw new ArgumentNullException(nameof(query), "The search query is required.");
            IRestResponse response;

            var queryString = $"api_key={TheMovieDbApiKey}&query={query}";

            var request = new RestRequest
            {
                Method = Method.GET,
                Resource = $"3/search/tv?{queryString}",
                RequestFormat = DataFormat.Json
            };

            try
            {
                _restClient.BaseUrl = new Uri(TheMovieDbBaseUrl);

                response = _restClient.Execute(request);
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                throw;
            }

            return MapGetTelevisionShowsResponse(response);
        }

        /// <summary>
        /// Gets television shows that are similar to the television show whose id is passed in.
        /// </summary>
        /// <param name="id">The id of the television show used to find similar television shows. </param>
        /// <returns cref="TelevisionSearchResponseDto"></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="StrategyCorpsException"></exception>
        /// <exception cref="Exception"></exception>
        public TelevisionSearchResponseDto GetSimilarTelevisionShowsById(int id)
        {
            if (id <= 0) throw new ArgumentException("The id  must be greater than 0.", nameof(id));

            IRestResponse response;

            var request = new RestRequest
            {
                Method = Method.GET,
                Resource = $"3/tv/{id}/similar?api_key={TheMovieDbApiKey}",
                RequestFormat = DataFormat.Json
            };

            try
            {
                _restClient.BaseUrl = new Uri(TheMovieDbBaseUrl);

                response = _restClient.Execute(request);
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                throw;
            }

            return MapGetTelevisionShowsResponse(response);
        }

        /// <summary>
        /// Maps the rest response from the get television shows request
        /// </summary>
        /// <param name="response" cref="IRestResponse">The rest response from the rest request. </param>
        /// <returns cref="TelevisionSearchResponseDto"></returns>
        /// <exception cref="StrategyCorpsException"></exception>
        private TelevisionSearchResponseDto MapGetTelevisionShowsResponse(IRestResponse response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var televisionSearchResponse = JsonConvert.DeserializeObject<TelevisionSearchResponse>(response.Content);
                    return _mapper.Map<TelevisionSearchResponse, TelevisionSearchResponseDto>(televisionSearchResponse);
                case HttpStatusCode.NotFound:
                    return null;
                default:
                    throw new StrategyCorpsException("There was a problem calling The Movie Db.", ErrorCode.Unknown, null);
            }
        }
    }
}