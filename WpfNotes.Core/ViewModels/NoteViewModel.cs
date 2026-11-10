using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfNotes.Core.Models;
using WpfNotes.Core.Services;

namespace WpfNotes.Core.ViewModels;

// La logique de l'écran « une note »
public partial class NoteViewModel : ObservableObject
{
    private readonly Note _note;
    private readonly IFileService _fileService;
    private readonly INotificationService _notificationService;
    private readonly INavigationService _navigationService;

    public NoteViewModel(
        Note note,
        IFileService fileService,
        INotificationService notificationService,
        INavigationService navigationService)
    {
        _note = note;
        _fileService = fileService;
        _notificationService = notificationService;
        _navigationService = navigationService;

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

    // Bouton Enregistrer : écrire la note, prévenir l'utilisateur, revenir à la liste
    [RelayCommand]
    private void Save()
    {
        _note.Text = Text;
        _fileService.SaveNote(_note);

        _notificationService.Show("Note enregistrée");
        _navigationService.NavigateToAllNotes();
    }
}
