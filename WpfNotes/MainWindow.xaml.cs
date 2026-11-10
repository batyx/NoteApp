using System.Windows;
using WpfNotes.Core.Services;
using WpfNotes.Core.ViewModels;

namespace WpfNotes;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // On assemble les pièces à la main : le service, puis le ViewModel qui l'utilise
        IFileService fileService = new FileService();
        AllNotesViewModel viewModel = new AllNotesViewModel(fileService);
        viewModel.LoadNotes();

        // Le ViewModel devient la source des Binding de la fenêtre et de ce qu'elle contient
        DataContext = viewModel;
    }
}
