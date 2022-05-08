using Newtonsoft.Json;

namespace TestingModuleWebApp.Models
{
    public class BodyTask
    {
        [JsonProperty("question")]
        public string? Question { get; set; }
        [JsonProperty("rightAns")]
        public int RightAnswer { get; set; }
        [JsonProperty("userAns")]
        public int UserAnswer { get; set; }
        [JsonProperty("isRight")]
        public string? IsRight { get; set; }
    }
}
