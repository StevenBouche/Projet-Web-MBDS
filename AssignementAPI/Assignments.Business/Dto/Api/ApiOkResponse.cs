using Newtonsoft.Json;

namespace Assignments.Business.Dto.Api
{
    public class ApiOkResponse<T>
    {
        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public T? Result { get; set; } = default;
    }
}
