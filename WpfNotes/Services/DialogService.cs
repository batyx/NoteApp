using MaterialDesignThemes.Wpf;
using WpfNotes.Core.Services;
using WpfNotes.Views;

namespace WpfNotes.Services;

// L'implémentation réelle de IDialogService : le DialogHost de MaterialDesign
public class DialogService : IDialogService
{
    public async Task<bool> ConfirmAsync(string message)
    {
        // "RootDialog" est l'Identifier du DialogHost déclaré dans MainWindow.xaml
        ConfirmDialog dialog = new ConfirmDialog(message);
        object? answer = await DialogHost.Show(dialog, "RootDialog");
        bool confirmed = object.Equals(answer, true);
        return confirmed;
    }
}
