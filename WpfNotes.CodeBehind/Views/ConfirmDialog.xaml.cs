using System.Windows.Controls;

namespace WpfNotes.CodeBehind.Views;

public partial class ConfirmDialog : UserControl
{
    public ConfirmDialog(string message)
    {
        InitializeComponent();
        MessageText.Text = message;
    }
}
