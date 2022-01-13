using Newtonsoft.Json;

namespace Assignments.API.Models.Api
{
    public class ApiErrorResponse
    {
        [JsonProperty("statuscode", NullValueHandling = NullValueHandling.Ignore)]
        public int? StatusCode { get; set; } = null;
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string? Message { get; set; }
    }
}
