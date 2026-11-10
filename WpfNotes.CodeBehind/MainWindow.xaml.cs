using System.IO;
using System.Windows;
using WpfNotes.CodeBehind.Models;
using WpfNotes.CodeBehind.Views;

namespace WpfNotes.CodeBehind;

public partial class MainWindow : Window
{
    // Dossier où sont rangées les notes, par exemple C:\Users\toi\AppData\Local\WpfNotes\Notes
    public static readonly string NotesFolder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "WpfNotes",
        "Notes");

    public MainWindow()
    {
        InitializeComponent();
        ShowAllNotes();
    }

    // Affiche l'écran « liste des notes »
    public void ShowAllNotes()
    {
        MainContent.Content = new AllNotesView();
    }

    // Affiche l'écran « une note », pour la note reçue en paramètre
    public void ShowNote(Note note)
    {
        MainContent.Content = new NoteView(note);
    }

    // Affiche un court message en bas de la fenêtre
    public void ShowMessage(string message)
    {
        if (MainSnackbar.MessageQueue != null)
        {
            MainSnackbar.MessageQueue.Enqueue(message);
        }
    }
}
