using Newtonsoft.Json;

namespace AkuzelUI.Dtos
{
    public class SelectListDto
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
