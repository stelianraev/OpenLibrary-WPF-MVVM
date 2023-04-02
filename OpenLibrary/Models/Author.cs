namespace OpenLibrary.Models
{
    using Newtonsoft.Json;
    public class Author
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("alternate_names")]
        public string[] AlternateNames { get; set; }

        [JsonProperty("birth_date")]
        public string BirthDate { get; set; }

        [JsonProperty("top_work")]
        public string TopWork { get; set; }

        [JsonProperty("top_subjects")]
        public string[] TopSubjects { get; set; }
    }
}
