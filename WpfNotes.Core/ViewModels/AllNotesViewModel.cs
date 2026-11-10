using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using WpfNotes.Core.Models;
using WpfNotes.Core.Services;

namespace WpfNotes.Core.ViewModels;

// La logique de l'écran « liste des notes », sans aucun lien avec WPF
public partial class AllNotesViewModel
{
    private readonly IFileService _fileService;
    private readonly INavigationService _navigationService;

    public AllNotesViewModel(IFileService fileService, INavigationService navigationService)
    {
        _fileService = fileService;
        _navigationService = navigationService;
    }

    // Les notes affichées par la vue, la plus récente en premier
    public ObservableCollection<Note> Notes { get; } = new ObservableCollection<Note>();

    // Lit les notes et remplit la collection
    public void LoadNotes()
    {
        List<Note> notes = _fileService.GetNotes();
        List<Note> sortedNotes = notes.OrderByDescending(note => note.Date).ToList();

        Notes.Clear();
        foreach (Note note in sortedNotes)
        {
            Notes.Add(note);
        }
    }

    // Bouton « + » : ouvrir une note neuve
    [RelayCommand]
    private void NewNote()
    {
        Note newNote = new Note();
        _navigationService.NavigateToNote(newNote);
    }

    // Clic sur une carte : ouvrir la note qu'elle affiche
    [RelayCommand]
    private void OpenNote(Note note)
    {
        _navigationService.NavigateToNote(note);
    }
}
