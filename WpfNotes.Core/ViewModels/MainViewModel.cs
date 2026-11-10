using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using WpfNotes.Core.Models;
using WpfNotes.Core.Services;

namespace WpfNotes.Core.ViewModels;

// Le ViewModel de la fenêtre. Il expose l'écran courant, et c'est lui qui change d'écran :
// il joue donc aussi le rôle du service de navigation.
public partial class MainViewModel : ObservableObject, INavigationService
{
    private readonly IFileService _fileService;
    private readonly IDialogService _dialogService;
    private readonly INotificationService _notificationService;
    private readonly IMessenger _messenger;
    private readonly AllNotesViewModel _allNotesViewModel;

    public MainViewModel(
        IFileService fileService,
        IDialogService dialogService,
        INotificationService notificationService,
        IMessenger messenger)
    {
        _fileService = fileService;
        _dialogService = dialogService;
        _notificationService = notificationService;
        _messenger = messenger;

        // La liste existe pendant toute la vie de l'application ; « this » est son service de navigation
        _allNotesViewModel = new AllNotesViewModel(fileService, this, messenger);
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
        NoteViewModel noteViewModel = new NoteViewModel(
            note, _fileService, _dialogService, _notificationService, this, _messenger);
        CurrentViewModel = noteViewModel;
    }
}
