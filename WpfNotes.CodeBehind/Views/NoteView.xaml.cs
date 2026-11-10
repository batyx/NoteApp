using System.IO;
using System.Windows;
using System.Windows.Controls;
using WpfNotes.CodeBehind.Models;

namespace WpfNotes.CodeBehind.Views;

public partial class NoteView : UserControl
{
    // La note affichée par cet écran
    private readonly Note _note;

    public NoteView(Note note)
    {
        InitializeComponent();

        _note = note;
        NoteText.Text = _note.Text;
        UpdateDisplay();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        // Revenir à la liste, sans rien enregistrer
        MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
        mainWindow.ShowAllNotes();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        // 1. Récupérer le texte saisi
        _note.Text = NoteText.Text;

        // 2. Première sauvegarde : choisir un nom de fichier unique
        if (_note.Filename == null)
        {
            _note.Filename = "note-" + DateTime.Now.ToString("yyyyMMdd-HHmmss-fff") + ".txt";
        }

        // 3. Écrire le fichier sur le disque
        Directory.CreateDirectory(MainWindow.NotesFolder);
        string path = Path.Combine(MainWindow.NotesFolder, _note.Filename);
        File.WriteAllText(path, _note.Text);
        _note.Date = File.GetLastWriteTime(path);

        // 4. Prévenir l'utilisateur et revenir à la liste
        MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
        mainWindow.ShowMessage("Note enregistrée");
        mainWindow.ShowAllNotes();
    }

    // Affiche la date en haut de l'écran
    private void UpdateDisplay()
    {
        if (_note.Filename == null)
        {
            DateText.Text = "Nouvelle note";
        }
        else
        {
            DateText.Text = "Enregistrée le " + _note.Date.ToString("dd/MM/yyyy HH:mm");
        }
    }
}
