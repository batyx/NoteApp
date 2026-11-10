using System.Collections.ObjectModel;
using WpfNotes.Core.Models;
using WpfNotes.Core.Services;

namespace WpfNotes.Core.ViewModels;

// La logique de l'écran « liste des notes », sans aucun lien avec WPF
public class AllNotesViewModel
{
    private readonly IFileService _fileService;

    public AllNotesViewModel(IFileService fileService)
    {
        _fileService = fileService;
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
}
