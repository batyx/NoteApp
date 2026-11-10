using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using WpfNotes.Core.Messages;
using WpfNotes.Core.Models;
using WpfNotes.Core.Services;

namespace WpfNotes.Core.ViewModels;

// La logique de l'écran « liste des notes », sans aucun lien avec WPF.
// Elle reçoit les messages envoyés après un enregistrement ou une suppression.
public partial class AllNotesViewModel : ObservableRecipient,
    IRecipient<NoteSavedMessage>,
    IRecipient<NoteDeletedMessage>
{
    private readonly IFileService _fileService;
    private readonly INavigationService _navigationService;

    public AllNotesViewModel(IFileService fileService, INavigationService navigationService, IMessenger messenger)
        : base(messenger)
    {
        _fileService = fileService;
        _navigationService = navigationService;

        // Commencer à recevoir les messages
        IsActive = true;
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

    // Une note vient d'être enregistrée : elle passe en tête de la liste
    public void Receive(NoteSavedMessage message)
    {
        Notes.Remove(message.Note);
        Notes.Insert(0, message.Note);
    }

    // Une note vient d'être supprimée : elle quitte la liste
    public void Receive(NoteDeletedMessage message)
    {
        Notes.Remove(message.Note);
    }
}
