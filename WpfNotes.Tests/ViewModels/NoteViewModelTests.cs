using CommunityToolkit.Mvvm.Messaging;
using WpfNotes.Core.Messages;
using WpfNotes.Core.Models;
using WpfNotes.Core.ViewModels;
using WpfNotes.Tests.Fakes;

namespace WpfNotes.Tests.ViewModels;

// Les tests de NoteViewModel, avec des fakes écrits à la main.
// xUnit crée un nouvel objet de cette classe pour CHAQUE test : les champs ci-dessous
// sont donc neufs à chaque fois, et les tests ne peuvent pas se gêner entre eux.
public class NoteViewModelTests
{
    private readonly FakeFileService _fileService = new FakeFileService();
    private readonly FakeDialogService _dialogService = new FakeDialogService();
    private readonly FakeNotificationService _notificationService = new FakeNotificationService();
    private readonly FakeNavigationService _navigationService = new FakeNavigationService();

    // Un Messenger neuf pour chaque test, au lieu de WeakReferenceMessenger.Default partagé
    private readonly IMessenger _messenger = new WeakReferenceMessenger();

    // Crée le ViewModel à tester, avec tous les fakes
    private NoteViewModel CreateViewModel(Note note)
    {
        NoteViewModel viewModel = new NoteViewModel(
            note, _fileService, _dialogService, _notificationService, _navigationService, _messenger);
        return viewModel;
    }

    // Test 1 — Modifier le texte prévient la vue
    [Fact]
    public void Text_WhenChanged_RaisesPropertyChanged()
    {
        // Arrange
        NoteViewModel viewModel = CreateViewModel(new Note());

        // Act et Assert : l'action doit déclencher PropertyChanged pour la propriété "Text"
        Assert.PropertyChanged(viewModel, "Text", () => viewModel.Text = "Bonjour");
    }

    // Test 2 — Supprimer est grisé pour une note neuve
    [Fact]
    public void DeleteCommand_ForNewNote_CannotExecute()
    {
        // Arrange
        NoteViewModel viewModel = CreateViewModel(new Note());

        // Act
        bool canExecute = viewModel.DeleteCommand.CanExecute(null);

        // Assert
        Assert.False(canExecute);
    }

    // Test 3 — Enregistrer écrit la note
    [Fact]
    public void SaveCommand_WritesNoteToFileService()
    {
        // Arrange
        NoteViewModel viewModel = CreateViewModel(new Note());
        viewModel.Text = "Bonjour";

        // Act
        viewModel.SaveCommand.Execute(null);

        // Assert
        Note savedNote = Assert.Single(_fileService.Notes);
        Assert.Equal("Bonjour", savedNote.Text);
    }

    // Test 4 — Enregistrer prévient la liste par un message
    [Fact]
    public void SaveCommand_SendsNoteSavedMessage()
    {
        // Arrange : le test s'abonne lui-même au message
        NoteSavedMessage? receivedMessage = null;
        _messenger.Register<NoteSavedMessage>(this, (recipient, message) => receivedMessage = message);
        NoteViewModel viewModel = CreateViewModel(new Note());
        viewModel.Text = "Bonjour";

        // Act
        viewModel.SaveCommand.Execute(null);

        // Assert
        Assert.NotNull(receivedMessage);
        Assert.Equal("Bonjour", receivedMessage.Note.Text);
    }

    // Test 5 — Enregistrer revient à la liste
    [Fact]
    public void SaveCommand_NavigatesBackToList()
    {
        // Arrange
        NoteViewModel viewModel = CreateViewModel(new Note());

        // Act
        viewModel.SaveCommand.Execute(null);

        // Assert
        Assert.Equal(1, _navigationService.NavigatedToAllNotesCount);
    }

    // Test 6 — Annuler revient à la liste sans rien enregistrer
    [Fact]
    public void CancelCommand_NavigatesBackWithoutSaving()
    {
        // Arrange
        NoteViewModel viewModel = CreateViewModel(new Note());
        viewModel.Text = "Brouillon";

        // Act
        viewModel.CancelCommand.Execute(null);

        // Assert
        Assert.Empty(_fileService.Notes);
        Assert.Equal(1, _navigationService.NavigatedToAllNotesCount);
    }

    // Test 7 — Supprimer, puis répondre « non » : rien n'est supprimé
    [Fact]
    public async Task DeleteCommand_WhenUserSaysNo_DeletesNothing()
    {
        // Arrange : le fake répondra « non »
        _dialogService.Answer = false;
        Note note = new Note();
        note.Filename = "note-1.txt";
        NoteViewModel viewModel = CreateViewModel(note);

        // Act : la commande est asynchrone, on attend qu'elle se termine
        await viewModel.DeleteCommand.ExecuteAsync(null);

        // Assert
        Assert.Equal(1, _dialogService.CallCount);
        Assert.Empty(_fileService.DeletedFilenames);
        Assert.Empty(_notificationService.Messages);
    }

    // Test 8 — Supprimer, puis répondre « oui » : la note est supprimée, et tout le monde est prévenu
    [Fact]
    public async Task DeleteCommand_WhenUserSaysYes_DeletesNotifiesAndNavigates()
    {
        // Arrange
        _dialogService.Answer = true;
        Note note = new Note();
        note.Filename = "note-1.txt";
        NoteViewModel viewModel = CreateViewModel(note);

        // Act
        await viewModel.DeleteCommand.ExecuteAsync(null);

        // Assert
        Assert.Contains("note-1.txt", _fileService.DeletedFilenames);
        Assert.Single(_notificationService.Messages);
        Assert.Equal(1, _navigationService.NavigatedToAllNotesCount);
    }
}
