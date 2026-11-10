using CommunityToolkit.Mvvm.Messaging;
using WpfNotes.Core.Messages;
using WpfNotes.Core.Models;
using WpfNotes.Core.ViewModels;
using WpfNotes.Tests.Fakes;

namespace WpfNotes.Tests.ViewModels;

// Les tests de AllNotesViewModel, avec des fakes écrits à la main
public class AllNotesViewModelTests
{
    private readonly FakeFileService _fileService = new FakeFileService();
    private readonly FakeNavigationService _navigationService = new FakeNavigationService();
    private readonly IMessenger _messenger = new WeakReferenceMessenger();

    private AllNotesViewModel CreateViewModel()
    {
        AllNotesViewModel viewModel = new AllNotesViewModel(_fileService, _navigationService, _messenger);
        return viewModel;
    }

    // Test 9 — Charger remplit la liste, quel que soit le nombre de notes.
    // [Theory] : le même test est exécuté une fois par ligne [InlineData].
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    public void LoadNotes_FillsNotesFromFileService(int noteCount)
    {
        // Arrange
        for (int i = 1; i <= noteCount; i++)
        {
            Note note = new Note();
            note.Filename = "note-" + i + ".txt";
            _fileService.Notes.Add(note);
        }
        AllNotesViewModel viewModel = CreateViewModel();

        // Act
        viewModel.LoadNotes();

        // Assert
        Assert.Equal(noteCount, viewModel.Notes.Count);
    }

    // Test 10 — Recevoir « note supprimée » retire la note de la liste
    [Fact]
    public void ReceiveNoteDeletedMessage_RemovesNoteFromList()
    {
        // Arrange : deux notes dans la liste
        Note firstNote = new Note();
        firstNote.Filename = "note-1.txt";
        Note secondNote = new Note();
        secondNote.Filename = "note-2.txt";
        _fileService.Notes.Add(firstNote);
        _fileService.Notes.Add(secondNote);
        AllNotesViewModel viewModel = CreateViewModel();
        viewModel.LoadNotes();

        // Act : on envoie le message, comme le ferait NoteViewModel
        _messenger.Send(new NoteDeletedMessage(firstNote));

        // Assert
        Note remainingNote = Assert.Single(viewModel.Notes);
        Assert.Equal("note-2.txt", remainingNote.Filename);
    }

    // Test 11 — Le bouton « + » ouvre une note neuve
    [Fact]
    public void NewNoteCommand_NavigatesToNewNote()
    {
        // Arrange
        AllNotesViewModel viewModel = CreateViewModel();

        // Act
        viewModel.NewNoteCommand.Execute(null);

        // Assert : une note a été ouverte, et elle n'a pas encore de fichier
        Assert.NotNull(_navigationService.LastOpenedNote);
        Assert.Null(_navigationService.LastOpenedNote.Filename);
    }
}
