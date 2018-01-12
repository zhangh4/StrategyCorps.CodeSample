using System;
using System.Linq;
using System.Net;
using System.Web.Http.Results;
using AutoMapper;
using ExpectedObjects;
using FizzWare.NBuilder;
using Moq;
using NLog;
using NUnit.Framework;
using StrategyCorps.CodeSample.Core.Exceptions;
using StrategyCorps.CodeSample.Interfaces.Services;
using StrategyCorps.CodeSample.Models;
using StrategyCorps.CodeSample.WebApi.Controllers;
using StrategyCorps.CodeSample.WebApi.Tests.Extensions;
using StrategyCorps.CodeSample.WebApi.ViewModels;

namespace StrategyCorps.CodeSample.WebApi.Tests.Controllers
{
    [TestFixture]
    public class MovieAlternativeTitlesByIdTests
    {
        [Test]
        [TestCase(null)]
        [TestCase(0)]
        [TestCase(-1)]
        public void MovieAlternativeTitlesById_When_IdIsNullOrInvalidInteger_Returns_BadRequest(int id)
        {
            var movieController = new MovieController(null, null, null);
            var actionResult = movieController.AlternativeMovieTitles(id);

            var response = actionResult.CheckActionResultAndCast<NegotiatedContentResult<string>>();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public void MovieAlternativeTitlesById_When_MovieServiceReturnsNull_Returns_NotFound()
        {
            var logger = new Mock<ILogger>();
            logger.Setup(x => x.Error(It.IsAny<Exception>())).Verifiable();

            var serviceMock = new Mock<IMovieService>();
            serviceMock.Setup(x => x.GetAlternativeMovieTitlesById(It.IsAny<int>())).Returns((MovieAlternativeTitlesResponseDto) null);

            var controller = new MovieController(serviceMock.Object, logger.Object, null);
            var actionResult = controller.AlternativeMovieTitles(33); // doesn't matter

            var response = actionResult.CheckActionResultAndCast<NegotiatedContentResult<string>>();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

            logger.Verify(x => x.Error(It.IsAny<Exception>()), Times.Never);
        }

        [Test]
        public void MovieAlternativeTitlesById_When_ServiceThrowsException_Returns_InternalServerError()
        {
            var logger = new Mock<ILogger>();
            logger.Setup(x => x.Error(It.IsAny<Exception>())).Verifiable();

            var serviceMock = new Mock<IMovieService>();
            serviceMock.Setup(x => x.GetAlternativeMovieTitlesById(It.IsAny<int>())).Throws<Exception>();

            var controller = new MovieController(serviceMock.Object, logger.Object, null);
            var actionResult = controller.AlternativeMovieTitles(33); // doesn't matter

            var response = actionResult.CheckActionResultAndCast<NegotiatedContentResult<string>>();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));

            logger.Verify(x => x.Error(It.IsAny<Exception>()), Times.Once);
        }

        [Test]
        public void MovieAlternativeTitlesById_When_ServiceThrowsStrategyCorpsException_Returns_InternalServerError()
        {
            var logger = new Mock<ILogger>();
            logger.Setup(x => x.Error(It.IsAny<Exception>())).Verifiable();

            var serviceMock = new Mock<IMovieService>();
            serviceMock.Setup(x => x.GetAlternativeMovieTitlesById(It.IsAny<int>())).Throws<StrategyCorpsException>();

            var controller = new MovieController(serviceMock.Object, logger.Object, null);
            var actionResult = controller.AlternativeMovieTitles(33);

            var response = actionResult.CheckActionResultAndCast<NegotiatedContentResult<string>>();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));

            logger.Verify(x => x.Error(It.IsAny<Exception>()), Times.Once);
        }

        [Test]
        [TestCase("Gotham")]
        public void MovieAlternativeTitlesById_When_ServiceReturnsMovieTitlesResponseDTO_Returns_Ok(string query)
        {
            var responseDto = Builder<MovieAlternativeTitlesResponseDto>.CreateNew().Build();
            var movieTitlesViewModel = Builder<MovieTitleViewModel>.CreateListOfSize(5).Build();
            var expectedResult = Builder<MovieAlternativeTitlesViewModel>.CreateNew()
                .With(x => x.Titles = movieTitlesViewModel.ToList()).Build();

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Error(It.IsAny<Exception>())).Verifiable();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<MovieAlternativeTitlesResponseDto, MovieAlternativeTitlesViewModel>(It.IsAny<MovieAlternativeTitlesResponseDto>()))
                .Returns(expectedResult).Verifiable();

            var serviceMock = new Mock<IMovieService>();
            serviceMock.Setup(x => x.GetAlternativeMovieTitlesById(It.IsAny<int>())).Returns(responseDto);

            var controller = new MovieController(serviceMock.Object, loggerMock.Object, mapperMock.Object);
            var actionResult = controller.AlternativeMovieTitles(33);

            var response = actionResult.CheckActionResultAndCast<OkNegotiatedContentResult<MovieAlternativeTitlesViewModel>>();
            response.Content.ToExpectedObject().ShouldEqual(expectedResult);

            loggerMock.Verify(x => x.Error(It.IsAny<Exception>()), Times.Never);
            mapperMock.Verify(x => x.Map<MovieAlternativeTitlesResponseDto, MovieAlternativeTitlesViewModel>(It.IsAny<MovieAlternativeTitlesResponseDto>()), Times.Once);
        }
    }
}