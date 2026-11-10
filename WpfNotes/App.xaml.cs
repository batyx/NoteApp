using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using WpfNotes.Core.Services;
using WpfNotes.Core.ViewModels;
using WpfNotes.Services;

namespace WpfNotes;

public partial class App : Application
{
    // Le conteneur d'injection de dépendances, gardé pour être libéré à la fermeture
    private ServiceProvider? _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // 1. Déclarer, pour chaque type demandé, ce que le conteneur doit fournir
        ServiceCollection services = new ServiceCollection();
        services.AddSingleton<IFileService, FileService>();
        services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<SnackbarMessageQueue>(new SnackbarMessageQueue(TimeSpan.FromSeconds(2)));
        services.AddSingleton<INotificationService, NotificationService>();
        services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();

        // 2. Construire le conteneur
        _serviceProvider = services.BuildServiceProvider();

        // 3. Lui demander la fenêtre : il crée tout ce dont elle a besoin, dans le bon ordre
        MainWindow mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        if (_serviceProvider != null)
        {
            _serviceProvider.Dispose();
        }
        base.OnExit(e);
    }
}
