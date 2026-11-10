namespace WpfNotes.Core.Services;

// Afficher un court message à l'utilisateur.
// L'implémentation réelle utilise le Snackbar de MaterialDesign, dans le projet WPF.
public interface INotificationService
{
    void Show(string message);
}
