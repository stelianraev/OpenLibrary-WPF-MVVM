namespace OpenLibraryApi.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using System.Windows.Media.Imaging;
    using NLog;
    using OpenLibrary.Models;
    using OpenLibrary.Services;

    public class OpenLibraryRequest : IOpenLibraryRequest
    {
        private IOpenLibraryResponseParsing _openLibraryResponseParsing;
        private readonly ILogger _logger;
        public OpenLibraryRequest(IOpenLibraryResponseParsing openLibraryResponseParsing, ILogger logger)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _openLibraryResponseParsing = openLibraryResponseParsing;
        }

        private readonly string _baseBookUri = "https://openlibrary.org/search.json?title=";
        private readonly string _baseAuthorUri = " https://openlibrary.org/search.json?author=";

        /// <summary>
        /// Dynamicaly Url Building depends on requeirements
        /// </summary>
        /// <param name="bookTitle">desired book title. Possible to be null</param>
        /// <param name="authorName">desired author name. Possible to be null</param>
        /// <returns></returns>
        public string UrlBuilder(string bookTitle, string authorName)
        {
            bool bookTrue = false;
            bool authorTrue = false;

            string requestUri = null;

            if (!String.IsNullOrEmpty(bookTitle) && !String.IsNullOrWhiteSpace(bookTitle))
            {
                bookTrue = true;
            }

            if (!String.IsNullOrEmpty(authorName) && !String.IsNullOrWhiteSpace(authorName))
            {
                authorTrue = true;
            }

            try
            {
                if (bookTrue && authorTrue)
                {
                    var book = String.Join('+', bookTitle.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries));
                    var author = String.Join('+', bookTitle.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries));
                    requestUri = $"{_baseBookUri}{book}&author={author}";
                }
                else if (bookTrue)
                {
                    var book = String.Join('+', bookTitle.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries));
                    requestUri = $"{_baseBookUri}{book}";
                }
                else if (authorTrue)
                {
                    var author = String.Join('+', authorName.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries));
                    requestUri = $"{_baseAuthorUri}{author}";
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "Url builder fail: {URLBuilder}");
            }            

            return requestUri;
        }

               
        /// <summary>
        /// Send Request and get JSON Response from API
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<string> GetRequestAsync(string uri)
        {
            string result = null;
            try
            {
                if (!String.IsNullOrEmpty(uri) && !String.IsNullOrWhiteSpace(uri))
                {
                    using (var client = new HttpClient())
                    {
                        var bookTitleRequest = new HttpRequestMessage
                        {
                            Method = HttpMethod.Get,
                            RequestUri = new Uri(uri)
                        };

                        using (var response = await client.SendAsync(bookTitleRequest))
                        {

                            response.EnsureSuccessStatusCode();
                            result = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Request problem {Requestproblem}");
            }

            return result;
        }

        /// <summary>
        /// Send response and return model parsed from json
        /// Expose method for public using.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bookTitle"></param>
        /// <param name="authorName"></param>
        /// <returns></returns>
        public async Task<T> SendRequestAsync<T>(string bookTitle, string authorName) where T : class
        {

            T result = null;

            try
            {
                var uri = UrlBuilder(bookTitle, authorName);

                var response = await GetRequestAsync(uri);

                result = _openLibraryResponseParsing.OpenLibraryBookResponseModelParsing<T>(response);
            }
            catch (Exception ex)
            {
                
            }      

            return result;
        }

        public async Task<T> SendRequestAsync<T>(string url) where T : class
        {
            var response = await GetRequestAsync(url);

            return _openLibraryResponseParsing.OpenLibraryBookResponseModelParsing<T>(response);
        }

        /// <summary>
        /// Construct uri depends on filled inputs
        /// </summary>
        /// <param name="olid"></param>
        /// <returns></returns>
        public async Task GetBookCoverImage(Book book)
        {
            string coverUrl;

            var olid = book.Key.Split("/", StringSplitOptions.RemoveEmptyEntries).ToArray()[1];

            if (!String.IsNullOrEmpty(olid) && !String.IsNullOrWhiteSpace(olid))
            {
            //var input = inputTitle.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            //var join = String.Join('+', input);           
                coverUrl = $"https://covers.openlibrary.org/b/olid/{olid}-L.jpg";

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(coverUrl); // Download the image data as a HttpResponseMessage
                    if (response.IsSuccessStatusCode)
                    {
                        byte[] imageData = await response.Content.ReadAsByteArrayAsync();

                        await using (var stream = new MemoryStream(imageData))
                        {
                            BitmapImage image = new BitmapImage();
                            //var bitmap = Bitmap.FromStream(stream);

                            image.BeginInit();
                            image.CacheOption = BitmapCacheOption.OnLoad;
                            image.StreamSource = stream;
                            image.EndInit();

                            book.CoverImage = image;
                        }
                    }
                }
            }
        }        
    }
}
