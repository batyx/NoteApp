using System.Windows;
using System.Windows.Controls;

namespace WpfNotes.CodeBehind.Views;

public partial class NoteView : UserControl
{
    public NoteView()
    {
        InitializeComponent();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        // Revenir à la liste, sans rien enregistrer
        MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
        mainWindow.ShowAllNotes();
    }
}
