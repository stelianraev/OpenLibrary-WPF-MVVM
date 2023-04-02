namespace OpenLibrary.Models
{
    using Newtonsoft.Json;
    public class BaseResponse<T> where T : class
    {
        [JsonProperty("numFound")]
        public int NumFound { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("numFoundExact")]
        public bool NumFoundExact { get; set; }

        [JsonProperty("docs")]
        public T[] Docs { get; set; }

        [JsonProperty("q")]
        public string Q { get; set; }

        [JsonProperty("offset")]
        public string Offset { get; set; }
    }
}
