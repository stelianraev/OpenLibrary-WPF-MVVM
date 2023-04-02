namespace OpenLibrary.Commands
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection.Emit;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using NLog;
    using OpenLibrary.Models;
    using OpenLibrary.Services;
    using OpenLibrary.ViewModels;
    
    public class SearchCommand : AsyncCommandBase
    {
        private readonly SearchViewModel _searchListViewModel;
        private readonly CancellationToken _cancellationToken;
        private readonly IOpenLibraryRequest _request;
        private readonly ILogger _logger;

        public SearchCommand(SearchViewModel searchListViewModel, IOpenLibraryRequest request, ILogger logger, CancellationToken cancellationToken)
        {
            _request = request;
            _logger = logger;
            _cancellationToken = cancellationToken;
            _searchListViewModel = searchListViewModel;
        }

        /// <summary>
        /// Getting parsed request result and populate the collection for visualization
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override async Task ExecuteAsync(object? parameter)
        {
            _searchListViewModel.ClearBooksCollection();

            var requestResult = await _request.SendRequestAsync<BaseResponse<Book>>(_searchListViewModel.BookTitleSearch, _searchListViewModel.AuthorNameSearch);


            /// MUST TO OPTIMISE THIS
            foreach (var book in requestResult.Docs)
            {
                if (_cancellationToken.IsCancellationRequested)
                {
                    // Cancel the calculation
                    return;
                }

                ///WITHOUT AWAIT WORKING FASTER
                 _request.GetBookCoverImage(book);

                _searchListViewModel.PopulateBooksCollection(book);
            }

            _logger.Info("Successfully request");

            if (!_searchListViewModel.BooksListing.Any())
            {
                _logger.Debug("Empty Collection with parameters {BookTitle} and {AuthorName}", _searchListViewModel.BookTitleSearch, _searchListViewModel.AuthorNameSearch);

                MessageBox.Show("Not result");
            }
           
        }    
    }
}