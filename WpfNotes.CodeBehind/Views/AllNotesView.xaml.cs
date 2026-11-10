using System.IO;
using System.Windows;
using System.Windows.Controls;
using WpfNotes.CodeBehind.Models;

namespace WpfNotes.CodeBehind.Views;

public partial class AllNotesView : UserControl
{
    public AllNotesView()
    {
        InitializeComponent();
        LoadNotes();
    }

    // Lit toutes les notes du dossier et les affiche
    private void LoadNotes()
    {
        Directory.CreateDirectory(MainWindow.NotesFolder);

        // 1. Une note par fichier .txt du dossier
        List<Note> notes = new List<Note>();
        string[] paths = Directory.GetFiles(MainWindow.NotesFolder, "*.txt");
        foreach (string path in paths)
        {
            Note note = new Note();
            note.Filename = Path.GetFileName(path);
            note.Text = File.ReadAllText(path);
            note.Date = File.GetLastWriteTime(path);
            notes.Add(note);
        }

        // 2. La plus récente en premier
        List<Note> sortedNotes = notes.OrderByDescending(note => note.Date).ToList();

        // 3. Donner la liste à l'ItemsControl, qui dessine une carte par note
        NotesList.ItemsSource = sortedNotes;

        // 4. Le message « Aucune note » n'apparaît que si la liste est vide
        if (notes.Count == 0)
        {
            EmptyText.Visibility = Visibility.Visible;
        }
        else
        {
            EmptyText.Visibility = Visibility.Collapsed;
        }
    }

    private void NoteCard_Click(object sender, RoutedEventArgs e)
    {
        // sender est le bouton cliqué ; son DataContext est la note qu'il affiche
        Button clickedButton = (Button)sender;
        Note clickedNote = (Note)clickedButton.DataContext;

        MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
        mainWindow.ShowNote(clickedNote);
    }

    private void NewNoteButton_Click(object sender, RoutedEventArgs e)
    {
        Note newNote = new Note();
        MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
        mainWindow.ShowNote(newNote);
    }
}
