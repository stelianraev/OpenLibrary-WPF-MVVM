namespace OpenLibrary.Services
{
    using System.Threading.Tasks;
    using OpenLibrary.Models;
    public interface IOpenLibraryRequest
    {
        /// <summary>
        /// Dynamicaly Url Building depends on requeirements
        /// </summary>
        /// <param name="bookTitle">desired book title. Possible to be null</param>
        /// <param name="authorName">desired author name. Possible to be null</param>
        /// <returns></returns>
        string UrlBuilder(string bookTitle, string authorName);


        /// <summary>
        /// Send Request and get JSON Response from API
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        Task<string> GetRequestAsync(string uri);


        /// <summary>
        /// Send response and return model parsed from json
        /// Expose method for public using.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bookTitle"></param>
        /// <param name="authorName"></param>
        /// <returns></returns>
        Task<T> SendRequestAsync<T>(string bookTitle, string authorName) where T : class;

        Task<T> SendRequestAsync<T>(string url) where T : class;

        /// <summary>
        /// Construct uri depends on filled inputs
        /// </summary>
        /// <param name="olid"></param>
        /// <returns></returns>
        Task GetBookCoverImage(Book book);
    }
}
