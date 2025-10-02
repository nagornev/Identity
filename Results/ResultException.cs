using System;

namespace OperationResults
{
    public class ResultException : Exception
    {
        public ResultException(int type, string message)
            : this(type, message, default)
        {
        }

        public ResultException(int type, string message, Exception inner = default)
            : base(message, inner)
        {
            Type = type;
        }

        public int Type { get; }

        public Result GetResult()
        {
            return Result.Failure(new ResultError(Type, Message));
        }

        public Result<TContentType> GetResult<TContentType>()
        {
            return Result<TContentType>.Failure(new ResultError(Type, Message));
        }
    }
}
