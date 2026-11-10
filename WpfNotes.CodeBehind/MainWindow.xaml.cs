using System.Windows;
using WpfNotes.CodeBehind.Views;

namespace WpfNotes.CodeBehind;

public partial class MainWindow : Window
{
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

    // Affiche l'écran « une note »
    public void ShowNote()
    {
        MainContent.Content = new NoteView();
    }
}
