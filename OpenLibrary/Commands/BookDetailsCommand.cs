namespace OpenLibrary.Commands
{
    using System.Windows;
    using OpenLibrary.ViewModels;

    /// <summary>
    /// Showing the specific Book details in another window
    /// </summary>
    public class BookDetailsCommand : CommandBase
    {
        BookDetailsViewModel _bookDetailsViewModel;
        public BookDetailsCommand(BookDetailsViewModel bookDetailsViewModel)
        {
            _bookDetailsViewModel = bookDetailsViewModel;
        }

        public override void Execute(object? parameter)
        {
            Window bookDetailsWindow = new BookDetailsWindow();

            bookDetailsWindow.DataContext = _bookDetailsViewModel;
            bookDetailsWindow.Show();
        }
    }
}
