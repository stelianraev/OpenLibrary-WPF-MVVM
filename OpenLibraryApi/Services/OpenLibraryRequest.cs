namespace OpenLibraryApi.Services
{
    public class OpenLibraryRequest
    {
        const string bookTitleUri = "https://openlibrary.org/search.json?title=";
        public HttpRequestMessage BookTitleSearch(string title)
        {
            title = title ?? title.Replace(' ', '+');
            var bookTitleRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(bookTitleUri + title)
            };

            return bookTitleRequest;
        }
    }
}
