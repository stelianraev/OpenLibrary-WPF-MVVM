namespace OpenLibrary.Commands
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using NLog;
    using OpenLibrary.Models;
    using OpenLibrary.Services;
    using OpenLibrary.ViewModels;
    
    public class SearchCommand : AsyncCommandBase
    {
        private readonly SearchViewModel _searchListViewModel;
        private readonly IOpenLibraryRequest _request;
        private readonly ILogger _logger;

        public SearchCommand(SearchViewModel searchListViewModel, IOpenLibraryRequest request, ILogger logger)
        {
            _request = request;
            _logger = logger;
            _searchListViewModel = searchListViewModel;
        }

        /// <summary>
        /// Getting parsed request result and populate the collection for visualization
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override async Task ExecuteAsync(object? parameter)
        {
            MessageBox.Show("Please wait");

            _searchListViewModel.ClearBooksCollection();

            var requestResult = await _request.SendRequestAsync<BaseResponse<Book>>(_searchListViewModel.BookTitleSearch, _searchListViewModel.AuthorNameSearch);

            foreach (var book in requestResult.Docs)
            {
                _searchListViewModel.PopulateBooksCollection(book);                

                _request.GetBookCoverImage(book);

                _logger.Info("All pass sucessfully");
            }

            if (!_searchListViewModel.BooksListing.Any())
            {
                _logger.Debug("Empty Collection with parameters {BookTitle} and {AuthorName}", _searchListViewModel.BookTitleSearch, _searchListViewModel.AuthorNameSearch);

                MessageBox.Show("Not result");
            }
           
        }
    }
}