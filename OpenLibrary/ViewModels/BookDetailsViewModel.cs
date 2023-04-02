namespace OpenLibrary.ViewModels
{
    using System;
    using System.Windows.Media.Imaging;

    using OpenLibrary.Models;
    public class BookDetailsViewModel : ViewModelBase
    {
        private readonly Book _book;
        public BookDetailsViewModel(Book book)
        {
            _book = book;
            CurrentViewModel = this;
        }

        public string Title => _book.Title;
        public int? FirstPublishYear => _book.FirstPublishYear;
        public int? NumberOfPages => _book.NumberOfPages;
        public float? RatingAverage => _book.RatingAverage;
        public string AuthorName => String.Join(Environment.NewLine, _book.AuthorName);
        public BitmapImage? CoverImage => _book.CoverImage;

        public ViewModelBase CurrentViewModel { get; }
    }
}
