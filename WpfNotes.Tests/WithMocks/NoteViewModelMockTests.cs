using CommunityToolkit.Mvvm.Messaging;
using NSubstitute;
using WpfNotes.Core.Models;
using WpfNotes.Core.Services;
using WpfNotes.Core.ViewModels;

namespace WpfNotes.Tests.WithMocks;

// Les tests 3, 7 et 8 réécrits avec NSubstitute, à comparer avec leur version à fakes
public class NoteViewModelMockTests
{
    // Substitute.For<T>() fabrique un objet qui respecte l'interface, sans écrire de classe
    private readonly IFileService _fileService = Substitute.For<IFileService>();
    private readonly IDialogService _dialogService = Substitute.For<IDialogService>();
    private readonly INotificationService _notificationService = Substitute.For<INotificationService>();
    private readonly INavigationService _navigationService = Substitute.For<INavigationService>();
    private readonly IMessenger _messenger = new WeakReferenceMessenger();

    private NoteViewModel CreateViewModel(Note note)
    {
        NoteViewModel viewModel = new NoteViewModel(
            note, _fileService, _dialogService, _notificationService, _navigationService, _messenger);
        return viewModel;
    }

    // Test 3 avec un mock — vérifier l'argument reçu
    [Fact]
    public void SaveCommand_WritesNoteWithItsText()
    {
        // Arrange
        NoteViewModel viewModel = CreateViewModel(new Note());
        viewModel.Text = "Bonjour";

        // Act
        viewModel.SaveCommand.Execute(null);

        // Assert : SaveNote a été appelée une fois, avec une note dont le texte est "Bonjour"
        _fileService.Received(1).SaveNote(Arg.Is<Note>(note => note.Text == "Bonjour"));
    }

    // Test 7 avec un mock — configurer une réponse
    [Fact]
    public async Task DeleteCommand_WhenUserSaysNo_DeletesNothing()
    {
        // Arrange : « quand on te demande une confirmation, quelle que soit la question, réponds non »
        _dialogService.ConfirmAsync(Arg.Any<string>()).Returns(false);
        Note note = new Note();
        note.Filename = "note-1.txt";
        NoteViewModel viewModel = CreateViewModel(note);

        // Act
        await viewModel.DeleteCommand.ExecuteAsync(null);

        // Assert : DeleteNote n'a jamais été appelée
        _fileService.DidNotReceive().DeleteNote(Arg.Any<string>());
    }

    // Test 8 avec un mock — vérifier qu'un appel a eu lieu une seule fois
    [Fact]
    public async Task DeleteCommand_WhenUserSaysYes_NotifiesOnce()
    {
        // Arrange
        _dialogService.ConfirmAsync(Arg.Any<string>()).Returns(true);
        Note note = new Note();
        note.Filename = "note-1.txt";
        NoteViewModel viewModel = CreateViewModel(note);

        // Act
        await viewModel.DeleteCommand.ExecuteAsync(null);

        // Assert
        _notificationService.Received(1).Show("Note supprimée");
    }
}
