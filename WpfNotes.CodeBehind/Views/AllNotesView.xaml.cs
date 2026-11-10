using System.Windows;
using System.Windows.Controls;

namespace WpfNotes.CodeBehind.Views;

public partial class AllNotesView : UserControl
{
    public AllNotesView()
    {
        InitializeComponent();
    }

    private void NewNoteButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
        mainWindow.ShowNote();
    }
}
