using System;
using System.Text.Json.Serialization;

namespace OperationResults
{
    [Serializable]
    public class ResultError
    {
        public ResultError(int type,
                           string message)
        {
            Type = type;
            Message = message;
        }

        [JsonPropertyName("type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Type { get; }

        [JsonPropertyName("message")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Message { get; }
    }
}
