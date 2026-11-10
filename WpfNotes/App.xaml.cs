using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
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
        IDialogService dialogService = new DialogService();
        SnackbarMessageQueue messageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
        INotificationService notificationService = new NotificationService(messageQueue);
        IMessenger messenger = WeakReferenceMessenger.Default;
        MainViewModel mainViewModel = new MainViewModel(fileService, dialogService, notificationService, messenger);

        MainWindow mainWindow = new MainWindow(mainViewModel, messageQueue);
        mainWindow.Show();
    }
}
