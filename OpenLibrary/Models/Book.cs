namespace OpenLibrary.Models
{
    using Newtonsoft.Json;
    using System.Windows.Media.Imaging;

    public class Book
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("first_publish_year")]
        public int FirstPublishYear { get; set; }

        [JsonProperty("number_of_pages_median")]
        public int NumberOfPages { get; set; }

        [JsonProperty("ratings_average")]
        public float RatingAverage { get; set; }

        [JsonProperty("author_name")]
        public string[] AuthorName { get; set; }

        [JsonProperty("author_key")]
        public string[] AuthorKey { get; set; }

        [JsonProperty("edition_count")]
        public int EditionCount { get; set; }

        [JsonProperty("cover_edition_key")]
        public string CoverEditionKey { get; set; }

        public BitmapImage? CoverImage { get; set; }
    }
}
