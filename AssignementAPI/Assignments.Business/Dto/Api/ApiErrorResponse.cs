using Newtonsoft.Json;

namespace Assignments.Business.Dto.Api
{
    public class ApiErrorResponse
    {
        [JsonProperty("statuscode", NullValueHandling = NullValueHandling.Ignore)]
        public int? StatusCode { get; set; } = null;

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string? Message { get; set; }
    }
}