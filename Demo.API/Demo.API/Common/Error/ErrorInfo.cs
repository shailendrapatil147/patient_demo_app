using Newtonsoft.Json;

namespace API.Common.Error
{
    [JsonObject(MemberSerialization.OptIn, Id = "errorResponse", Title = "errorResponse")]
    public class ErrorInfo
    {
        /// <summary>
        /// </summary>
        /// <param name="field"></param>
        /// <param name="type"></param>
        /// <param name="detail"></param>
        public ErrorInfo(string field, string type, string detail)
        {
            Field = field;
            Type = type;
            Detail = detail;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "field")]
        public string Field { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "detail")]
        public string Detail { get; set; }
    }
}
