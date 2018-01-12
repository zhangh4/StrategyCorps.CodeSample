using System;
using System.Net;
using System.Web.Http;
using AutoMapper;
using NLog;
using StrategyCorps.CodeSample.Interfaces.Services;
using StrategyCorps.CodeSample.Models;
using StrategyCorps.CodeSample.WebApi.ViewModels;
using Swashbuckle.Swagger.Annotations;

namespace StrategyCorps.CodeSample.WebApi.Controllers
{
    /// <summary>
    /// The television controller
    /// </summary>
    public class MovieController : ApiController
    {
        private const string InternalServerErrorDefaultMessage = "There was a problem processing the request, please try again later.";

        private readonly IMovieService _movieService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// The television controller constructor
        /// </summary>
        /// <param name="movieService" cref="IMovieService">The movie service</param>
        /// <param name="logger" cref="ILogger">The NLog logger</param>
        /// <param name="mapper" cref="IMapper">The AutoMapper mapper</param>
        public MovieController(IMovieService movieService, ILogger logger, IMapper mapper)
        {
            _movieService = movieService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        ///     Get alternative movie titles
        /// </summary>
        /// <remarks>
        ///     Get alternative movie titles
        /// </remarks>
        /// <param name="id">Movie id</param>
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(MovieAlternativeTitlesViewModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Movie id is required.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "The movie id {id} was not found.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, InternalServerErrorDefaultMessage)]
        [HttpGet]
        [Route("api/movie/{id}/alternative_titles")]
        public IHttpActionResult AlternativeMovieTitles(int id)
        {
            if (id <= 0) return Content(HttpStatusCode.BadRequest, "Movie id is not correct");

            try
            {
                var movieAlternativeTitlesResponseDto = _movieService.GetAlternativeMovieTitlesById(id);

                if (movieAlternativeTitlesResponseDto == null) return Content(HttpStatusCode.NotFound, $"The movie id {id} was not found.");

                var movieAlternativeTitlesViewModel = _mapper.Map<MovieAlternativeTitlesResponseDto, MovieAlternativeTitlesViewModel>(movieAlternativeTitlesResponseDto);

                return Ok(movieAlternativeTitlesViewModel);

            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                return Content(HttpStatusCode.InternalServerError, InternalServerErrorDefaultMessage);
            }
        }
    }
}
