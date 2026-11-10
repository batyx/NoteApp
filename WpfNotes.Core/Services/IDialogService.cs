namespace WpfNotes.Core.Services;

// Poser une question à l'utilisateur.
// L'implémentation réelle utilise le DialogHost de MaterialDesign, dans le projet WPF.
public interface IDialogService
{
    // Demande une confirmation ; renvoie true si l'utilisateur accepte
    Task<bool> ConfirmAsync(string message);
}
