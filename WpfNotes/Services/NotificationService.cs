using MaterialDesignThemes.Wpf;
using WpfNotes.Core.Services;

namespace WpfNotes.Services;

// L'implémentation réelle de INotificationService : le Snackbar de MaterialDesign.
// Elle vit dans le projet WPF, la seule partie de l'application qui connaît MaterialDesign.
public class NotificationService : INotificationService
{
    private readonly SnackbarMessageQueue _messageQueue;

    public NotificationService(SnackbarMessageQueue messageQueue)
    {
        _messageQueue = messageQueue;
    }

    public void Show(string message)
    {
        _messageQueue.Enqueue(message);
    }
}
