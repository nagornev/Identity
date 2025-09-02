using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OperationResults
{
    [Serializable]
    public class Result
    {
        protected Result(bool isSuccess,
                         ResultError error = default)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        [JsonPropertyName("success")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public bool IsSuccess { get; }

        [JsonPropertyName("error")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault | JsonIgnoreCondition.WhenWritingNull)]
        public ResultError Error { get; }

        [JsonIgnore]
        public bool IsFailure => !IsSuccess;

        public static Result Success()
        {
            return new Result(isSuccess: true);
        }

        public static Result Failure(ResultError error)
        {
            return new Result(isSuccess: false,
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
