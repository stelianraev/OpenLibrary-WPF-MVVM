namespace OpenLibrary.Services
{
    using Newtonsoft.Json;
    public class OpenLibraryResponseParsing : IOpenLibraryResponseParsing
    {
        /// <summary>
        /// To be possible to use it while code is extend it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public T OpenLibraryBookResponseModelParsing<T>(string response) where T : class
        {
            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}
