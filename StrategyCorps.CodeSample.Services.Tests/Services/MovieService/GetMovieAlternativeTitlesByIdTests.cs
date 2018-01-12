using System;
using ExpectedObjects;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using StrategyCorps.CodeSample.Core;
using StrategyCorps.CodeSample.Core.Exceptions;
using StrategyCorps.CodeSample.Interfaces.Dispatchers;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Services.Tests.Services.MovieService
{
    [TestFixture]
    public class GetMovieAlternativeTitlesByIdTests
    {
        private Mock<IEntertainmentDispatcher> _entertainmentDispatcherMock;

        [SetUp]
        public void SetUp()
        {
            _entertainmentDispatcherMock = new Mock<IEntertainmentDispatcher>();
        }

        [TearDown]
        public void TearDown()
        {
            _entertainmentDispatcherMock = null;
        }

        [Test]
        public void GetMovieAlternativeTitlesById_When_TheMovieDbDispatcherThrowsException_Throws_Exception()
        {
            _entertainmentDispatcherMock.Setup(x => x.GetMovieAlternativeTitlesById(It.IsAny<int>())).Throws<Exception>();
            var movieService = new CodeSample.Services.MovieService(_entertainmentDispatcherMock.Object);

            Assert.Catch<Exception>(() => movieService.GetAlternativeMovieTitlesById(0));
        }

        [Test]
        public void GetMovieAlternativeTitlesById_When_TheMovieDbDispatcherThrowsStrategyCorpsException_Throws_Exception()
        {
            var expectedException = Builder<StrategyCorpsException>.CreateNew()
                .With(x => x.StrategyCorpsErrorCode = ErrorCode.Default).Build();
            _entertainmentDispatcherMock.Setup(x => x.GetMovieAlternativeTitlesById(It.IsAny<int>())).Throws(expectedException);
            var movieService = new CodeSample.Services.MovieService(_entertainmentDispatcherMock.Object);

            var actualException = Assert.Catch<Exception>(() => movieService.GetAlternativeMovieTitlesById(0));

            actualException.ToExpectedObject().ShouldEqual(expectedException);
        }

        [Test]
        public void GetMovieAlternativeTitlesById_When_Successful_Returns_MovieAlternativeTitlesResponseDTO()
        {
            var expectedResult = Builder<MovieAlternativeTitlesResponseDto>.CreateNew().Build();
            _entertainmentDispatcherMock.Setup(x => x.GetMovieAlternativeTitlesById(It.IsAny<int>())).Returns(expectedResult);
            var movieService = new CodeSample.Services.MovieService(_entertainmentDispatcherMock.Object);
            var actualResult = movieService.GetAlternativeMovieTitlesById(0);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }
    }
}