namespace OpenLibrary.ViewModels
{
    using System;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using NLog;
    using OpenLibrary.Commands;
    using OpenLibrary.Models;
    public class BookListViewModel : ViewModelBase
    {
        private readonly Book _book;
        ILogger _logger;
        //private readonly NavigationStore _navigationStore;
        public BookListViewModel(Book book, ILogger logger)
        {
            _logger = logger;
            _book = book;
            //_navigationStore = navigationStore;
            try
            {
                BookDetailsCommand = new BookDetailsCommand(new BookDetailsViewModel(_book));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "BookDetails error");
            }           
        }
        
        public string Title => _book.Title;
        public int? FirstPublishYear => _book.FirstPublishYear;
        public float? RatingAverage => _book.RatingAverage;
        public string AuthorName => String.Join(Environment.NewLine, _book.AuthorName); 
        public BitmapImage? CoverImage => _book.CoverImage;


        public ICommand BookDetailsCommand { get; }        
    }
}
