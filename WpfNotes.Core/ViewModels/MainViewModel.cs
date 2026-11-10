using CommunityToolkit.Mvvm.ComponentModel;
using WpfNotes.Core.Models;
using WpfNotes.Core.Services;

namespace WpfNotes.Core.ViewModels;

// Le ViewModel de la fenêtre. Il expose l'écran courant, et c'est lui qui change d'écran :
// il joue donc aussi le rôle du service de navigation.
public partial class MainViewModel : ObservableObject, INavigationService
{
    private readonly IFileService _fileService;
    private readonly INotificationService _notificationService;
    private readonly AllNotesViewModel _allNotesViewModel;

    public MainViewModel(IFileService fileService, INotificationService notificationService)
    {
        _fileService = fileService;
        _notificationService = notificationService;

        // La liste existe pendant toute la vie de l'application ; « this » est son service de navigation
        _allNotesViewModel = new AllNotesViewModel(fileService, this);
        _allNotesViewModel.LoadNotes();

        // Au démarrage, on affiche la liste
        NavigateToAllNotes();
    }

    // Le ViewModel de l'écran affiché ; la fenêtre choisit la vue qui lui correspond
    [ObservableProperty]
    private object? _currentViewModel;

    public void NavigateToAllNotes()
    {
        CurrentViewModel = _allNotesViewModel;
    }

    public void NavigateToNote(Note note)
    {
        NoteViewModel noteViewModel = new NoteViewModel(note, _fileService, _notificationService, this);
        CurrentViewModel = noteViewModel;
    }
}
