namespace OpenLibrary
{
    using System.Windows;

    using Microsoft.Extensions.DependencyInjection;
    using NLog;
    using OpenLibrary.Services;
    using OpenLibrary.ViewModels;
    using OpenLibraryApi.Services;
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ILogger _logger;
        private readonly IOpenLibraryResponseParsing _parser;
        private readonly IOpenLibraryRequest _request;
        public App()
        {
            _logger = NLog.LogManager.GetCurrentClassLogger();
            _parser = new OpenLibraryResponseParsing();
            _request = new OpenLibraryRequest(_parser, _logger);
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);            
        }
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IOpenLibraryResponseParsing, OpenLibraryResponseParsing>();
            services.AddSingleton<IOpenLibraryRequest, OpenLibraryRequest>();
            services.AddTransient<SearchViewModel>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<ILogger>();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_request, _logger)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
