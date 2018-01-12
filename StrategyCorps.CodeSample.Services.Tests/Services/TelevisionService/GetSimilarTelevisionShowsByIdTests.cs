using System;
using ExpectedObjects;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using StrategyCorps.CodeSample.Core;
using StrategyCorps.CodeSample.Core.Exceptions;
using StrategyCorps.CodeSample.Interfaces.Dispatchers;
using StrategyCorps.CodeSample.Models;

namespace StrategyCorps.CodeSample.Services.Tests.Services.TelevisionService
{
    [TestFixture]
    public class GetSimilarTelevisionShowsByIdTests
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
        public void GetSimilarTelevisionShowsById_When_TheMovieDbDispatcherThrowsException_Throws_Exception()
        {
            _entertainmentDispatcherMock.Setup(x => x.GetSimilarTelevisionShowsById(It.IsAny<int>())).Throws<Exception>();
            var televisionService = new CodeSample.Services.TelevisionService(_entertainmentDispatcherMock.Object);

            Assert.Catch<Exception>(() => televisionService.GetSimilarTelevisionShowsById(0));
        }

        [Test]
        public void GetSimilarTelevisionShowsById_When_TheMovieDbDispatcherThrowsStrategyCorpsException_Throws_Exception()
        {
            var expectedException = Builder<StrategyCorpsException>.CreateNew()
                .With(x => x.StrategyCorpsErrorCode = ErrorCode.Default).Build();
            _entertainmentDispatcherMock.Setup(x => x.GetSimilarTelevisionShowsById(It.IsAny<int>())).Throws(expectedException);
            var televisionService = new CodeSample.Services.TelevisionService(_entertainmentDispatcherMock.Object);

            var actualException = Assert.Catch<Exception>(() => televisionService.GetSimilarTelevisionShowsById(0));

            actualException.ToExpectedObject().ShouldEqual(expectedException);
        }

        [Test]
        public void GetSimilarTelevisionShowsById_When_Successful_Returns_TelevisionSearchResponseDTO()
        {
            var expectedResult = Builder<TelevisionSearchResponseDto>.CreateNew().Build();
            _entertainmentDispatcherMock.Setup(x => x.GetSimilarTelevisionShowsById(It.IsAny<int>())).Returns(expectedResult);
            var televisionService = new CodeSample.Services.TelevisionService(_entertainmentDispatcherMock.Object);
            var actualResult = televisionService.GetSimilarTelevisionShowsById(0);

            actualResult.ToExpectedObject().ShouldEqual(expectedResult);
        }
    }
}