namespace OpenLibrary.Models
{
    using Newtonsoft.Json;
    using System.Windows.Media.Imaging;

    public class Book
    {
        [JsonProperty("key")]
        public string Key { get; init; }

        [JsonProperty("title")]
        public string Title { get; init; }

        [JsonProperty("first_publish_year")]
        public int FirstPublishYear { get; init; }

        [JsonProperty("number_of_pages_median")]
        public int NumberOfPages { get; init; }

        [JsonProperty("ratings_average")]
        public float RatingAverage { get; init; }

        [JsonProperty("author_name")]
        public string[] AuthorName { get; init; }

        [JsonProperty("author_key")]
        public string[] AuthorKey { get; init; }

        [JsonProperty("edition_count")]
        public int EditionCount { get; init; }

        [JsonProperty("cover_edition_key")]
        public string CoverEditionKey { get; init; }

        [JsonProperty("cover_i")]
        public int CoverId { get; init; }

        public BitmapImage? CoverImage { get; set; }
    }
}
