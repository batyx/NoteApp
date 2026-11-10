using System.Windows;
using System.Windows.Controls;
using WpfNotes.CodeBehind.Models;

namespace WpfNotes.CodeBehind.Views;

public partial class AllNotesView : UserControl
{
    public AllNotesView()
    {
        InitializeComponent();
    }

    private void NewNoteButton_Click(object sender, RoutedEventArgs e)
    {
        Note newNote = new Note();
        MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
        mainWindow.ShowNote(newNote);
    }
}
