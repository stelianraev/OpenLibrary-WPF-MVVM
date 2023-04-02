namespace OpenLibrary.Services
{
    public interface IOpenLibraryResponseParsing
    {
        T OpenLibraryBookResponseModelParsing<T>(string response) where T : class;
    }
}
