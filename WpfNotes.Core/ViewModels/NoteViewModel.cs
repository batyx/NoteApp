using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using WpfNotes.Core.Messages;
using WpfNotes.Core.Models;
using WpfNotes.Core.Services;

namespace WpfNotes.Core.ViewModels;

// La logique de l'écran « une note »
public partial class NoteViewModel : ObservableObject
{
    private readonly Note _note;
    private readonly IFileService _fileService;
    private readonly IDialogService _dialogService;
    private readonly INotificationService _notificationService;
    private readonly INavigationService _navigationService;
    private readonly IMessenger _messenger;

    public NoteViewModel(
        Note note,
        IFileService fileService,
        IDialogService dialogService,
        INotificationService notificationService,
        INavigationService navigationService,
        IMessenger messenger)
    {
        _note = note;
        _fileService = fileService;
        _dialogService = dialogService;
        _notificationService = notificationService;
        _navigationService = navigationService;
        _messenger = messenger;

        Text = note.Text;
    }

    // Le texte en cours d'édition, lié au champ de saisie de la vue
    [ObservableProperty]
    private string _text = "";

    // La ligne affichée en haut de l'écran
    public string DateText
    {
        get
        {
            if (_note.Filename == null)
            {
                return "Nouvelle note";
            }
            return "Enregistrée le " + _note.Date.ToString("dd/MM/yyyy HH:mm");
        }
    }

    // Bouton Annuler : revenir à la liste sans rien enregistrer
    [RelayCommand]
    private void Cancel()
    {
        _navigationService.NavigateToAllNotes();
    }

    // Bouton Enregistrer : écrire la note, prévenir la liste et l'utilisateur, revenir à la liste
    [RelayCommand]
    private void Save()
    {
        _note.Text = Text;
        _fileService.SaveNote(_note);

        _messenger.Send(new NoteSavedMessage(_note));
        _notificationService.Show("Note enregistrée");
        _navigationService.NavigateToAllNotes();
    }

    // Bouton Supprimer : seulement pour une note déjà enregistrée (voir CanDelete)
    [RelayCommand(CanExecute = nameof(CanDelete))]
    private async Task DeleteAsync()
    {
        if (_note.Filename == null)
        {
            return;
        }
        string filename = _note.Filename;

        // 1. Demander confirmation et attendre la réponse
        bool confirmed = await _dialogService.ConfirmAsync("Supprimer cette note ?");
        if (!confirmed)
        {
            return;
        }

        // 2. Supprimer le fichier
        _fileService.DeleteNote(filename);

        // 3. Prévenir la liste et l'utilisateur, revenir à la liste
        _messenger.Send(new NoteDeletedMessage(_note));
        _notificationService.Show("Note supprimée");
        _navigationService.NavigateToAllNotes();
    }

    // Une note neuve n'a pas encore de fichier : rien à supprimer, le bouton est grisé
    private bool CanDelete()
    {
        return _note.Filename != null;
    }
}
