using Newtonsoft.Json;

namespace Assignments.API.Models.Api
{
    public class ApiOkResponse<T>
    {
        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public T? Result { get; set; } = default;
    }
}
