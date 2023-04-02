namespace OpenLibrary.ViewModels
{
    using NLog;
    using OpenLibrary.Services;
    public class MainViewModel : ViewModelBase
    {
        private readonly IOpenLibraryRequest _request;
        private readonly ILogger _logger;
        public MainViewModel(IOpenLibraryRequest request, ILogger logger) 
        {
            _request = request;
            _logger = logger;
            CurrentViewModel = new SearchViewModel(_request, _logger);
        }

        public ViewModelBase CurrentViewModel { get; }
    }
}
