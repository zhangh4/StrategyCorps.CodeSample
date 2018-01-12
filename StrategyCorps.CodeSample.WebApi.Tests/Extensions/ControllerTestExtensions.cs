using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using NUnit.Framework;
using StrategyCorps.CodeSample.Core;

namespace StrategyCorps.CodeSample.WebApi.Tests.Extensions
{
    internal static class ControllerTestExtensions
    {
        public static void CheckForCorrectErrorResponseBody(this NegotiatedContentResult<HttpError> response)
        {
            response.CheckForCorrectErrorResponseBody(null, null, string.Empty, string.Empty);
        }

        public static void CheckForCorrectErrorResponseBody(this NegotiatedContentResult<HttpError> response, HttpStatusCode expectedStatusCode)
        {
            response.CheckForCorrectErrorResponseBody(expectedStatusCode, null, string.Empty, string.Empty);
        }

        public static void CheckForCorrectErrorResponseBody(this NegotiatedContentResult<HttpError> response, ErrorCode expectedErrorCode)
        {
            response.CheckForCorrectErrorResponseBody(null, expectedErrorCode, string.Empty, string.Empty);
        }

        public static void CheckForCorrectErrorResponseBody(this NegotiatedContentResult<HttpError> response, HttpStatusCode expectedStatusCode, ErrorCode expectedErrorCode)
        {
            response.CheckForCorrectErrorResponseBody(expectedStatusCode, expectedErrorCode, string.Empty, string.Empty);
        }

        public static void CheckForCorrectErrorResponseBody(this NegotiatedContentResult<HttpError> response, ErrorCode expectedErrorCode, string expectedMessage)
        {
            response.CheckForCorrectErrorResponseBody(null, expectedErrorCode, expectedMessage, string.Empty);
        }

        public static void CheckForCorrectErrorResponseBody(this NegotiatedContentResult<HttpError> response, HttpStatusCode expectedStatusCode, ErrorCode expectedErrorCode, string expectedMessage)
        {
            response.CheckForCorrectErrorResponseBody(expectedStatusCode, expectedErrorCode, expectedMessage, string.Empty);
        }

        public static void CheckForCorrectErrorResponseBody(this NegotiatedContentResult<HttpError> response, string expectedMessage, string expectedDeveloperMessage)
        {
            response.CheckForCorrectErrorResponseBody(null, null, expectedMessage, expectedDeveloperMessage);
        }

        public static void CheckForCorrectErrorResponseBody(this NegotiatedContentResult<HttpError> response, HttpStatusCode expectedStatusCode, string expectedMessage, string expectedDeveloperMessage)
        {
            response.CheckForCorrectErrorResponseBody(expectedStatusCode, null, expectedMessage, expectedDeveloperMessage);
        }

        public static void CheckForCorrectErrorResponseBody(this NegotiatedContentResult<HttpError> response, string expectedDeveloperMessage, ErrorCode expectedErrorCode)
        {
            response.CheckForCorrectErrorResponseBody(null, expectedErrorCode, string.Empty, expectedDeveloperMessage);
        }

        public static void CheckForCorrectErrorResponseBody(this NegotiatedContentResult<HttpError> response, HttpStatusCode expectedStatusCode, string expectedDeveloperMessage, ErrorCode expectedErrorCode)
        {
            response.CheckForCorrectErrorResponseBody(expectedStatusCode, expectedErrorCode, string.Empty, expectedDeveloperMessage);
        }

        public static void CheckForCorrectErrorResponseBody(this NegotiatedContentResult<HttpError> response, HttpStatusCode? expectedStatusCode, ErrorCode? expectedErrorCode, string expectedMessage, string expectedDeveloperMessage)
        {
            Assert.That(response.Content.ContainsKey("code"));
            Assert.That(response.Content.ContainsKey("message"));
            Assert.That(response.Content.ContainsKey("developerMessage"));

            if (expectedStatusCode != null) Assert.That(response.StatusCode, Is.EqualTo(expectedStatusCode));
            if (expectedErrorCode != null) Assert.That(response.Content.GetPropertyValue<ErrorCode>("code"), Is.EqualTo(expectedErrorCode));
            if (!string.IsNullOrEmpty(expectedMessage)) Assert.That(response.Content.GetPropertyValue<string>("message"), Is.EqualTo(expectedMessage));
            if (!string.IsNullOrEmpty(expectedDeveloperMessage)) Assert.That(response.Content.GetPropertyValue<string>("developerMessage"), Is.EqualTo(expectedDeveloperMessage));
        }

        public static T CheckActionResultAndCast<T>(this IHttpActionResult actionResult)
        {
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult, Is.TypeOf<T>());
            return (T)actionResult;
        }
    }
}