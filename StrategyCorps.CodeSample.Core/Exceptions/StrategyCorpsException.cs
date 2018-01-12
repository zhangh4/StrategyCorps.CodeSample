using System;

namespace StrategyCorps.CodeSample.Core.Exceptions
{
    public class StrategyCorpsException:Exception
    {
        public StrategyCorpsException()
        {
        }

        public StrategyCorpsException(string message)
            : base(message)
        {
        }

        public StrategyCorpsException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public StrategyCorpsException(string message, ErrorCode errorCode, Exception innerException) : base(message, innerException)
        {
            StrategyCorpsErrorCode = errorCode;
        }

        public ErrorCode StrategyCorpsErrorCode { get; set; }
    }
}
