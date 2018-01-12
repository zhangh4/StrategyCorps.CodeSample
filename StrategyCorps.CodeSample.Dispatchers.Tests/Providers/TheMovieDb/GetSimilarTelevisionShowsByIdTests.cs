using System;
using System.Linq;
using System.Net;
using AutoMapper;
using ExpectedObjects;
using FizzWare.NBuilder;
using Moq;
using Newtonsoft.Json;
using NLog;
using NUnit.Framework;
using RestSharp;
using StrategyCorps.CodeSample.Core.Exceptions;
using StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB;
using StrategyCorps.CodeSample.Dispatchers.Providers.TheMovieDB.Model;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Dispatchers.Tests.Providers.TheMovieDb
{
    [TestFixture]
    public class GetSimilarTelevisionShowsByIdTests
    {
        private Mock<IRestClient> _restClientMock;
        private Mock<ILogger> _loggerMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void SetUp()
        {
            _restClientMock = new Mock<IRestClient>();
            _loggerMock = new Mock<ILogger>();
            _mapperMock = new Mock<IMapper>();
        }

        [TearDown]
        public void TearDown()
        {
            _restClientMock = null;
            _loggerMock = null;
            _mapperMock = null;
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void GetSimilarTelevisionShowsByIdy_When_QueryIsNullOrWhitespace_Throws_StrategyCorpsException(int id)
        {
            var theMovieDbDispatcher = new TheMovieDbDispatcher(null, null, null);
            var exception = Assert.Throws<ArgumentException>(() => theMovieDbDispatcher.GetSimilarTelevisionShowsById(id));
            Assert.AreEqual("id", exception.ParamName);
        }

        [Test]
        [TestCase(10)]
        public void GetSimilarTelevisionShowsById_When_RestClientThrowsException_Throws_Exception(int id)
        {
            _loggerMock.Setup(x => x.Error(It.IsAny<Exception>())).Verifiable();
            _restClientMock.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Throws<Exception>().Verifiable();
            var theMovieDbDispatcher = new TheMovieDbDispatcher(_restClientMock.Object, _loggerMock.Object, null);
            Assert.Throws<Exception>(() => theMovieDbDispatcher.GetSimilarTelevisionShowsById(id));

            _loggerMock.Verify(x => x.Error(It.IsAny<Exception>()), Times.Once);
            _restClientMock.Verify(x => x.Execute(It.IsAny<IRestRequest>()), Times.Once);
        }

        [Test]
        [TestCase(10)]
        public void GetSimilarTelevisionShowsById_When_RestClientReturnsNotFound_Returns_Null(int id)
        {
            var restResponse = Builder<RestResponse>.CreateNew()
                                                    .With(x => x.StatusCode = HttpStatusCode.NotFound).Build();
            _loggerMock.Setup(x => x.Error(It.IsAny<StrategyCorpsException>())).Verifiable();
            _restClientMock.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(restResponse).Verifiable();
            var theMovieDbDispatcher = new TheMovieDbDispatcher(_restClientMock.Object, _loggerMock.Object, null);
            var actualResult = theMovieDbDispatcher.GetSimilarTelevisionShowsById(id);

            _loggerMock.Verify(x => x.Error(It.IsAny<StrategyCorpsException>()), Times.Never);
            _restClientMock.Verify(x => x.Execute(It.IsAny<IRestRequest>()), Times.Once);

            Assert.IsNull(actualResult);
        }

        [Test]
        [TestCase(10)]
        public void GetSimilarTelevisionShowsById_When_RestClientReturnsBadRequest_Throws_StrategyCorpException(int id)
        {
            var restResponse = Builder<RestResponse>.CreateNew()
                                                    .With(x => x.StatusCode = HttpStatusCode.BadRequest).Build();
            _loggerMock.Setup(x => x.Error(It.IsAny<StrategyCorpsException>())).Verifiable();
            _restClientMock.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(restResponse).Verifiable();
            var theMovieDbDispatcher = new TheMovieDbDispatcher(_restClientMock.Object, _loggerMock.Object, null);
            Assert.Throws<StrategyCorpsException>(() => theMovieDbDispatcher.GetSimilarTelevisionShowsById(id));

            _loggerMock.Verify(x => x.Error(It.IsAny<StrategyCorpsException>()), Times.Never);
            _restClientMock.Verify(x => x.Execute(It.IsAny<IRestRequest>()), Times.Once);
        }

        [Test]
        [TestCase(10)]
        public void GetSimilarTelevisionShowsById_When_Successful_Returns_TelevisionSearchResponseDTO(int id)
        {
            var televisionResults = Builder<TelevisionResult>.CreateListOfSize(5).Build().ToList();
            var televisionSearchResponse = Builder<TelevisionSearchResponse>.CreateNew().With(x => x.Results = televisionResults).Build();
            var televisionResultsDto = televisionResults.Select(televisionResult => new TelevisionResultDto
            {
                FirstAirDate = DateTime.Now,
                Id = televisionResult.Id,
                Name = televisionResult.Name,
                OriginalLanguage = televisionResult.OriginalLanguage,
                OriginalName = televisionResult.OriginalName,
                Overview = televisionResult.Overview,
                Popularity = televisionResult.Popularity,
                VoteAverage = televisionResult.VoteAverage,
                VoteCount = televisionResult.VoteCount
            }).ToList();

            var expectedResult = Builder<TelevisionSearchResponseDto>.CreateNew()
                                                                     .With(x => x.Page = televisionSearchResponse.Page)
                                                                     .With(x => x.TotalPages = televisionSearchResponse.TotalPages)
                                                                     .With(x => x.TotalResults = televisionSearchResponse.TotalResults)
                                                                     .With(x => x.Results = televisionResultsDto).Build();
            var restResponse = Builder<RestResponse>.CreateNew()
                                                    .With(x => x.StatusCode = HttpStatusCode.OK)
                                                    .With(x => x.Content = JsonConvert.SerializeObject(televisionSearchResponse)).Build();
            _loggerMock.Setup(x => x.Error(It.IsAny<Exception>())).Verifiable();
            _restClientMock.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(restResponse).Verifiable();
            _mapperMock.Setup(x => x.Map<TelevisionSearchResponse, TelevisionSearchResponseDto>(It.IsAny<TelevisionSearchResponse>())).Returns(expectedResult).Verifiable();

            var theMovieDbDispatcher = new TheMovieDbDispatcher(_restClientMock.Object, _loggerMock.Object, _mapperMock.Object);
            var actualResult = theMovieDbDispatcher.GetSimilarTelevisionShowsById(id);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);

            _loggerMock.Verify(x => x.Error(It.IsAny<Exception>()), Times.Never);
            _restClientMock.Verify(x => x.Execute(It.IsAny<IRestRequest>()), Times.Once);
            _mapperMock.Verify(x => x.Map<TelevisionSearchResponse, TelevisionSearchResponseDto>(It.IsAny<TelevisionSearchResponse>()), Times.Once);

        }
    }
}