using Demo.API.Common;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace API.Common.Error
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ErrorServiceResponse
    {
        [JsonProperty(PropertyName = "errors")]
        public List<ErrorResponse> Errors { get; set; }

        public override string ToString()
        {
            return JsonService.SerializeObject(this);
        }
    }
}
