using System.Windows;
using MaterialDesignThemes.Wpf;
using WpfNotes.Core.Services;
using WpfNotes.Core.ViewModels;
using WpfNotes.Services;

namespace WpfNotes;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // On assemble l'application à la main : chaque objet reçoit ce dont il a besoin
        IFileService fileService = new FileService();
        SnackbarMessageQueue messageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
        INotificationService notificationService = new NotificationService(messageQueue);
        MainViewModel mainViewModel = new MainViewModel(fileService, notificationService);

        MainWindow mainWindow = new MainWindow(mainViewModel, messageQueue);
        mainWindow.Show();
    }
}
