namespace OpenLibrary.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Controls;
    using System.Windows.Input;
    using NLog;
    using OpenLibrary.Commands;
    using OpenLibrary.Models;
    using OpenLibrary.Services;
    using OpenLibraryApi.Services;
   
    /// <summary>
    /// SearchView - Actually is a home screen view
    /// </summary>
    public class SearchViewModel : ViewModelBase
    {
        private string _bookTitleSearch;
        private string _authorNameSearch;
        private readonly ObservableCollection<BookListViewModel> _booksListing;
        private readonly IOpenLibraryRequest _request;
        private readonly ILogger _logger;
        public SearchViewModel(IOpenLibraryRequest request, ILogger logger)
        {
            _request = request;
            _logger = logger;
            _booksListing = new ObservableCollection<BookListViewModel>();
            SearchCommand = new SearchCommand(this, _request, _logger);            
        }

        /// <summary>
        /// Collection after request
        /// </summary>
        public IEnumerable<BookListViewModel> BooksListing => _booksListing;
                
        public string BookTitleSearch
        {
            get { return _bookTitleSearch; }
            set 
            { 
                _bookTitleSearch = value; 
                OnPropertyChanged(nameof(BookTitleSearch));
            }
        }

        public string AuthorNameSearch
        {
            get { return _authorNameSearch; }
            set
            {
                _authorNameSearch = value;
                OnPropertyChanged(nameof(AuthorNameSearch));
            }
        }

        /// <summary>
        /// Related to button Search
        /// </summary>
        public ICommand SearchCommand { get; }

        /// <summary>
        /// Using to populate collection witch store result of request. I am using method to keep collection encapsulated
        /// </summary>
        /// <param name="book"></param>
        public void PopulateBooksCollection(Book book)
        {            
            try
            {
                BookListViewModel bookListView = new BookListViewModel(book, _logger);

                _booksListing.Add(bookListView);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "BookListViewModel parsing book error");
            }
            
        }

        /// <summary>
        /// Empty the collection. If second request is made it it is needed to be cleared
        /// </summary>
        public void ClearBooksCollection()
        {
            _booksListing.Clear();
        }
    }
}
