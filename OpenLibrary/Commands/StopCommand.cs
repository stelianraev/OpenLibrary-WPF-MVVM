namespace OpenLibrary.Commands
{
    using System.Threading;
    using NLog;
    using OpenLibrary.ViewModels;
    public class StopCommand : CommandBase
    {
        private readonly ILogger _logger;
        private CancellationTokenSource _cancellationTokenSource;

        public StopCommand(ILogger logger, CancellationTokenSource cancellationTokenSource)
        {
            _logger = logger;
            _cancellationTokenSource = cancellationTokenSource;
        }
        public override void Execute(object? parameter)
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
