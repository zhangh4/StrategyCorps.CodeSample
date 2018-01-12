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
    public class TelevisionController : ApiController
    {
        private const string InternalServerErrorDefaultMessage = "There was a problem processing the request, please try again later.";

        private readonly ITelevisionService _televisionService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// The television controller constructor
        /// </summary>
        /// <param name="televisionService" cref="ITelevisionService">The television service</param>
        /// <param name="logger" cref="ILogger">The NLog logger</param>
        /// <param name="mapper" cref="IMapper">The AutoMapper mapper</param>
        public TelevisionController(ITelevisionService televisionService, ILogger logger, IMapper mapper)
        {
            _televisionService = televisionService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        ///     Get television show
        /// </summary>
        /// <remarks>
        ///     Search for any television show
        /// </remarks>
        /// <param name="query">search query</param>
        [SwaggerResponse(HttpStatusCode.OK, "",typeof(TelevisionResultViewModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Television search query is required.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "The television search {query} was not found.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, InternalServerErrorDefaultMessage)]
        [HttpGet]
        [Route("api/television/{query}")]
        public IHttpActionResult TelevisionSearchByQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return Content(HttpStatusCode.BadRequest, "Television search query is required.");

            try
            {
                var televisionSearchResponseDto = _televisionService.GetTelevisionShowsByQuery(query);

                if (televisionSearchResponseDto == null) return Content(HttpStatusCode.NotFound, $"The television search {query} was not found.");

                var televisionSearchResponseViewModel = _mapper.Map<TelevisionSearchResponseDto, TelevisionSearchResponseViewModel>(televisionSearchResponseDto);

                return Ok(televisionSearchResponseViewModel);
                
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                return Content(HttpStatusCode.InternalServerError, InternalServerErrorDefaultMessage);
            }
        }

        /// <summary>
        ///     Get similar television shows
        /// </summary>
        /// <remarks>
        ///     Get similar television shows
        /// </remarks>
        /// <param name="id">Television show id</param>
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(TelevisionResultViewModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Television id is required.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "The television id {id} was not found.")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, InternalServerErrorDefaultMessage)]
        [HttpGet]
        [Route("api/television/{id}/similar")]
        public IHttpActionResult SimilarTelevisionShows(int id)
        {
            if (id <= 0) return Content(HttpStatusCode.BadRequest, "Television id is not correct");

            try
            {
                var televisionSearchResponseDto = _televisionService.GetSimilarTelevisionShowsById(id);

                if (televisionSearchResponseDto == null) return Content(HttpStatusCode.NotFound, $"The television id {id} was not found.");

                var televisionSearchResponseViewModel = _mapper.Map<TelevisionSearchResponseDto, TelevisionSearchResponseViewModel>(televisionSearchResponseDto);

                return Ok(televisionSearchResponseViewModel);

            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                return Content(HttpStatusCode.InternalServerError, InternalServerErrorDefaultMessage);
            }
        }
    }
}
