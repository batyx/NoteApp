using System.Windows;
using MaterialDesignThemes.Wpf;
using WpfNotes.Core.ViewModels;

namespace WpfNotes;

public partial class MainWindow : Window
{
    // Plus aucune logique ici : la fenêtre reçoit son ViewModel et la file de messages du Snackbar
    public MainWindow(MainViewModel viewModel, SnackbarMessageQueue messageQueue)
    {
        InitializeComponent();

        DataContext = viewModel;
        MainSnackbar.MessageQueue = messageQueue;
    }
}
