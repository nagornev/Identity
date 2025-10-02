using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OperationResults
{
    [Serializable]
    public class Result<T> : Result
    {
        [JsonConstructor]
        protected Result(bool isSuccess,
                         T content = default,
                         ResultError error = default)
            : base(isSuccess, error)
        {
            Content = content;
        }

        [JsonPropertyName("content")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull)]
        public T Content { get; private set; }

        public static Result<T> Success(T content)
        {
            return new Result<T>(isSuccess: true,
                                 content: content);
        }

        public new static Result<T> Failure(ResultError error)
        {
            return new Result<T>(isSuccess: false,
                                 error: error);
        }

        #region Overrides

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        #endregion
    }
}
